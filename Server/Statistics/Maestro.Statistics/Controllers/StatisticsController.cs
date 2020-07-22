using Core.Services.Controllers;
using Maestro.Statistics.Models;
using Maestro.Statistics.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Core.Constants.Roles;

namespace Maestro.Statistics.Controllers
{
    [Authorize(Roles = AdministratorRole)]
    public class StatisticsController : ApiController
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        [Route(nameof(GetAllAverageEmployeeTime))]
        public async Task<IEnumerable<AverageEmployeeWorkTimeOutputModel>> GetAllAverageEmployeeTime()
            => await _statisticsService.GetAverageAll();

        [HttpGet]
        [Route(nameof(GetAverageEmployeeTimeByEmployeeId))]
        public async Task<AverageEmployeeWorkTimeOutputModel> GetAverageEmployeeTimeByEmployeeId([FromQuery]string employeeId)       
            => await _statisticsService.GetAverageByEmployeeId(employeeId);
    }
}
