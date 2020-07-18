using Microsoft.EntityFrameworkCore;
using Requests.Data.Model;
using System.Reflection;

namespace Requests.Data
{
    public class RequestDbContext : DbContext
    {
        public RequestDbContext(DbContextOptions<RequestDbContext> options)
            : base(options)
        {
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
