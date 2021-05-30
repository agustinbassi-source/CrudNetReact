using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Portal.Service.DTO
{
    public class UserProfiles
    {
        public int PortalId { get; set; }
        public Guid UserId { get; set; }
        public string Module { get; set; }
        public IList<UserModuleProfileDTO> Profiles { get; set; } 
    }
}
