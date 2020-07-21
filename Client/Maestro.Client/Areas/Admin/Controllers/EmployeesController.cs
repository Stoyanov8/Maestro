using Maestro.Client.Services.External;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Maestro.Client.Areas.Admin.Controllers
{
    public class EmployeesController : BaseAdminController
    {
        private readonly IEmployeeGatewayService _employeeService;

        public EmployeesController()
        {

        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetEmployees();

            return View(employees);
        }
    }
}
