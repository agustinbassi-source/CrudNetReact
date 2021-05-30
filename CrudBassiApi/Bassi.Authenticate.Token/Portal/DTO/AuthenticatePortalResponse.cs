using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Authenticate
{
    public class AuthenticatePortalResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TokenLoginPortalDTO token { get; set; }
    }
}
