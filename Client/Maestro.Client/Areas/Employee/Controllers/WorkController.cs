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

        public WorkController(IEmployeeService employeeService, IRequestService requestService)
        {
            _employeeService = employeeService;
            _requestService = requestService;
        }

        public async Task<IActionResult> Mine()
        {
            var myWork = await _employeeService.MyWork();

            var model = await GetWorkViewModel(myWork.Work);

            return View(model);
        }

        public async Task<IActionResult> Available()
        {
            var available = await _employeeService.AvailableWork();

            var model = await GetWorkViewModel(available.Work);

            return View(model);
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

        private async Task<IEnumerable<WorkViewModel>> GetWorkViewModel(IEnumerable<WorkOutputModel> work)
        {
            var map = work.ToDictionary(x => x.RequestId);

            var requests = await _requestService
               .GetAllIn(new RequestsByIdsInputModel
               {
                   Ids = work.Select(w => w.RequestId)
               });

            return requests.Select(request =>
                  new WorkViewModel
                  {
                      Id = map[request.Id].Id,
                      CategoryName = request.CategoryName,
                      Description = request.Description,
                      StatusText = map[request.Id].Status.GetDescription(),
                      Status = map[request.Id].Status
                  });
        }
    }
}
