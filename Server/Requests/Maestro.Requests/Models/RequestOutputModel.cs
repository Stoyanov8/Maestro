using Core.Models;
using Maestro.Requests.Data.Models;

namespace Maestro.Requests.Models
{
    public class RequestOutputModel : IMapFrom<Request>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        // User Id
        public string IssuerId { get; set; }

        public string CategoryName { get; set; }

        //public void Mapping(Profile mapper)
        // => mapper
        //     .CreateMap<Request, RequestOutputModel>()
        //     .ForMember(x=> x.CategoryName, c=> c.MapFrom(b=> b.Category.Na))
    }
}
