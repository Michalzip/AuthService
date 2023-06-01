using Microsoft.EntityFrameworkCore;
using AuthService.Modules.infrastructure.EF.Configurations;
using AuthService.Modules.Core.Entities;

namespace AuthService.Modules.infrastructure.EF.Context
{
    public class UsersDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}