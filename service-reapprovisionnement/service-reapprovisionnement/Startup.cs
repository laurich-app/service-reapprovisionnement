using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using Consul;
using Securities;
using Microsoft.AspNetCore.Http;
using Securities;
using services;
using Dtos;
using Extensions;
using service_reapprovisionnement.Repository;

namespace service_reapprovisionnement
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
            services.AddSingleton<DatabaseService>();
            services.AddSingleton<WeatherService>();
            services.AddSingleton<FournisseurRepository>();
            services.AddSingleton<FournisseurService>();
            services.AddControllers();
            services.AddOptions();
            services.Configure<ConsulOptions>(Configuration);
            services.AddTransient<ConsulService>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<ClaimsHelper>(); // Enregistrer ClaimsHelper comme un service Scoped
            services.AddCors(options =>
            {
                options.AddPolicy("Cors",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200");
                        builder.WithExposedHeaders("X-Get-Header");
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowCredentials();
                    });
            });
            services.AddAuthorization();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "self",
                    // ValidAudience = "your-audience",
                    IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                    {
                        return GetSecurityKeys();
                    }
                };

            });

        }

        private IEnumerable<SecurityKey> GetSecurityKeys()
        {
            var rsa = JWTDecoder.GetInstance.GetSecurityKey();
            return new List<SecurityKey> { rsa };
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("Cors");

            app.UseAuthentication();
            app.UseAuthorization();

            app.ConsulRegister();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });

        }
    }
}
