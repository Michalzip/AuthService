using AuthService.Modules.Core.Entities;

namespace AuthService.Modules.Core.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetAsync(string name);
        Task<IReadOnlyList<Role>> GetAllAsync();
        Task AddAsync(Role role);
    }
}