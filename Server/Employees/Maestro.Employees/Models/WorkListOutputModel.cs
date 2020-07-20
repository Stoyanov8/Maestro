using System.Collections.Generic;

namespace Maestro.Employees.Models
{
    public class WorkListOutputModel
    {
        public IEnumerable<WorkOutputModel> Work { get; set; }
    }
}
