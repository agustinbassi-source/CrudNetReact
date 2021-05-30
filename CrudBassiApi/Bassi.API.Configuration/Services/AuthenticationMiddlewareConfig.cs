using Bassi.API.Utilities.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Configuration.Services
{
    public static class AuthenticationMiddlewareConfig
    {
        public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddTransient<IClaimsTransformation, GemClaimsTransformer>();

            var key = Encoding.ASCII.GetBytes(configuration.GetSection("SecurityToken")["SecuryKey"]);
            var encrypKey = Encoding.ASCII.GetBytes(configuration.GetSection("SecurityToken")["EncrypCretentialKey"]);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    LifetimeValidator = LifetimeValidator,
                    ValidAudience = "GEMSAPI",
                    ValidIssuer = "GEMS",
                    ValidateIssuer = true,
                    ValidateAudience = true
                };
            });
        }

        private static bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}
