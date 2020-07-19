using Client.Models.Identity;
using Refit;
using System.Threading.Tasks;

namespace Client.Services.External
{
    public interface IIdentityService
    {
        [Post("/Identity/Login")]
        Task<UserOutputModel> Login([Body] UserBaseInputModel loginInput);

        [Post("/Identity/Register")]
        Task<UserOutputModel> Register([Body] UserRegisterModel registerInput);
    }
}