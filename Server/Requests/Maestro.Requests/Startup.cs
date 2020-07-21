using Core.Infrastructure;
using Core.Services;
using Maestro.Core.Infrastructure;
using Maestro.Requests.Data;
using Maestro.Requests.Data.Models;
using Maestro.Requests.Services.Category;
using Maestro.Requests.Services.Requests;
using Maestro.Requests.Services.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maestro.Requests
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
            .AddWebService<RequestDbContext>(this.Configuration)             
            .AddTransient<IDataSeeder, CategoryDataSeeder>()             
            .AddTransient<IDataSeeder, RequestDataSeeder>()             
            .AddTransient<IRequestService, RequestService>()             
            .AddTransient<ICategoryService, CategoryService>()            
            .AddMessaging(this.Configuration);

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize();
    }
}
