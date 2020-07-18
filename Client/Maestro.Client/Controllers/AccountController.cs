using Client.Models.Identity;
using Client.Services.External;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Core.Infrastructure.InfrastructureConstants;

namespace Client.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
            => this._identityService = identityService;

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public Task<IActionResult> Login(UserBaseInputModel model)
            => HandleUserInput(() => this._identityService.Login(model), model, nameof(Login));

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public Task<IActionResult> Register(UserRegisterModel model)
            => HandleUserInput(() => this._identityService.Register(model), model, nameof(Register));


        [Authorize]
        public IActionResult Logout()
        {
            this.Response.Cookies.Delete(AuthenticationCookieName);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private async Task<IActionResult> HandleUserInput(Func<Task<UserOutputModel>> action, UserBaseInputModel model, string caller)
        {
            if (!ModelState.IsValid)
            {
                return View(caller, model);
            }

            return await Handle(async () =>
            {
                var response = await action();

                this.Response.Cookies.Append(
                AuthenticationCookieName,
                    response.Token,
                 new CookieOptions
                 {
                     HttpOnly = true,
                     Secure = true,
                     MaxAge = TimeSpan.FromDays(7)
                 });
            }, RedirectToAction(nameof(HomeController.Index), "Home"),
                View(caller, model));
        }
    }
}