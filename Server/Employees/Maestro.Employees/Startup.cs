using Core.Infrastructure;
using Core.Services;
using Maestro.Core.Infrastructure;
using Maestro.Employees.Consumers;
using Maestro.Employees.Services;
using Maestro.Employees.Services.Seeders;
using Maestro.Requests.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maestro.Employees
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
          => services
              .AddWebService<EmployeesDbContext>(this.Configuration)
              .AddTransient<IDataSeeder, EmployeesDataSeeder>()
              .AddTransient<IEmployeeService, EmployeeService>()
              .AddMessaging(this.Configuration, typeof(UserPromotedConsumer), typeof(RequestCreatedConsumer));

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize()
                .UseHangfire();
    }
}
