using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductCatalogAPI.Data;

namespace ProductCatalogAPI
{
    public class Startup
    {
        //constractor 
        public Startup(IConfiguration configuration)
        {
            // where the database is created... this application read from 
            //basically the app setting from JSON
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.


        public void ConfigureServices(IServiceCollection services)
        {
            // This is required before start up
            //1. i need mvc
            //2. i need Dbcontext
            //3. where and host
            // Migration
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //My service be aware of this database Context
            // the where to install
            // catalgecontext database is created before the service up

            //var server = Configuration["DatabaseServer"];
            //var database = Configuration["DatabaseName"];
            //var user = Configuration["DatabaseUSer"];
            //var Password = Configuration["DatabasePassword"];
            //var ConnectionString = $"server={server}; Database={database}; User={user}; Password={Password};";
            var server = Configuration["DatabaseServer"];
            var database = Configuration["DatabaseName"];
            var user = Configuration["DatabaseUser"];
            var password = Configuration["DatabasePassword"];

            var connectionString = 
                $"Server={database}; User= {user}; Password= {password}";

            services.AddDbContext<CatalogContext>(options =>
            //connectionstring used to connect to the database
            options.UseSqlServer(Configuration["connectionString"])
            //options.UseSqlServer(Configuration["ConnectionString"])
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
