using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maestro.Statistics.Data.Models
{
    // So basically I ran out of ideas and this happend
    public class AverageEmployeeWorkTime
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string EmployeeId { get; set; }

        public long AverageTime { get; set; }
    }
}
