
using AuthService.Modules.infrastructure.EF.Context;
using AuthService.Modules.Core.ValueObjects.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AuthService.Modules.Core.Entities;
using AuthService.Shared;

namespace AuthService.Modules.infrastructure.EF
{
    public class UsersInitializer : IInitializer
    {
        private const string Admin = $"{AvailableRole.Admin}";
        private const string User = $"{AvailableRole.User}";

        private readonly HashSet<string> _adminPermissions = new()
        {

        };

        private readonly HashSet<string> _userPermissions = new()
        {

        };
        private readonly ILogger<UsersInitializer> _logger;
        private readonly UsersDbContext _dbContext;
        public UsersInitializer(UsersDbContext dbContext, ILogger<UsersInitializer> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task InitAsync()
        {
            //check if in table exists any record with data 
            if (await _dbContext.Roles.AnyAsync())
            {
                await InitializeRoleAsync();
                return;
            }

            await AddRolesAsync();
            await _dbContext.SaveChangesAsync();
        }

        private async Task InitializeRoleAsync()
        {
            var admin = await _dbContext.Roles.Where(q => q.Name == Admin).SingleOrDefaultAsync();
            if (admin is null)
            {
                //seeder
                await AddAdminRole();
            }
            else
            {
                //check if progamer add additional permissions
                await InitializeAdminRole(admin);
            }

            var user = await _dbContext.Roles.Where(q => q.Name == User).SingleOrDefaultAsync();
            if (user is null)
            {
                await AddUserRole();
            }
            else
            {
                await InitializeUserRole(user);
            }

            _logger.LogInformation("Initialize roles.");
        }
        private async Task AddRolesAsync()
        {
            await _dbContext.Roles.AddAsync(new Role
            {
                Name = Admin,
                Permissions = _adminPermissions
            });

            await _dbContext.Roles.AddAsync(new Role
            {
                Name = User,
                Permissions = _userPermissions
            });


            _logger.LogInformation("Initialized roles.");
        }
        private async Task AddAdminRole()
        {
            await _dbContext.Roles.AddAsync(new Role
            {
                Name = Admin,
                Permissions = _adminPermissions
            });
            await _dbContext.SaveChangesAsync();
        }

        private async Task AddUserRole()
        {
            await _dbContext.Roles.AddAsync(new Role
            {
                Name = User,
                Permissions = _userPermissions
            });
            await _dbContext.SaveChangesAsync();
        }

        private async Task InitializeAdminRole(Role admin)
        {
            var adminDifferences = _adminPermissions.Except(admin.Permissions)
                .Union(admin.Permissions.Except(_adminPermissions)).ToList();

            if (adminDifferences.Count > 0)
            {
                admin.Permissions = _adminPermissions;
                _dbContext.Roles.Update(admin);
                await _dbContext.SaveChangesAsync();
            }
        }


        private async Task InitializeUserRole(Role user)
        {
            //Expect return as list collection permission that exists within _userPermissions but not within user.Permissions 
            //the code uses another Except() method to find the elements in service.Permissions that are not present in _servicePermissions.
            //The code then uses the Union() method to combine the two collections of differences obtained from the previous steps
            var companyEmployeeDifferences = _userPermissions.Except(user.Permissions)
                .Union(user.Permissions.Except(_userPermissions)).ToList();
            //if difference exists add it to the database
            if (companyEmployeeDifferences.Count > 0)
            {
                user.Permissions = _userPermissions;
                _dbContext.Roles.Update(user);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}