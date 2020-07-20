using Core.Models;
using Maestro.Employees.Data.Models;

namespace Maestro.Employees.Models
{
    public class WorkOutputModel : IMapFrom<Work>
    {
        public string Id { get; set; }

        public string RequestId { get; set; }
    }
}
