using Client.Infrastructure;
using Client.Models;
using Client.Services.External;
using Core.Infrastructure;
using Core.Models;
using Core.Services.Identity;
using Maestro.Client.Services.External;
using Maestro.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using System.Reflection;

namespace Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceEndpoints = this.Configuration
                  .GetSection(nameof(ServiceEndpoints))
                  .Get<ServiceEndpoints>(config => config.BindNonPublicProperties = true);

            services
                .AddAutoMapperProfile(Assembly.GetExecutingAssembly())
                .AddTokenAuthentication(this.Configuration)
                .AddScoped<ICurrentTokenService, CurrentTokenService>()
                .AddTransient<JwtCookieAuthenticationMiddleware>()
                .AddControllersWithViews(options => options
                    .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services
                .AddRefitClient<IIdentityService>()
                .WithConfiguration(serviceEndpoints.Identity);

            services
              .AddRefitClient<IRequestService>()
              .WithConfiguration(serviceEndpoints.Requests);

            services
         .AddRefitClient<IEmployeeGatewayService>()
         .WithConfiguration(serviceEndpoints.EmployeesGateway);

            services
       .AddRefitClient<IEmployeeService>()
       .WithConfiguration(serviceEndpoints.Employees);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage();
            }
            else
            {
                app
                    .UseExceptionHandler("/Home/Error")
                    .UseHsts();
            }

            app
                .UseStaticFiles()
                .UseRouting()
                .UseJwtCookieAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area}/{controller}/{action=Index}/{id?}");

                    endpoints.MapDefaultControllerRoute();
                });
        }
    }
}