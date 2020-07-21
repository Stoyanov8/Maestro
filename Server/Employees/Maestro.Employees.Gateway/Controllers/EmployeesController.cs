﻿using Core.Services.Controllers;
using Maestro.Employees.Gateway.Models.Employee;
using Maestro.Employees.Gateway.Models.User;
using Maestro.Employees.Gateway.Services.External;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.Employees.Gateway.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IIdentityService _identityService;

        public EmployeesController(IEmployeeService employeeService, IIdentityService identityService)
        {
            _employeeService = employeeService;
            _identityService = identityService;
        }

        public async Task<IEnumerable<EmployeeInformationOutputModel>> GetEmployees()
        {
            var outputEmployees = await _employeeService.GetEmployees();

            var userInfo = await _identityService.GetEmployees(new UsersIdInputModel { Ids = outputEmployees.Employees.Select(e => e.UserId) });

            var empdict = outputEmployees.Employees.ToDictionary(x => x.UserId);

            var output = new List<EmployeeInformationOutputModel>();

            foreach (var u in userInfo)
            {
                var currentEmployee = empdict[u.UserId];
                output.Add(new EmployeeInformationOutputModel
                {
                    CurrentWorkCount = currentEmployee.CurrentWorkCount,
                    Id = currentEmployee.Id,
                    EmployeeSince = currentEmployee.EmployeeSince,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                });
            }

            return output;
        }
    }
}
