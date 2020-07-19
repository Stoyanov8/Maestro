using Core.Services;
using Identity.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Core.Constants.Roles;

namespace Identity.Services
{
    public class IdentitySeeder : IDataSeeder
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IdentitySeeder(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void SeedData()
        {
            if (this.roleManager.Roles.Any())
                return;

            Task.Run(async () =>
                {
                    var roles = new Dictionary<string, string>() {

                        { "97f11350-2ef8-441f-bc3a-04c806717900", EmployeeRole },
                        { "c083eb98-310d-42a4-b867-c3f4373151bd", AdministratorRole },
                        { "3f5d7133-eb69-4e45-89e3-e44e8fb7316f", UserRole }
                    };

                    foreach ((var id, var roleName) in roles)
                    {
                        await CreateFakeUsers(id, roleName);
                    }
                })
                .GetAwaiter()
                .GetResult();
        }
        private async Task CreateFakeUsers(string fakeId, string roleName)
        {
            var role = new IdentityRole(roleName);
            await this.roleManager.CreateAsync(role);

            var user = new User
            {
                Id = fakeId,
                UserName = $"{roleName}@maestro.bg",
                Email = $"{roleName}@maestro.bg",
                FirstName = roleName,
                LastName = $"{roleName}ov",
                SecurityStamp = "RandomSecurityStamp"
            };

            await userManager.CreateAsync(user, roleName + 123);

            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}