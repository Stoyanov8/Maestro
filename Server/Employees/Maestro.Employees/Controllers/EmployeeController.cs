using Core.Services.Controllers;
using Maestro.Employees.Models;
using Maestro.Employees.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using static Core.Constants.Roles;

namespace Maestro.Employees.Controllers
{
    [Authorize(Roles = EmployeeRole)]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<ActionResult<EmployeeWorkOutputModel>> MyWork()      
            => await _employeeService.GetMyWork();

        //TODO event
        public async Task CreateWork([FromBody] CreateWorkInputModel work)
           => await _employeeService.CreateWork(work);

        public async Task<ActionResult> TakeWork([FromBody] TakeWorkInputModel work)
            => await _employeeService.TakeWork(work);

        public async Task<ActionResult> CloseWork([FromBody] WorkInputModel work)
           => await _employeeService.CloseWork(work);

    }
}
