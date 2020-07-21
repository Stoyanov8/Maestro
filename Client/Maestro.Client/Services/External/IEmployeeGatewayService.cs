using Maestro.Client.Areas.Admin.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Client.Services.External
{
    public interface IEmployeeGatewayService
    {
        [Get("/Employees/GetEmployees")]
        Task<IEnumerable<EmployeeInformationOutputModel>> GetEmployees();
    }
}
