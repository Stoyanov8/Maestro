using Maestro.Statistics.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Maestro.Statistics.Data
{
    public class StatisticsDbContext : DbContext
    {
        public StatisticsDbContext(DbContextOptions<StatisticsDbContext> options)
               : base(options)
        {
        }

        public DbSet<AverageEmployeeWorkTime> AverageWorkTime { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
