using Maestro.Employees.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Maestro.Requests.Data
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Work> Work { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
