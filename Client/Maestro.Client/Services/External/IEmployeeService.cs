using Core.Models;
using Maestro.Client.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Threading.Tasks;

namespace Maestro.Client.Services.External
{
    public interface IEmployeeService
    {

        [Get("/Employee/MyWork")]
        Task<EmployeeWorkOutputModel> MyWork();

        [Post("/Request/GetCategories")]
        Task<Result> TakeWork([Body] TakeWorkInputModel work);

        [Post("/Employee/CloseWork")]
        Task<Result> CloseWork([Body] WorkInputModel work);
    }
}
