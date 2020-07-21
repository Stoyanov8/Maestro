using Maestro.Employees.Gateway.Models.User;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Employees.Gateway.Services.External
{
    public interface IIdentityService
    {
        [Post("/Identity/GetEmployees")]
        Task<IEnumerable<UserOutputModel>> GetEmployees([Body] UsersIdInputModel model);
    }
}
