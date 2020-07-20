using Maestro.Client.Models.Employee;
using System.Collections.Generic;

namespace Maestro.Client.Areas.Employee.Models
{
    public class WorkListOutputModel
    {
        public IEnumerable<WorkOutputModel> Work { get; set; }
    }
}
