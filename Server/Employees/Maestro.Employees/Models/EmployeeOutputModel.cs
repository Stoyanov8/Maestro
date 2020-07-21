using AutoMapper;
using Core.Models;
using Maestro.Employees.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maestro.Employees.Models
{
    public class EmployeeOutputModel : IMapFrom<Employee>
    {
        public string Id { get; set; }

        public DateTime EmployeeSince { get; set; }

        public int CurrentWorkCount { get; set; }

        public string UserId { get; set; }

        void Mapping(Profile mapper)
        {
             mapper.CreateMap<Employee, EmployeeOutputModel>()
                .ForMember(e=> e.CurrentWorkCount, e=> e.MapFrom(c=> c.Work.Count()));
        }
    }

    public class EmployeesOutputModel
    {
        public IEnumerable<EmployeeOutputModel> Employees { get; set; }
    }
}
