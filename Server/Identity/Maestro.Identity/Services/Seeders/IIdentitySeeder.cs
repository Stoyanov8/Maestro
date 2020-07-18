using Core.Services;
using Identity.Data.Models;
using Microsoft.AspNetCore.Identity;
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
                    await this.roleManager.CreateAsync(new IdentityRole(EmployeeRole));

                    var adminRole = new IdentityRole(AdministratorRole);
                    await this.roleManager.CreateAsync(adminRole);

                    var adminUser = new User
                    {
                        UserName = "ab@a.b",
                        Email = "ab@a.b",
                        FirstName = "A",
                        LastName = "B",
                        SecurityStamp = "RandomSecurityStamp"
                    };

                    await userManager.CreateAsync(adminUser, "koftiparola");

                    await userManager.AddToRoleAsync(adminUser, AdministratorRole);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}