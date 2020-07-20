using Maestro.Core.Enums;

namespace Maestro.Employees.Models
{
    public class WorkInputModel
    {
        public string Id { get; set; }

        public string RequestId { get; set; }

        public WorkStatus Status { get; set; }
    }
}
