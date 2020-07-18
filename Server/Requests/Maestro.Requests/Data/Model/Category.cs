using System.ComponentModel.DataAnnotations.Schema;

namespace Requests.Data.Model
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
