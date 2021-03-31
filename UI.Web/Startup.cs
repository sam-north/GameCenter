using Core.DataAccess;
using Core.Framework.Models;
using Core.Logic.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UI.Web.Hubs;
using UI.Web.Models;

namespace GameCenter
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie();
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddSignalR();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<ModelContext>(options => options.
                    UseSqlServer(Configuration.GetConnectionString("Default")));

            RegisterAppAssemblies(services);
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<GameHub>("/gameHub");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    var proxyToSpaDevelopmentServer = Configuration["ProxyToSpaDevelopmentServer"];
                    if (!string.IsNullOrWhiteSpace(proxyToSpaDevelopmentServer))
                    {
                        spa.UseProxyToSpaDevelopmentServer(proxyToSpaDevelopmentServer);
                    }
                    else
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                }
            });
        }

        private void RegisterAppAssemblies(IServiceCollection services)
        {
            var assemblies = new List<Assembly>
            {
                typeof(ICrudExampleLogic).Assembly,       //Core
            };

            var classes = new List<Type>();
            var interfaces = new List<Type>();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                classes.AddRange(types.Where(o => o.IsClass).ToList());
                interfaces.AddRange(types.Where(o => o.IsInterface).ToList());
            }
            foreach (var targetClass in classes)
            {
                var matchingInterface = interfaces.SingleOrDefault(o => o.Name.Substring(1) == targetClass.Name && !targetClass.IsAbstract);
                if (matchingInterface != null)
                    services.AddTransient(matchingInterface, targetClass);
            }

            services.AddTransient<IRequestContext, RequestContext>();
        }
    }
}
