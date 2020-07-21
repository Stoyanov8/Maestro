using Maestro.Employees.Models;
using Maestro.Employees.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Employees.Controllers
{

    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<ActionResult<EmployeesOutputModel>> GetEmployees()
        {
            return await _employeeService.GetEmployees();
        }
    }
}
