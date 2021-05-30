using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bassi.API.Utilities.Authentication
{
    public class GemClaimsTransformer : IClaimsTransformation
    {
        private readonly IConfiguration _configuration;

        public GemClaimsTransformer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var gemPrincipal = new GemPrincipal(principal, _configuration);
            return Task.FromResult(gemPrincipal as ClaimsPrincipal);
        }
    }
}
