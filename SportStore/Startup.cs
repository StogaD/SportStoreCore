using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportStore.EF;
using SportStore.Models;

namespace SportStore
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EFProductContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddSession();
            services.AddMemoryCache();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
           
            app.UseMvc(routes =>
           {
               routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" }
                    );
                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    }
                    );
               routes.MapRoute(
               name: null,
               template: "{category}",
               defaults: new
               {
                   controller = "Product",
                   action = "List",
                   productPage = 1
               }
               );
               routes.MapRoute(
               name: null,
               template: "",
               defaults: new
               {
                   controller = "Product",
                   action = "List",
                   productPage = 1
               });
               routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
           });
            SeedData.EnsurePopulated(app);
        }
    }
}
