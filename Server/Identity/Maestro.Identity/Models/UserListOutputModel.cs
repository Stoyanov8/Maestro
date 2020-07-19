using Core.Models;
using Identity.Data.Models;
using System.Collections.Generic;

namespace Maestro.Identity.Models
{
    public class UserListOutputModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
