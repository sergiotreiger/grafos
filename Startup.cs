using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Grafos.Repository.Implementations;
using Grafos.Repository.Interfaces;
using Grafos.Services.Interfaces;
using Grafos.Services.Implementations;
using Grafos.Repository.Configuration;

namespace Grafos
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
            
            services.AddTransient(typeof(IGraphService), typeof(GraphService));
            services.AddTransient(typeof(IRouteService), typeof(RouteService));
            services.AddTransient(typeof(IGraphRepository), typeof(GraphRepository));
            services.AddDbContext<GraphDataContext>(options =>
                        options.UseInMemoryDatabase("GraphDataBase"));
                        
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, GraphDataContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            DbInitializer.Initialize(context);
        }
    }
}
