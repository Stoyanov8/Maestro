using System.Collections.Generic;

namespace Maestro.Employees.Models
{
    public class EmployeeWorkOutputModel
    {
        public string Id { get; set; }

        public IEnumerable<WorkOutputModel> Work { get; set; }
    }
}
