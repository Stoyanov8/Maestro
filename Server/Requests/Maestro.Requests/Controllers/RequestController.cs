using Core.Models;
using Core.Services.Controllers;
using Maestro.Requests.Models;
using Maestro.Requests.Services;
using System.Threading.Tasks;

namespace Requests.Controllers
{
    public class RequestController : ApiController
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<Result> Create(RequestInputModel model)
        {
            var response = await _requestService.Create(model);

            return response;
        }
    }
}
