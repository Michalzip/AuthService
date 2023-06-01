using AuthService.Shared;
using AuthService.Shared.Clock;
using AuthService.Modules.infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;
using AuthService.Modules.Core.ValueObjects.Roles;
using AuthService.Modules.Core.Entities;
using Microsoft.AspNetCore.Identity;
using AuthService.Modules.Users.Core.Entities;

namespace AuthService.Modules.Users.infrastructure.EF
{
    public class AdminInitializer : IInitializer
    {
        //automatically admin generated with email : admin@gmail.com
        private const string AdminEmail = "admin@gmail.com";
        private const string AdminName = "admin";
        private const string AdminPassword = "!awdawd";
        private const string AdminRole = $"{AvailableRole.Admin}";
        private readonly UsersDbContext _dbContext;
        private readonly IClock _clock;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AdminInitializer(UsersDbContext dbContext, IClock clock, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _clock = clock;
        }
        public async Task InitAsync()
        {
            //if admin is not exists, create him
            if (!await _dbContext.Users.AnyAsync(q => q.Email == AdminEmail))
            {
                var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Name == AdminRole);
                var now = _clock.CurrentDateTime();
                var password = _passwordHasher.HashPassword(default, AdminPassword);
                var user = User.Create(AdminEmail, AdminName, AdminName, password, role, UserState.Active, now);
                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}