using Core.Services;
using Maestro.Requests.Data;
using Maestro.Requests.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.Requests.Services.Seeders
{
    public class RequestDataSeeder : IDataSeeder
    {
        private readonly RequestDbContext _context;

        public RequestDataSeeder(RequestDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (_context.Requests.Any())
                return;


            Task.Run(async () =>
            {
                var category = _context.Categories.FirstOrDefault(x => x.Name == "Other");

                var req = new Request
                {
                    CategoryId = category.Id,
                    Description = "My sink broke..halp ;( .."
                };

                await _context.Requests.AddAsync(req);

                await _context.SaveChangesAsync();

            }).GetAwaiter().GetResult();
        }
    }
}
