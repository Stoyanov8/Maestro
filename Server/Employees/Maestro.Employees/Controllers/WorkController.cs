﻿using System.Threading.Tasks;
using Core.Services.Controllers;
using Maestro.Employees.Models;
using Maestro.Employees.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static Core.Constants.Roles;

namespace Maestro.Employees.Controllers
{
    [Authorize(Roles = EmployeeRole)]
    public class WorkController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public WorkController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        [Route(nameof(MyWork))]
        public async Task<ActionResult<EmployeeWorkOutputModel>> MyWork()
             => await _employeeService.GetMyWork();


        [HttpGet]
        [Route(nameof(AvailableWork))]
        public async Task<ActionResult<WorkListOutputModel>> AvailableWork()
             => await _employeeService.AvailableWork();



        [HttpPost]
        [Route(nameof(TakeWork))]
        public async Task<ActionResult> TakeWork([FromBody] TakeWorkInputModel work)
             => await _employeeService.TakeWork(work);

        [HttpPost]
        [Route(nameof(CloseWork))]
        public async Task<ActionResult> CloseWork([FromBody] WorkInputModel work)
           => await _employeeService.CloseWork(work);
    }
}
