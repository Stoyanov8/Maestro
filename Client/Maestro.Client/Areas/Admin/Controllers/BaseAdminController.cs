using Client.Controllers;
using Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using static Maestro.Client.Constants.Area;

namespace Maestro.Client.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    [AuthorizeAdministrator]
    public class BaseAdminController : BaseController
    {
    }
}
