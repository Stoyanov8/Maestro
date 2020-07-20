using System.Collections.Generic;

namespace Maestro.Client.Models.Employee
{
    public class EmployeeWorkOutputModel
    {
        //Employee Id
        public string Id { get; set; }

        public IEnumerable<WorkOutputModel> Work { get; set; }
    }
}
