using DragonBall.Domain.Entities;
using DragonBall.Domain.Interfaces;

namespace DragonBall.Application.Services
{
    public class CharacterService
    {
        private readonly ICharacterRepository _repository;

        public CharacterService(ICharacterRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Character>> GetAllCharactersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Character?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Character>> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<List<Character>> GetByAffiliationAsync(string affiliation)
        {
            return await _repository.GetByAffiliationAsync(affiliation);
        }
    }
}
