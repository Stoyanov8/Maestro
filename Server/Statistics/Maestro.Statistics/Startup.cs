using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure;
using Maestro.Core.Infrastructure;
using Maestro.Statistics.Consumers;
using Maestro.Statistics.Data;
using Maestro.Statistics.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Maestro.Statistics
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
             .AddWebService<StatisticsDbContext>(this.Configuration)
             .AddTransient<IStatisticsService, StatisticsService>()
             .AddMessaging(this.Configuration,typeof(WorkCompledConsumer));

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseWebService(env)
                .Initialize()
                .UseHangfire();
    }
}
