using Core.Models;
using Maestro.Statistics.Data.Models;
using System;

namespace Maestro.Statistics.Models
{
    public class AverageEmployeeWorkTimeOutputModel : IMapFrom<AverageEmployeeWorkTime>
    {
        public string EmployeeId { get; set; }

        public long AverageTime { get; set; }
    }
}
