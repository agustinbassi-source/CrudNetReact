using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Portal.Service.DTO
{
    public class LoginUserPortalDTO
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public int InternalStatus { get; set; }
        public UserPortalDTO User { get; set; }
    }
}
