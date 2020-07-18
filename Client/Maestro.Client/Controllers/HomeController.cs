using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index() => View();
    }
}