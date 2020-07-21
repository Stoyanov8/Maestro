using Maestro.Employees.Gateway.Models.User;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Employees.Gateway.Services.External
{
    public interface IIdentityService
    {
        [Post("/Identity/GetAllIn")]
        Task<IEnumerable<UserOutputModel>> GetAllIn([Body] UsersIdInputModel model);
    }
}
