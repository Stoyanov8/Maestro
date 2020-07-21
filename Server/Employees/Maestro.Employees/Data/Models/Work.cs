using Maestro.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maestro.Employees.Data.Models
{
    public class Work
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string RequestId { get; set; }

        public WorkStatus Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
