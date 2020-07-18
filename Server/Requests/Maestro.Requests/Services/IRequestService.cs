using Core.Models;
using Maestro.Requests.Models;
using System.Threading.Tasks;

namespace Maestro.Requests.Services
{
    public interface IRequestService
    {
        Task<Result> Create(RequestInputModel model);
    }
}
