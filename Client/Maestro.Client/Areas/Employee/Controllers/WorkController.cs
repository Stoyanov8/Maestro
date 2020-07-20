using Client.Services.External;
using Maestro.Client.Areas.Employee.ViewModels;
using Maestro.Client.Models.Employee;
using Maestro.Client.Models.Request;
using Maestro.Client.Services.External;
using Maestro.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Core.Constants.Roles;
using static Maestro.Client.Constants.Area;

namespace Maestro.Client.Areas.Employee.Controllers
{
    [Authorize(Roles = EmployeeRole)]
    [Area(EmployeeArea)]
    public class WorkController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRequestService _requestService;

        public WorkController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Mine()
        {
            //TODO Cant this be in one request using a gateway ? (x files theme music starts playing)
            var myWork = await _employeeService.MyWork();

            var requests = await _requestService
                .GetAllIn(new RequestsByIdsInputModel
                {
                    Ids = myWork.Work.Select(w => w.Id)
                });

            var map = myWork.Work.ToDictionary(x => x.RequestId);

            var model = requests.Select(request =>
                 new WorkViewModel
                 {
                     CategoryName = request.CategoryName,
                     Description = request.Description,
                     Status = map[request.Id].Status.GetDescription()
                 });

            return View(model);
        }

        public async Task<IActionResult> Available()
        {
            var myWork = await _employeeService.AvailableWork();

            return View();
        }

        public async Task<IActionResult> TakeWork(string workId)
        {
            await _employeeService.TakeWork(new TakeWorkInputModel { Id = workId });

            return RedirectToAction(nameof(Mine));
        }

        public async Task<IActionResult> CloseWork(string workId)
        {
            await _employeeService.CloseWork(new WorkInputModel { Id = workId });

            return RedirectToAction(nameof(Mine));
        }
    }
}
