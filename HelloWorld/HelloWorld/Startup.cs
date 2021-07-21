using HelloWorld.API.Repository;
using HelloWorld.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorld
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
            services.AddControllersWithViews();
            services.AddScoped<DataContext>();
            services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("dbcon")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseStatusCodePages();
                app.UseStatusCodePagesWithRedirects("/ErrorPage/{0}");
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //  endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller}/{action}/{id?}");

                //endpoints.MapControllerRoute(
                //   name: "Employee",
                //   pattern: "Employeesdata",
                //   defaults:new { controller="Employees", action="Index/{id?}/{name?}"});

            });
        }
    }
}
