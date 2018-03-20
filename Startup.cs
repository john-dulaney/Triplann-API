using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Triplann.Data;
using Triplann.Models;

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
                    builder
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
            
 // Set up identity server so services like SignInManager can be injected into controllers
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

             // Set up JWT authentication service
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";  
                options.DefaultChallengeScheme = "Jwt";              
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7A735D7B-1A19-4D8A-9CFA-99F55483013F")), 
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });
        }    
        

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();
            app.UseCors("AllowSpecificOrigin");
            app.UseMvc();
            }
        }
    }