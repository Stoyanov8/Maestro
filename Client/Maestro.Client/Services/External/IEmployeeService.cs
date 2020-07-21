using Core.Models;
using Maestro.Client.Areas.Employee.Models;
using Maestro.Client.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Threading.Tasks;

namespace Maestro.Client.Services.External
{
    public interface IEmployeeService
    {

        [Get("/Work/MyWork")]
        Task<EmployeeWorkOutputModel> MyWork();

        [Get("/Work/AvailableWork")]
        Task<WorkListOutputModel> AvailableWork();


        [Post("/Work/TakeWork")]
        Task<Result> TakeWork([Body] TakeWorkInputModel work);

        [Post("/Work/CloseWork")]
        Task<Result> CloseWork([Body] WorkInputModel work);
    }
}
