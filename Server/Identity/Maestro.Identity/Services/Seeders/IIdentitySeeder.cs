using Core.Services;
using Identity.Data.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Core.Constants.Roles;

namespace Identity.Services
{
    public class IdentitySeeder : IDataSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IBus _bus;

        public IdentitySeeder(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, IBus bus)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _bus = bus;
        }

        public void SeedData()
        {
            if (this._roleManager.Roles.Any())
                return;

            Task.Run(async () =>
                {
                    var roles = new Dictionary<string, bool>() {

                        { EmployeeRole,false  },
                        {AdministratorRole, true  },
                        { UserRole,true }
                    };

                    foreach ((var roleName, var seedUser) in roles)
                    {
                        await CreateRoles(roleName, seedUser);
                    }
                })
                .GetAwaiter()
                .GetResult();
        }
        private async Task CreateRoles(string roleName,bool seedUser)
        {
            var role = new IdentityRole(roleName);
            await this._roleManager.CreateAsync(role);

            if (seedUser)
            {
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = $"{roleName}@maestro.bg",
                    Email = $"{roleName}@maestro.bg",
                    FirstName = roleName,
                    LastName = $"{roleName}ov",
                    SecurityStamp = "RandomSecurityStamp"
                };

                await _userManager.CreateAsync(user, roleName + 123);

                await _userManager.AddToRoleAsync(user, roleName);
            }
        }
    }
}