using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Day1.API.Repositories;

namespace Day1.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICourseRepository, CourseMockRepository>();
            //Felvesszük a szervizek közé az Mvc-t, de ezzel még nem tudjuk használni
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //throw new Exception("Ez elszállt");

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});


            //Megmondjuk, hogy a futtatási környezet használja az MVC környezetet (routing, stb,)
            //app.UseMvc(); //API esetén elég ez, mert ott Attributumokkal címezzük a routingot, de a webalkalmazásnál már névkonvenció alapú

            //app.UseMvc(
            //    routeBuilder => {
            //        routeBuilder.MapRoute(
            //            name: "Default", 
            //            template: "{controller}/{action}/{id?}",
            //            defaults: new { controller="Home", action="Index" });
            //});

            //WebAlkalmazások routingja ilyen például
            app.UseMvcWithDefaultRoute();
        }
    }
}
