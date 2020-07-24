using Core.Constants;
using Core.Services.Controllers;
using Maestro.Employees.Models;
using Maestro.Employees.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using static Core.Constants.Roles;

namespace Maestro.Employees.Controllers
{
    [Authorize(Roles = AdministratorRole)]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route(nameof(GetEmployees))]
        public async Task<ActionResult<EmployeesOutputModel>> GetEmployees()
        {
            return await _employeeService.GetEmployees();
        }
    }
}
