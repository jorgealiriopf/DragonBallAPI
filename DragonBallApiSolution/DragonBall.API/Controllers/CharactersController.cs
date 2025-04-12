using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DragonBall.Infrastructure.Persistence;
using DragonBall.Infrastructure.Services;

namespace DragonBallAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly DragonBallService _dragonBallService;

        public CharactersController(AppDbContext context, DragonBallService dragonBallService)
        {
            _context = context;
            _dragonBallService = dragonBallService;
        }
        [Authorize]
        [HttpPost("sync")]
        public async Task<IActionResult> SyncCharacters()
        {
            await _dragonBallService.ImportCharactersAsync();
            return Ok("Synchronization completed.");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCharacters()
        {
            var characters = await _context.Characters.Include(c => c.Transformations).ToListAsync();
            return Ok(characters);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(int id)
        {
            var character = await _context.Characters.Include(c => c.Transformations).FirstOrDefaultAsync(c => c.Id == id);

            if (character == null)
                return NotFound();

            return Ok(character);
        }
        [Authorize]
        [HttpGet("/api/transformations")]
        public async Task<IActionResult> GetTransformations()
        {
            var transformations = await _context.Transformations.Include(t => t.Character).ToListAsync();
            return Ok(transformations);
        }
        [Authorize]
        [HttpGet("search/by-name")]
        public async Task<IActionResult> GetByName(string name)
        {
            var results = await _context.Characters
                .Include(c => c.Transformations)
                .Where(c => c.Name.Contains(name))
                .ToListAsync();

            return Ok(results);
        }
        [Authorize]
        [HttpGet("search/by-affiliation")]
        public async Task<IActionResult> GetByAffiliation(string affiliation)
        {
            var results = await _context.Characters
                .Include(c => c.Transformations)
                .Where(c => c.Affiliation.Contains(affiliation))
                .ToListAsync();

            return Ok(results);
        }
    }
}