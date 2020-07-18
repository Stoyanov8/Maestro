using Core.Services.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class RequestsController : ApiController
    {
        public Task<IActionResult> Index()
        {
            throw new NotImplementedException();
        }
    }
}
