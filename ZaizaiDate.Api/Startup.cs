using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
using ZaizaiDate.Api.Extensions;
using ZaizaiDate.Business.Service;
using ZaizaiDate.Common.Exceptions;
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
            
            services.AddSingleton<ISecretSettings>(secretSettings);
            services.AddDbContext<ZaiZaiDateDbContext>(a =>
                a.UseSqlite(secretSettings.DatabaseConnectionString,
                    option => option.MigrationsAssembly("ZaizaiDate.Database.Migrations")));

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserManagementService, UserManagementService>();

            services.AddCors(options => options.AddPolicy(name: AllowAllOriginsPolicy,
                              builder =>
                              {
                                  builder.AllowAnyOrigin().AllowAnyHeader()
                                                  .AllowAnyMethod();
                              }));

            services.RegisterJwtAuthentication(secretSettings);

            services.AddControllers();
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
                app.UseExceptionHandler( builder=> {
                    builder.Run(async context => {
                       
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        
                        // TODO: add logger
                        Console.WriteLine(error?.Error);

                        if (error?.Error is FileNotFoundException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            context.Response.AddApplicationError(error.Error.Message);
                            
                            await context.Response.WriteAsync("File not found").ConfigureAwait(false);

                        }else if (error?.Error is ServerHandledException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.AddApplicationError("Unexpected server error");
                            await context.Response.WriteAsync("Unexpected server error").ConfigureAwait(false);
                        }
                    });
                });
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
