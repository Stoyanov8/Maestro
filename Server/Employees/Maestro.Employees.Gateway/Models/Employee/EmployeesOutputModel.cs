using System;
using System.Collections.Generic;

namespace Maestro.Employees.Gateway.Models.Employee
{
    public class EmployeesOutputModel
    {
        public IEnumerable<EmployeeOutputModel> Employees { get; set; }
    }

    public class EmployeeOutputModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime EmployeeSince { get; set; }

        public int CurrentWorkCount { get; set; }
    }
}
