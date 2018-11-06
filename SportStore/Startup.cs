using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using SportStore.EF;
using SportStore.Models;
using Microsoft.Extensions.Logging;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            var containerBuilder = new ContainerBuilder();
            var t = Configuration.GetSection("Data").GetSection("SportStoreProducts").GetValue<string>("ConnectionString");
            var t2 = Configuration.GetSection("Data:SportStoreProducts").GetValue<String>("ConnectionString");
            var t3 = Configuration.GetSection("Data:SportStoreProducts")["ConnectionString"];

            services.AddDbContext<EFProductContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
          //  services.AddTransient<IProductRepository, ProductRepository>();
            services.AddSession();
            services.AddMemoryCache();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddMvc();
            services.AddSwaggerGen(
                c => c.SwaggerDoc("v1",
                new Swashbuckle.AspNetCore.Swagger.Info { Title = "My API", Version = "v1" }));
       
            containerBuilder.Populate(services);
            containerBuilder.RegisterType<ProductRepository>().As<IProductRepository>();
            var container = containerBuilder.Build();

            var serviceProvider = new AutofacServiceProvider(container);
            

            return serviceProvider;
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
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
            app.UseMvc(routes =>
           {
               routes.MapRoute(
                    name: "Info",
                    template: "api2/{controller}/{action}/{prodId:int}",
                    defaults: new { controller = "Info", action = "GetMe" }
                    );
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
