using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Maestro.Core.Enums
{
    public enum WorkStatus
    {
        [Description(nameof(Pending))]
        Pending = 1,
        [Description("In Progress")]
        InProgress,
        [Description(nameof(Completed))]
        Completed
    }
}
