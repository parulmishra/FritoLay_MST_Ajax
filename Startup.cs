﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FritoLay.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FritoLay
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
     
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkMySql()
                    .AddDbContext<FritoLayContext>(options =>
                                              options
                                                   .UseMySql(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<FritoLayContext>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentity();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

        }
    }
}
