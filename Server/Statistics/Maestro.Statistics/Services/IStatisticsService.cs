using Maestro.Statistics.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Statistics.Services
{
    public interface IStatisticsService
    {
        Task<IEnumerable<AverageEmployeeWorkTimeOutputModel>> GetAverageAll();

        Task<AverageEmployeeWorkTimeOutputModel> GetAverageByEmployeeId(string employeeId);

        Task AddOrUpdate(string employeeId, IEnumerable<Tuple<DateTime, DateTime>> workPeriods);
    }
}
