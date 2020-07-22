using System;

namespace Maestro.Client.Areas.Admin.Models
{
    public class EmployeeInformationOutputModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public DateTime EmployeeSince { get; set; }

        public int CurrentWorkCount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public TimeSpan? AverageWorkTime =>  new TimeSpan(AverageTime);

        public long AverageTime { get; set; }
    }
}
