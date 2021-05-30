using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Bassi.Authenticate
{
    public static class JwtTokenManager
    {
        public static TokenLoginPortalDTO CreateToken(TokenConfigDTO tokenConfig, Dictionary<string, string> tokenData)
        {
            // Leemos el secret_key desde nuestro appseting
            var secretKey = tokenConfig.SecurityKey;
            var key = Encoding.ASCII.GetBytes(secretKey);
            
            // Creamos los claims (pertenencias, características) del usuario
            var claims = new List<Claim>(tokenData.Count);

            //claims.Add(new Claim(ClaimTypes.Name, "GENS-RI"));

            foreach(KeyValuePair<string, string> data in tokenData)
            {
                claims.Add(new Claim(data.Key, data.Value));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = "GEMSAPI",
                Issuer = "GEMS",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            if(false && !string.IsNullOrEmpty(tokenConfig.SecurityEncrypKey))
            {
                tokenDescriptor.EncryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig.SecurityEncrypKey)), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);
            }

            if(tokenConfig.Expire > 0)
            {
                tokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(tokenConfig.Expire);
            }
            
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(claims),
            //    Expires = DateTime.UtcNow.AddDays(1),
            //    Audience = "GEMSAPI",
            //    Issuer = "GEMS",
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};


            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            TokenLoginPortalDTO tokenCreate = new TokenLoginPortalDTO()
            {
                AccessToken = tokenHandler.WriteToken(createdToken),
                Expire = tokenConfig.Expire
            };

            return tokenCreate;
        }

        /// <summary>
        /// Validate JWT token is correct
        /// </summary>
        /// <param name="token">token to validate</param>
        /// <returns>ClaimPrincipal of the user logged</returns>
        public static ClaimsPrincipal ValidateToken(string securityKey, string token)
        {
            var now = DateTime.UtcNow;
            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes(securityKey));
            var securityEncriptionKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(securityKey));

            SecurityToken securityToken;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidAudience = "GEMSAPI",
                ValidIssuer = "GEMS",
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                LifetimeValidator = LifetimeValidator,
                IssuerSigningKey = key
            };

            return handler.ValidateToken(token, validationParameters, out securityToken);
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
