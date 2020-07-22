using System;
using System.Collections.Generic;

namespace Server.Core.Messages
{
    public class WorkCompletedMessage
    {
        public string EmployeeId { get; set; }

        public IEnumerable<Tuple<DateTime, DateTime >> PastWorkPeriod { get; set; }
    }
}
