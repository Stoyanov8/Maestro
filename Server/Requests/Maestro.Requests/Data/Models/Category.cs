using System.ComponentModel.DataAnnotations.Schema;

namespace Maestro.Requests.Data.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
