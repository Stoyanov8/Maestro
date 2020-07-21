using Client.Models.Identity;
using Core.Models;
using Maestro.Client.Areas.Admin.Models;
using Maestro.Client.Models.Identity;
using Microsoft.AspNetCore.Components;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Services.External
{
    public interface IIdentityService
    {
        [Post("/Identity/Login")]
        Task<UserOutputModel> Login([Body] UserBaseInputModel loginInput);

        [Post("/Identity/Register")]
        Task<UserOutputModel> Register([Body] UserRegisterModel registerInput);

        [Get("/Identity/All")]
        Task<IEnumerable<UserListOutputModel>> All();


        [Post("/Identity/PromoteToEmployee")]
        Task<Result> PromoteToEmployee([Body] UserRoleInputModel userRole);

        [Get("/Identity/Delete")]        
        Task<Result> Delete([Query]string userId);
    }
}