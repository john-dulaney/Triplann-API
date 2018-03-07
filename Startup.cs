using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using thoughtless_eels.Data;

namespace thoughtless_eels
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                    builder.WithOrigins("http://bangazon.com:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        ); 
            }
            );

            services.AddMvc();
            var connection = $"Filename={System.Environment.GetEnvironmentVariable("EelDB")}";
            Console.WriteLine($"{connection}");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connection));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(
              "AllowSpecificOrigin"  
            );
            app.UseMvc();
        }
    }
}