using Core.Models;
using Core.Services;
using Maestro.Employees.Data.Models;
using Maestro.Employees.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Employees.Services
{
    public interface IEmployeeService : IDataService<Employee>
    {
        Task<Result<EmployeeWorkOutputModel>> GetMyWork();

        Task CreateWork(CreateWorkInputModel input);

        Task<Result> TakeWork(TakeWorkInputModel input);

        Task<Result> CloseWork(WorkInputModel input);

        Task<Result<WorkListOutputModel>> AvailableWork();
    }
}
