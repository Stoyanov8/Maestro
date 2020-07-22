using Maestro.Employees.Gateway.Models.Statistics;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Employees.Gateway.Services.External
{
    public interface IStatisticsService
    {

        [Get("/Statistics/GetAllAverageEmployeeTime")]
        Task<IEnumerable<AverageEmployeeWorkTimeOutputModel>> GetAllAverageEmployeeTime();

        [Get("/Statistics/GetAverageEmployeeTimeByEmployeeId")]
        Task<AverageEmployeeWorkTimeOutputModel> GetAverageEmployeeTimeByEmployeeId([Query]string employeeId);
    }
}
