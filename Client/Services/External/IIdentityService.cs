using Identity.Models;
using Refit;
using System.Threading.Tasks;

namespace Client.Services.External
{
    public interface IIdentityService
    {
        [Post("/Identity/Login")]
        Task<UserOutputModel> Login([Body] UserBaseInputModel loginInput);
    }
}