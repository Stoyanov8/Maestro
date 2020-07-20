using Core.Models;
using Core.Services;
using Maestro.Requests.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Requests.Services.Requests
{
    public interface IRequestService : IDataService<Data.Models.Request>
    {
        Task<Result> Create(RequestInputModel model);
        Task<IEnumerable<RequestOutputModel>> GetCurrentUserRequests();

        Task<IEnumerable<RequestOutputModel>> RequestsIn(RequestsInIdsInputModel input);


    }
}
