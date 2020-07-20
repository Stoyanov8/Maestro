using System.Collections.Generic;

namespace Maestro.Client.Models.Employee
{
    public class EmployeeWorkOutputModel
    {
        public string Id { get; set; }

        public IEnumerable<WorkOutputModel> Work { get; set; }
    }
}
