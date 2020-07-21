using Maestro.Employees.Gateway.Models.User;
using System;

namespace Maestro.Employees.Gateway.Models.Employee
{
    public class EmployeeInformationOutputModel : UserOutputModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime EmployeeSince { get; set; }

        public int CurrentWorkCount { get; set; }
    }
}
