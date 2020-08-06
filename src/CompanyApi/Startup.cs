using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyApi.DBContexts;
using CompanyApi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CompanyApi
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
            var server = "";
            var developerFlag = Configuration["DeveloperFlag"];
            Console.WriteLine("Started :" + developerFlag);
            server = developerFlag == "False" ? Configuration.GetConnectionString("DBServer") : "localhost";
            Console.WriteLine("Server :" + server);
            var port = Configuration.GetConnectionString("DBPort");
            var database = Configuration.GetConnectionString("Database");
            var user = Configuration.GetConnectionString("DBUser");
            var password = Configuration.GetConnectionString("DBPassword");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            try
            {
                services.AddDbContext<HRMSDBContext>(options => options.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}"));
                services.AddTransient<ICompanyRepository, CompanyRepository>();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
