using Core.Services.Controllers;
using Maestro.Requests.Models;
using Maestro.Requests.Services.Category;
using Maestro.Requests.Services.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Requests.Controllers
{
    public class RequestController : ApiController
    {
        private readonly IRequestService _requestService;
        private readonly ICategoryService _categoryService;

        public RequestController(IRequestService requestService,
            ICategoryService categoryService)
        {
            _requestService = requestService;
            _categoryService = categoryService;
        }

        [HttpPost]
        [Route(nameof(Create))]
        public async Task<ActionResult> Create(RequestInputModel model)
        {
            var response = await _requestService.Create(model);

            if (response.Succeeded)
            {
                return Ok();
            }

            return BadRequest(response);
        }

        [HttpGet]
        [Route(nameof(GetCategories))]
        public async Task<IEnumerable<CategoryOutputModel>> GetCategories()
            => await _categoryService.GetAll();


        [HttpGet]
        [Route(nameof(GetCurrentUserRequests))]
        [Authorize]
        public async Task<IEnumerable<RequestOutputModel>> GetCurrentUserRequests()
            => await _requestService.GetCurrentUserRequests();

        [HttpGet]
        [Route(nameof(GetAllIn))]
        public async Task<IEnumerable<RequestOutputModel>> GetAllIn(RequestsInIdsInputModel model)
            => await _requestService.RequestsIn(model);
    }
}
