using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.api.Roulette.Core;
using Services.api.Roulette.Core.Repository;
using Services.api.Roulette.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.api.Roulette
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
            services.Configure<MongoSettings>(
                    options =>
                    {
                        options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                        options.Database = Configuration.GetSection("MongoDb:Database").Value;
                    }
                );
            
            services.AddSingleton<MongoSettings>();
            
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddScoped<IServiceRoulette, ServiceRoulette>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
