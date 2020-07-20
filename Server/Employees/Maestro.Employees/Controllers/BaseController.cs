using Core.Services.Controllers;
using Microsoft.AspNetCore.Authorization;
using static Core.Constants.Roles;

namespace Maestro.Employees.Controllers
{
    [Authorize(Roles = EmployeeRole + "," + AdministratorRole)]
    public abstract class BaseController : ApiController
    {
    }
    
}
