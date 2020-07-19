using Core.Services;
using Maestro.Requests.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Requests.Services.Category
{
    public interface ICategoryService : IDataService<Data.Models.Category>
    {
        Task<IEnumerable<CategoryOutputModel>> GetAll();
    }
}
