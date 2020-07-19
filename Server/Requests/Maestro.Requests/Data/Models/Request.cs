using System.ComponentModel.DataAnnotations.Schema;

namespace Maestro.Requests.Data.Models
{
    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Description { get; set; }

        // User Id
        public string IssuerId { get; set; }

        public Category Category { get; set; }

        public string CategoryId { get; set; }
    }
}
