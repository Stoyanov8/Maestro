using Client.Services.External;
using Maestro.Client.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Core.Constants.Roles;
namespace Maestro.Client.Areas.Admin.Controllers
{
    /// <summary>
    ///  This controller will visualize all users
    ///  Will have functionality to promote from User to Employee and delete users
    /// /summary>
    public class UsersController : BaseAdminController
    {
        private readonly IIdentityService _identityService;

        public UsersController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _identityService.All();

            return View(model);
        }

        public async Task<IActionResult> PromoteToEmployee(string userId)
        {
            var model = new UserRoleInputModel { UserId = userId, Role = EmployeeRole };
            await _identityService.AddToRole(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string userId)
        {
            var result = await _identityService.Delete(userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
