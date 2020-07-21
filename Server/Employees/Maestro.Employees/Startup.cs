using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure;
using Core.Services;
using Maestro.Core.Infrastructure;
using Maestro.Core.Messages;
using Maestro.Employees.Consumers;
using Maestro.Employees.Services;
using Maestro.Employees.Services.Seeders;
using Maestro.Requests.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                .Initialize();
    }
}
