using Maestro.Core.Enums;

namespace Maestro.Client.Areas.Employee.ViewModels
{
    public class WorkViewModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string StatusText { get; set; }

        public WorkStatus Status { get; set; }
    }
}
