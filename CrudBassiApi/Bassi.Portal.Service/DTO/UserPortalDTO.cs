using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Portal.Service.DTO
{
    public class UserPortalDTO
    {
        public Guid IUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public short AcCountry { get; set; }
        public List<ProfileModulePortalDTO> Profiles { get; set; }
    }
}
