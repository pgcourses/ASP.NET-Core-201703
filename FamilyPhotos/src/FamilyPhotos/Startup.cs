using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FamilyPhotos.Repository;
using FamilyPhotos.Filters;
using FamilyPhotos.Loggers;

namespace FamilyPhotos
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Ha tesztelni akarjuk az indulás közbeni hibakezelést, akkor ezt például így tehetjük meg
            //throw new Exception("ez itt egy hiba");

            //Azért, hogy minden egyes kérésnél ugyanahhoz a repositoryhoz jussunk, Singleton-ként kell regisztrálnunk.
            //A C# Singleton mintáról részletesen: http://csharpindepth.com/articles/general/singleton.aspx
            services.AddSingleton<PhotoRepository, PhotoRepository>();

            var autoMapperCfg = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile(new ViewModel.PhotoProfile()));
            var mapper = autoMapperCfg.CreateMapper();

            services.AddSingleton(mapper); //innentől kérhetem a Controller paraméterlistájában

            services.AddMvc(o =>
            {
                o.Filters.Add(new MyExceptionFilter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole()
                         .AddDebug(LogLevel.Trace)
                         .AddMyLogger()
                         ;

            //Ha nem csak egyszerű státuszkóddal akarunk válaszolni, hanem 
            //szeretnénk egyszerű információkat adni, akkor például így tudunk
            //400-599 közötti kódokhoz megoldás
            //de csak, ha a például a kivételt előtte kezeltük és a státuszkóddal térünk vissza
            //az action-ből

            //alapértelmezés
            //app.UseStatusCodePages();

            //Különböző beállítási lehetőségek
            //app.UseStatusCodePages("text/plain", "Ez egy hibás kérés, a kód: {0}");
            //app.UseStatusCodePages( async context => 
            //{
            //    context.HttpContext
            //           .Response
            //           .ContentType = "text/plain";

            //    await context.HttpContext
            //                 .Response
            //                 .WriteAsync($"Ez a UseStatusCodePages delegate settings, a kód pedig: {context.HttpContext.Response.StatusCode}");
            //});
            //és még rengeteg beállítási lehetőség: 
            //http://www.talkingdotnet.com/handle-404-error-asp-net-core-mvc-6/

            //Például átirányíthatjuk saját oldalra:
            //app.UseStatusCodePagesWithRedirects("~/Errors/StatusCodePagesWithRedirects/{0}"); //így id-vel kell átvenni
            //app.UseStatusCodePagesWithRedirects("~/Errors/StatusCodePagesWithRedirects?statusCode={0}"); //így meg statusCode-dal

            //Vagy visszaküldjük újrafeldolgozásra, ekkor több információhoz is hozzáférünk
            app.UseStatusCodePagesWithReExecute("/Errors/StatusCodePagesWithReExecute", "?statusCode={0}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Hibakezelés saját action-nel (middleware-rel): 
                //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling
                //app.UseExceptionHandler("/Errors"); //Ez így az /Errors/Index-re megy
                app.UseExceptionHandler("/Errors/ExceptionHandler");
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
