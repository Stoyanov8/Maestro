using Core.Services.Identity;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(
            IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        [HttpPost]    
        public async Task<ActionResult<UserOutputModel>> Register(UserRegisterModel input)
        {
            var result = await this._identityService.Register(input);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return await Login(input);
        }

        [HttpPost]
        public async Task<ActionResult<UserOutputModel>> Login(UserBaseInputModel input)
        {
            var result = await this._identityService.Login(input);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return new UserOutputModel
            {
                Token = result.Data.Token
            };
        }
    }
}
