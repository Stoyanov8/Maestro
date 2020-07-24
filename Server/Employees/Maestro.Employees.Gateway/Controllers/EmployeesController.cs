using Core.Services.Controllers;
using Maestro.Employees.Gateway.Models.Employee;
using Maestro.Employees.Gateway.Models.User;
using Maestro.Employees.Gateway.Services.External;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static Core.Constants.Roles;


namespace Maestro.Employees.Gateway.Controllers
{
    [Authorize(Roles = AdministratorRole)]
    public class EmployeesController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IIdentityService _identityService;
        private readonly IStatisticsService _statisticsService;

        public EmployeesController(IEmployeeService employeeService,
            IIdentityService identityService,
            IStatisticsService statisticsService)
        {
            _employeeService = employeeService;
            _identityService = identityService;
            _statisticsService = statisticsService;
        }


        [HttpGet]
        [Route(nameof(GetEmployees))]
        public async Task<IEnumerable<EmployeeInformationOutputModel>> GetEmployees()
        {
            var outputEmployees = await _employeeService.GetEmployees();

            var averageTimeStatistics = _statisticsService.GetAllAverageEmployeeTime();
            var userInfo = _identityService.GetAllIn(new UsersIdInputModel { Ids = outputEmployees.Employees.Select(e => e.UserId) });

            await Task.WhenAll(averageTimeStatistics, userInfo);

            var empdict = outputEmployees.Employees.ToDictionary(x => x.UserId);

            var output = new List<EmployeeInformationOutputModel>();

            foreach (var u in userInfo.Result)
            {
                var currentEmployee = empdict[u.UserId];

                output.Add(new EmployeeInformationOutputModel
                {
                    CurrentWorkCount = currentEmployee.CurrentWorkCount,
                    Id = currentEmployee.Id,
                    EmployeeSince = currentEmployee.EmployeeSince,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    AverageTime = averageTimeStatistics.Result.FirstOrDefault(avg => currentEmployee.Id == avg.EmployeeId)?.AverageTime ?? 0
                });
            }

            return output;
        }
    }
}
