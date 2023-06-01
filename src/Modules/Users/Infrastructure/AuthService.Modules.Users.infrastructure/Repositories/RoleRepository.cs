
using AuthService.Modules.Core.Entities;
using Microsoft.EntityFrameworkCore;
using AuthService.Modules.Core.Repositories;
using AuthService.Modules.infrastructure.EF.Context;

namespace AuthService.Modules.infrastructure.Repositories
{
    internal class RoleRepository : IRoleRepository
    {
        private readonly UsersDbContext _context;
        private readonly DbSet<Role> _roles;

        public RoleRepository(UsersDbContext context)
        {
            _context = context;
            _roles = _context.Roles;
        }

        public Task<Role> GetAsync(string name) => _roles.SingleOrDefaultAsync(x => x.Name == name);

        public async Task<IReadOnlyList<Role>> GetAllAsync() => await _roles.ToListAsync();

        public async Task AddAsync(Role role)
        {
            await _roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }
    }
}