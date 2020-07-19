using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Maestro.Client.Models.Request
{
    public class RequestInputModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public IEnumerable<CategoryOutputModel> Categories { get; set; } = new List<CategoryOutputModel>();
    }
}
