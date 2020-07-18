namespace Identity.Services
{
    using Core.Models;
    using Data.Models;
    using Identity.Models;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task<Result<User>> Register(UserRegisterModel userInput);

        Task<Result<UserOutputModel>> Login(UserBaseInputModel userInput);
    }
}