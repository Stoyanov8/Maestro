using Client.Services.External;
using Maestro.Client.Models.Request;
using Maestro.Client.Services.External;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using static Core.Constants.Roles;

namespace Maestro.Client.Areas.Employee.Controllers
{
    [Authorize(Roles = EmployeeRole)]
    public class WorkController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRequestService _requestService;

        public WorkController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> MyWork()
        {
            var myWork = await _employeeService.MyWork();
            var requests = await _requestService
                .GetAllIn(new RequestsByIdsInputModel
                {
                    Ids = myWork.Work.Select(w => w.Id)
                });



            return View();
        }
    }
}
