using Core.Infrastructure;
using Core.Services;
using Maestro.Requests.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Requests.Data;
using Requests.Services;

namespace Requests
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
              .AddTransient<IRequestService, RequestService>();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize();
    }
}
