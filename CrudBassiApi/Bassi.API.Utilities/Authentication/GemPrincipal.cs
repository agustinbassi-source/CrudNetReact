using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Bassi.Portal.Service;
using Bassi.Portal.Service.DTO;
using Microsoft.Extensions.Configuration;

namespace Bassi.API.Utilities.Authentication
{
    public class GemPrincipal : ClaimsPrincipal
    {
        private UserProfiles profiles = new UserProfiles();

        public GemPrincipal(IPrincipal principal, IConfiguration configuration) : base (principal)
        {
            PortalService portal = new PortalService(configuration.GetSection("PortalService")["Url"]);
            CredentialDTO credential = new CredentialDTO()
            {
                User = configuration.GetSection("PortalService")["UserService"],
                Password = configuration.GetSection("PortalService")["UserServicePassword"]
            };

            profiles = portal.UserProfiles(this.UserId, this.Module, credential);
        }

        public Guid UserId
        {
            get
            {
                Claim UserIdClaim = FindFirst("UserId");

                return UserIdClaim != null ? Guid.Parse(UserIdClaim.Value) : Guid.Empty;
            }
        }

        public string Module
        {
            get
            {
                return FindFirst("Module")?.Value;
            }

        }

        public override bool IsInRole(string role)
        {
            //TODO: Esta en true hasta que se defina el modulo y los roles para este webapi, despues ponerlo en false
            bool IsValid = false;
            int index = 0;

            while(!IsValid && index < profiles.Profiles.Count)
            {
                IsValid = profiles.Profiles.ElementAt(index).Name.ToLower() == role.ToLower();
                index++;
            }

            return IsValid;
        }
    }
}
