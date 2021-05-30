using Bassi.API.Authorize.DTO;
using Bassi.API.Authorize.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Bassi.API.Authorize
{

    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    /// <summary>
    /// Custom autorize
    /// </summary>
    public class CustomAuthorizationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly ICustomAuthenticate _customAuthenticate;

        public CustomAuthorizationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ICustomAuthenticate authenticate) : base(options, logger, encoder, clock)
        {
            _customAuthenticate = authenticate;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            string token = authorizationHeader.Substring("bearer".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

            try
            {
                return Task.FromResult(validateToken(token));
            }
            catch (Exception ex)
            {
                return Task.FromResult(AuthenticateResult.Fail(ex.Message));
            }
        }

        private AuthenticateResult validateToken(string token)
        {
            var validatedToken = !string.IsNullOrEmpty(token);
            if (!validatedToken)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
            
            var userValidate = _customAuthenticate.ValidateToken(token);
            
            if(userValidate == null || 
                userValidate.internalStatusCode != 0 || 
                userValidate.Data == null ||
                userValidate.Data.UserId == Guid.Empty)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            var claims = new List<Claim>
                {
                    new Claim("IUser", userValidate.Data.UserId.ToString()),
                    new Claim("Name", userValidate.Data.UserName),
                    new Claim("Email", userValidate.Data.Email),
                    new Claim("UserType", userValidate.Data.UserType.ToString() ),
                    new Claim("ACNetCountryUser", userValidate.Data.UserCountryACode.ToString()),
                    new Claim("Roles", GetListRoles(userValidate.Data.Roles)),
                };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            
            ClaimsPrincipal claims1 = new ClaimsPrincipal(identity);
            
            var principal = new UserGemsPrincipal(claims1);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }

        private string GetListRoles(List<AuthorizeUserRolesTokenResponseDTO> roles)
        {
            StringBuilder builder = new StringBuilder();

            if(roles != null)
            {
                roles.ForEach(role => builder.AppendLine(role.Name));
            }

            return builder.ToString();
        }
    }
}
