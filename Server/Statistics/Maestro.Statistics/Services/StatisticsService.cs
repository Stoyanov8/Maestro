using AutoMapper;
using Core.Infrastructure;
using Core.Services;
using Maestro.Statistics.Data;
using Maestro.Statistics.Data.Models;
using Maestro.Statistics.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.Statistics.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly StatisticsDbContext _context;
        private readonly IMapper _mapper;

        public StatisticsService(StatisticsDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddOrUpdate(string employeeId, IEnumerable<Tuple<DateTime, DateTime>> workPeriods)
        {
            var averageForEmployee = await _context.AverageWorkTime.SingleOrDefaultAsync(a => a.EmployeeId == employeeId);

            averageForEmployee ??= new AverageEmployeeWorkTime()
            {
                EmployeeId = employeeId
            };

            averageForEmployee.AverageTime = CalculateAverageTime(workPeriods);

            _context.Update(averageForEmployee);

            await _context.SaveChangesAsync();
        }

        private long CalculateAverageTime(IEnumerable<Tuple<DateTime, DateTime>> workPeriods)
        {
            var avg = workPeriods
                .Select((period) => new TimeSpan((period.Item2 - period.Item1).Ticks))
                .Average(timeSpan => timeSpan.Ticks);

            return Convert.ToInt64(avg);
        }

        public async Task<IEnumerable<AverageEmployeeWorkTimeOutputModel>> GetAverageAll()
        {
            var all = await _context.AverageWorkTime.ToListAsync();

            return _mapper.Map<IEnumerable<AverageEmployeeWorkTime>, IEnumerable<AverageEmployeeWorkTimeOutputModel>>(all);
        }

        public async Task<AverageEmployeeWorkTimeOutputModel> GetAverageByEmployeeId(string employeeId)
        {
            var emp = await _context.AverageWorkTime.FirstOrDefaultAsync(c => c.EmployeeId == employeeId);

            return _mapper.Map<AverageEmployeeWorkTime, AverageEmployeeWorkTimeOutputModel>(emp);
        }
    }
}
