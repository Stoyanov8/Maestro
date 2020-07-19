namespace Identity.Models
{
    public class UserRegisterModel : UserBaseInputModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}