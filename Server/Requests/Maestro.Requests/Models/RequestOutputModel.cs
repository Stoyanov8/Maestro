﻿using Core.Models;
using Maestro.Requests.Data.Models;

namespace Maestro.Requests.Models
{
    public class RequestOutputModel : IMapFrom<Request>
    {

        public string Id { get; set; }

        public string Description { get; set; }

        public string IssuerId { get; set; }

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }
    }
}
