﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Triplann.Data;

namespace Triplann {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddCors (options => {
                options.AddPolicy ("AllowSpecificOrigin", builder =>
                    builder.WithOrigins ("http://localhost:5000")
                    .AllowAnyHeader ()
                    .AllowAnyMethod ()
                    .AllowAnyOrigin ()
                    .AllowCredentials ()
                );
            });

            services.AddMvc ();
            // var connection = $"Data Source={System.Environment.GetEnvironmentVariable("Triplan")}";
            var connection = "Data Source=./bin/Triplann.db";
            Console.WriteLine ($"{connection}");
            services.AddDbContext<ApplicationDbContext> (options => options.UseSqlite (connection));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            app.UseCors (
                "AllowSpecificOrigin"
            );
            app.UseMvc ();
        }
    }
}