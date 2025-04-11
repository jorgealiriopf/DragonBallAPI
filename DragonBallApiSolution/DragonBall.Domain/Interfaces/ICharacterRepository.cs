using DragonBall.Domain.Entities;

namespace DragonBall.Domain.Interfaces
{
    public interface ICharacterRepository
    {
        Task<List<Character>> GetAllAsync();
        Task<Character?> GetByIdAsync(int id);
        Task<List<Character>> GetByAffiliationAsync(string affiliation);
        Task<List<Character>> GetByNameAsync(string name);
        Task AddAsync(Character character);
        Task SaveChangesAsync();
    }
}