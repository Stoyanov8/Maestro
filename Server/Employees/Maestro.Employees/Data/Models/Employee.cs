using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maestro.Employees.Data.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } 

        public string UserId { get; set; }

        public DateTime EmployeeSince { get; set; }

        public IEnumerable<Work> Work { get; set; }
    }
}
