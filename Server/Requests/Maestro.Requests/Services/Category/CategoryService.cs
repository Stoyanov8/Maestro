using AutoMapper;
using Core.Services;
using Maestro.Requests.Data;
using Maestro.Requests.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Requests.Services.Category
{
    public class CategoryService : DataService<Data.Models.Category>, ICategoryService
    {
        private readonly IMapper _mapper;
        public CategoryService(RequestDbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryOutputModel>> GetAll()
        {
            var all = await All().ToListAsync();

            return _mapper.Map<IEnumerable<Data.Models.Category>,IEnumerable<CategoryOutputModel>>(all);
        }      
    }
}
