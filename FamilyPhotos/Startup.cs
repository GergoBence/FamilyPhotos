﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FamilyPhotos.Data;
using FamilyPhotos.Filters;
using FamilyPhotos.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace FamilyPhotos
{
    public class Startup
    {
        private readonly IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment enviroment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(enviroment.ContentRootPath)
                .AddJsonFile("appsettings.json",optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{enviroment.EnvironmentName}.json", optional:true)
                .AddEnvironmentVariables()
                ;
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //beüzemeljük az EntityFramework Core eszközeit
            services.AddDbContext<FamilyPhotosContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

                //options.UseSqlServer("Server=(LocalDB)\\localDBDemo;Database=FamilyPhotosDB;Trusted_Connection=True;");
                //options.UseSqlServer("Server=(LocalDB)\\localDBDemo; Initial Catalog = FamilyPhotosDB; Integrated Security = True;");
                //options.UseSqlServer("Server=(LocalDB)\\localDBDemo;Database=FamilyPhotosDB;Trusted_Connection=True;");

            });


            //services.AddSingleton<IPhotoRepository, PhotoTestDataRepository>();
            services.AddSingleton<IPhotoRepository, PhotoEfCoreDataRepository>();

            var autoMapperCfg = new AutoMapper.MapperConfiguration(
            cfg => cfg.AddProfile(new ViewModel.PhotoProfile()));
            var mapper = autoMapperCfg.CreateMapper();

            services.AddSingleton(mapper);  //innentől kérhetem a controller paraméter listájában

            services.AddMvc(o =>
            {
                //o.Filters.Add(new MyExceptionFilter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {


            loggerFactory.AddConsole();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /// ha nem csak egyszerű stástusz kóddal akarunk válaszolni, hanem
            /// szeretnénk egyszerű információkat adni, akkor például így tudunk
            /// 400-599 közötti küdokhoz megoldást
            /// de csak, ha például a kivételt előtte kezeltük és a stástuszkóddal térünk vissza
            /// az action-ből

            //alapértelmezés
            //app.UseStatusCodePages();

            //különböző beállítási lehetőségek
            //app.UseStatusCodePages("text/plain","Ez egy hibás kérés, a kód: {0}");
            //app.UseStatusCodePages(async context =>
            //{
            //    context.HttpContext.Response.ContentType = "text/plain";
            //    await  context.HttpContext.Response.WriteAsync($"Ez a UseStatusCodePages delegate settings, a kód pedig: {context.HttpContext.Response.StatusCode}");

            //}); 

            //például átirányíthatjuk saját oldalra
            //app.UseStatusCodePagesWithRedirects("~/Errors/StatusCodeWithRedirects/{0}");

            app.UseStatusCodePagesWithReExecute("/Errors/UseStatusCodePagesWithReExecute", "?statusCode={0}");

            //hibakezelés saját action-nel (middleware):
            //app.UseExceptionHandler("/Errors"); //Ez így a Errors/Index-re
            app.UseExceptionHandler("/Errors/ExceptionHandler");

            //statikus fájlok kiszolgálása (most a boostrap stlye-hoz)
            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
