using Core.Services;
using Maestro.Requests.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.Requests.Services.Seeders
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
                var categories = categoryList.Select(c => new Data.Models.Category() { Name = c });

                await _context.Categories.AddRangeAsync(categories);

                await _context.SaveChangesAsync();

            }).GetAwaiter().GetResult();
        }
    }
}
