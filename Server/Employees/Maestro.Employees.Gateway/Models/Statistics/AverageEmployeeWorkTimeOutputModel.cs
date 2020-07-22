using System;

namespace Maestro.Employees.Gateway.Models.Statistics
{
    public class AverageEmployeeWorkTimeOutputModel
    {
        public string EmployeeId { get; set; }

        public int AverageTime { get; set; }
    }
}
