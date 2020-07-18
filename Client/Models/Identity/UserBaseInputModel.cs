using System.ComponentModel.DataAnnotations;

namespace Client.Models.Identity
{
    public class UserBaseInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}