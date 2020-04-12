using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaizaiDate.Common.Settings;

namespace ZaizaiDate.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterJwtAuthentication(this IServiceCollection services, ISecretSettings secretSettings)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (secretSettings is null)
            {
                throw new ArgumentNullException(nameof(secretSettings));
            }

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
        }
    }
}
