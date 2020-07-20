using System;
using System.Collections.Generic;

namespace Maestro.Employees.Data.Models
{
    public class Employee
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime EmployeeSince { get; set; }

        public IEnumerable<Work> Work { get; set; }
    }
}
