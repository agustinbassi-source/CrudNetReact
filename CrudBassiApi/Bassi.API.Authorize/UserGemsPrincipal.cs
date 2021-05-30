using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Bassi.API.Authorize
{
    public class UserGemsPrincipal : ClaimsPrincipal
    {

        public UserGemsPrincipal(IPrincipal principal) : base(principal)
        {
        }

        public Guid UserId
        {
            get
            {
                Claim UserIdClaim = FindFirst("IUser");
                return UserIdClaim != null ? Guid.Parse(UserIdClaim.Value) : Guid.Empty;
            }
        }

        public string Name
        {
            get
            {
                Claim UserIdClaim = FindFirst("Name");
                return UserIdClaim != null ? UserIdClaim.Value : "";
            }
        }

        public string Email
        {
            get
            {
                Claim UserIdClaim = FindFirst("Email");
                return UserIdClaim != null ? UserIdClaim.Value : "";
            }
        }

        public int UserType
        {
            get
            {
                Claim UserIdClaim = FindFirst("UserType");
                return UserIdClaim != null ? int.Parse(UserIdClaim.Value) : 0;
            }
        }

        public string Roles
        {
            get
            {
                Claim UserIdClaim = FindFirst("Roles");
                return UserIdClaim != null ? UserIdClaim.Value : "";
            }
        }

        public override bool IsInRole(string role)
        {
            bool _isValid = this.UserType == 1;
            
            if(_isValid == false)
            {
                _isValid = this.Roles.ToLower().Contains(role.ToLower());
            }

            return _isValid;

        }
    }
}
