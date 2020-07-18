using Core.Infrastructure;
using Core.Services;
using Identity.Data;
using Identity.Infrastructure;
using Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity
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
              .AddWebService<IdentityDbContext>(this.Configuration)
              .AddUserStorage()
              .AddTransient<IIdentityService, IdentityService>()
              .AddTransient<IDataSeeder, IdentitySeeder>()
              .AddTransient<ITokenGeneratorService, TokenGeneratorService>();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize();
    }
}