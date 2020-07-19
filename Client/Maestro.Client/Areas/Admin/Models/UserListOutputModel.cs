using Maestro.Client.Models.Identity;
using System.Collections.Generic;

namespace Maestro.Client.Areas.Admin.Models
{
    public class UserListOutputModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string RolesConcatenated => string.Join(",", this.Roles);

        public IEnumerable<string> Roles { get; set; } = new List<string>();

    }
}
