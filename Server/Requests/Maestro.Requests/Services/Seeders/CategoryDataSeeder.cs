using Core.Services;
using Requests.Data;
using Requests.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requests.Services
{
    public class CategoryDataSeeder : IDataSeeder
    {
        private readonly RequestDbContext _context;

        public CategoryDataSeeder(RequestDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (_context.Categories.Any())
                return;

            var categoryList = new List<string> { "Wall painting", "Assembling furniture", "Plastering", "Parquet replacement", "Other" };

            Task.Run(async () =>
            {
                var categories = categoryList.Select(c => new Category() { Name = c });

                await _context.AddRangeAsync(categories);

                await _context.SaveChangesAsync();

            }).GetAwaiter().GetResult();
        }
    }
}
