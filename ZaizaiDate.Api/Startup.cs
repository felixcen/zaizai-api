using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ZaizaiDate.Business.Service;
using ZaizaiDate.Common.Settings;
using ZaizaiDate.Database.DatabaseContext;

namespace ZaizaiDate.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        private readonly string AllowAllOriginsPolicy = "_allowAllOriginPolicy";
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {

            _env = env ?? throw new ArgumentNullException(nameof(env));

            var builder = new ConfigurationBuilder()
                     .SetBasePath(_env.ContentRootPath)
                     .AddJsonFile("appsettings.json",
                                  optional: false,
                                    reloadOnChange: true).AddEnvironmentVariables();

            if (_env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SecretSettings secretSettings;
            if (_env.IsDevelopment())
            {
                secretSettings = Configuration.GetSection("AppSecrets").Get<SecretSettings>();
            }
            else
            {
                // can be read from somewhere else such as docker secret or aws secret 
                secretSettings = new SecretSettings();
            }
            services.AddMvc();

            services.AddSingleton<ISecretSettings>(secretSettings);
            services.AddDbContext<ZaiZaiDateDbContext>(a =>
                a.UseSqlite(secretSettings.DatabaseConnectionString,
                    option => option.MigrationsAssembly("ZaizaiDate.Database.Migrations")));

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddCors(options => options.AddPolicy(name: AllowAllOriginsPolicy,
                              builder =>
                              {
                                  builder.AllowAnyOrigin().AllowAnyHeader()
                                                  .AllowAnyMethod();
                              }));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretSettings.JwtSigningKey)),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ClockSkew = TimeSpan.FromMinutes(5)
                        };
                    });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(AllowAllOriginsPolicy);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(AllowAllOriginsPolicy);
            });

        }
    }
}
