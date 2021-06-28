using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace store
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
     
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
                c.AddPolicy(name: MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200/", "http://localhost:3000/", "*");
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "wwwroot";
            });
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/json"
            });
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default",template: "{controller}/{action=Index}/{id?}");
                routes.MapRoute(name: "Handoff-iOS9", template: ".well-known/apple-app-site-association",
                    defaults: new { controller = "Home", action = "WellKnownAppleHandoff" });
                
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "wwwroot";

                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = new TimeSpan(0, 4, 30);
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
