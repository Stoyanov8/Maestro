using System.ComponentModel.DataAnnotations;

namespace Client.Models.Identity
{
    public class UserRegisterModel : UserBaseInputModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}