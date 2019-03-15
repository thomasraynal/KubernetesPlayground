﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KubernetesPlayground.Api.Service;
using KubernetesPlayground.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KubernetesPlayground.Api
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
            var MONGO_URI = Environment.GetEnvironmentVariable("MONGO_URI");
            var MONGO_DATABASE = Environment.GetEnvironmentVariable("MONGO_DATABASE");

            var mongoRepository = new MongoRepository(MONGO_URI, MONGO_DATABASE);

            mongoRepository.SeedIfNotCreated().Wait();

            services.AddSingleton<ITestRepository>(mongoRepository);
            services.AddHealthChecks();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks(Constants.HealthCheckUrl);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
