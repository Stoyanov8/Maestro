using Client.Services.External;
using Maestro.Client.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class RequestsController : BaseController
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            var model = await CreateRequestInputModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> New(RequestInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return await Handle(async() => await _requestService.Create(model),
                RedirectToAction(nameof(Mine)), 
                View(model));
        }

        public async Task<IActionResult> Mine()
        {
            var requests = await _requestService.GetCurrentUserRequests();

            return View(requests);
        }
        private async Task<RequestInputModel> CreateRequestInputModel()
            => new RequestInputModel()
            {
                Categories = await _requestService.GetCategories()
            };
    }
}
