using AutoMapper;
using Core.Models;
using Identity.Data.Models;

namespace Maestro.Identity.Models
{
    public class UserInformationModel : IMapFrom<User>
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public void Mapping(Profile mapper)
        {
            mapper.CreateMap<User, UserInformationModel>()
               .ForMember(x => x.UserId, c => c.MapFrom(b => b.Id));
        }
    }
}
