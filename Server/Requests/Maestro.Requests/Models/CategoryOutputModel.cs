using Core.Models;
using Maestro.Requests.Data.Models;

namespace Maestro.Requests.Models
{
    public class CategoryOutputModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
