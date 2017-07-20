using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FamilyPhotosWithIdentity.Data;
using FamilyPhotosWithIdentity.Models;
using FamilyPhotosWithIdentity.Services;
using DataTables.AspNet.AspNetCore;
using FamilyPhotosWithIdentity.Helpers;

namespace FamilyPhotosWithIdentity
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Itt lehet a jelszó policy-t megváltoztatni.
            services.AddIdentity<ApplicationUser, ApplicationRole>( options=> {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddAuthentication();

            services.AddAuthorization(options => 
            {
                options.AddPolicy("RequiredElevatedAdminRights", 
                    policy => policy.RequireRole("Administrators"));
                options.AddPolicy("RequiredElevatedAdminRightsForAPI",
                    policy => policy.RequireClaim("role", "Administrators"));

                options.AddPolicy("RequiredElevatedAdminRigthsCombined",
                    policy => policy.RequireAssertion(context => {
                        return context.User.IsInRole("Administrators")
                                || context.User.HasClaim(
                                    claim=>claim.Type=="role" && claim.Value=="Administrators");
                    }));

            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //A DataTables DI szervize
            services.RegisterDataTables();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseOAuthValidation();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseOpenIdConnectServer(
                options => {
                    options.AllowInsecureHttp = true;
                    options.TokenEndpointPath = "/api/token";
                    options.AccessTokenLifetime = TimeSpan.FromDays(1);
                    //azért, hogy ne kelljen ide írni a kódot, extension függvényt írunk
                    //options.Provider.OnValidateTokenRequest = context => { return Task.FromResult(0); };
                    options.UseMyValidateTokenRequest();
                    options.UseMyHandleTokenRequest();

                }
            );

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
