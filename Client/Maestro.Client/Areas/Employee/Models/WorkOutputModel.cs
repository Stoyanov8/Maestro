using Core.Models;
using Maestro.Core.Enums;

namespace Maestro.Client.Models.Employee
{
    public class WorkOutputModel
    {
        public string Id { get; set; }

        public string RequestId { get; set; }

        public WorkStatus Status { get; set; }
    }
}
