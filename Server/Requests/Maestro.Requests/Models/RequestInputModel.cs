using Core.Models;

namespace Maestro.Requests.Models
{
    public class RequestInputModel
    {
        public string Description { get; set; }

        public string CategoryId { get; set; }

        public string IssuerId { get; set; }
    }
}
