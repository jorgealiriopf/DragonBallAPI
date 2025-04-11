using DragonBall.Domain.Entities;
using DragonBall.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using System.Text.Json;
using DragonBall.Application.Dtos;



namespace DragonBall.Infrastructure.Services

{
    public class DragonBallService
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public DragonBallService(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task ImportCharactersAsync()
        {
            if (_context.Characters.Any())
            {
                Console.WriteLine("You need to clean up the data first");
                return;
            }

            string? nextUrl = "https://dragonball-api.com/api/characters";
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            while (!string.IsNullOrEmpty(nextUrl))
            {
                var response = await _httpClient.GetAsync(nextUrl);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error fetching data: {response.StatusCode}");
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<CharacterResponseDto>(json, options);

                if (responseObject?.Items == null || responseObject.Items.Count == 0)
                {
                    Console.WriteLine("No characters found in the response.");
                    return;
                }

                Console.WriteLine($"Total characters on this page: {responseObject.Items.Count}");

                foreach (var characterDto in responseObject.Items)
                {
                    Console.WriteLine($"Evaluating: {characterDto.Name}");

                    if (characterDto.Race?.Equals("Saiyan", StringComparison.OrdinalIgnoreCase) == true &&
                        characterDto.Affiliation?.Equals("Z Fighter", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        var detailResponse = await _httpClient.GetAsync($"https://dragonball-api.com/api/characters/{characterDto.Id}");
                        if (!detailResponse.IsSuccessStatusCode) continue;

                        var detailJson = await detailResponse.Content.ReadAsStringAsync();
                        var characterDetail = JsonSerializer.Deserialize<CharacterDto>(detailJson, options);
                        if (characterDetail == null) continue;

                        var character = new Character
                        {
                            Name = characterDetail.Name ?? "Unknown",
                            Ki = characterDetail.Ki ?? "0",
                            Race = characterDetail.Race ?? "Unknown",
                            Gender = characterDetail.Gender ?? "Unknown",
                            Affiliation = characterDetail.Affiliation ?? "Unknown",
                            Description = characterDetail.Description ?? "No description",
                            Transformations = new List<Transformation>()
                        };

                        if (characterDetail.Transformations != null)
                        {
                            foreach (var t in characterDetail.Transformations)
                            {
                                character.Transformations.Add(new Transformation
                                {
                                    Name = t.Name ?? "Unknown",
                                    Power = t.Ki ?? "0"
                                });
                            }
                        }

                        _context.Characters.Add(character);
                        Console.WriteLine($"Added: {character.Name}");
                    }
                }

                await _context.SaveChangesAsync();

                nextUrl = responseObject.Links?.Next;
            }

            Console.WriteLine("Import completed!");
        }
    }
}
