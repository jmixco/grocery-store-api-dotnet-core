using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.Installers;
using StoreAPI.Models;
using StoreAPI.Options;
using StoreAPI.Services;
using System;
using System.Linq;
using System.Text;

namespace StoreAPI
{
    public class Startup
    {
        readonly string AllowSpecificOrigins = "_allowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(JwtSettings), jwtSettings);

            services.AddSingleton(jwtSettings);

            services.InstallServicesInAssembly(Configuration);

            
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod();
                });
            });
            services.AddControllers();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true

                };
            }
            );
            services.AddScoped<IIdentityService, IdentityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
