namespace Identity.Services
{
    using Core.Models;
    using Core.Services;
    using Data.Models;
    using Identity.Models;
    using Maestro.Identity.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIdentityService : IDataService<User>
    {
        Task<Result<User>> Register(UserRegisterModel userInput);

        Task<Result<UserOutputModel>> Login(UserBaseInputModel userInput);

        Task<IEnumerable<UserListOutputModel>> GetAll();

        Task<Result> AddToRole(UserRoleInputModel input);

        Task<Result> Delete(string userId);
    }
}