using Maestro.Employees.Gateway.Models.Employee;
using Refit;
using System.Threading.Tasks;

namespace Maestro.Employees.Gateway.Services.External
{
    public interface IEmployeeService
    {
        [Get("/Employee/GetEmployees")]
        Task<EmployeesOutputModel> GetEmployees();
    }
}
