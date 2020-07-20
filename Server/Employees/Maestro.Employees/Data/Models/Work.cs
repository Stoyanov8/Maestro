using Maestro.Employees.Enums;
using System;

namespace Maestro.Employees.Data.Models
{
    public class Work
    {
        public string Id { get; set; }

        public string RequestId { get; set; }

        public WorkStatus Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
