using Client.Models.Request;
using Core.Models;
using Maestro.Client.Models.Request;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Services.External
{
    public interface IRequestService
    {
       
        [Get("/Request/GetCategories")]
        Task<IEnumerable<CategoryOutputModel>> GetCategories();

        [Post("/Request/Create")]
        Task<Result> Create([Body] RequestInputModel model);

        [Get("/Request/GetCurrentUserRequests")]
        Task<IEnumerable<RequestOutputModel>> GetCurrentUserRequests();
    }
}
