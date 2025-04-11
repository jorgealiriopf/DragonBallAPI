using DragonBall.Domain.Entities;
using DragonBall.Domain.Interfaces;
using DragonBall.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class CharacterRepository : ICharacterRepository
{
    private readonly AppDbContext _context;

    public CharacterRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Character>> GetAllAsync()
        => await _context.Characters.Include(c => c.Transformations).ToListAsync();

    public async Task AddAsync(Character character)
        => await _context.Characters.AddAsync(character);

    public async Task<Character?> GetByIdAsync(int id)
        => await _context.Characters.Include(c => c.Transformations)
                                    .FirstOrDefaultAsync(c => c.Id == id);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public async Task<List<Character>> GetByAffiliationAsync(string affiliation)
        => await _context.Characters.Where(c => c.Affiliation == affiliation).ToListAsync();

    public async Task<List<Character>> GetByNameAsync(string name)
        => await _context.Characters.Where(c => c.Name.Contains(name)).ToListAsync();
}
