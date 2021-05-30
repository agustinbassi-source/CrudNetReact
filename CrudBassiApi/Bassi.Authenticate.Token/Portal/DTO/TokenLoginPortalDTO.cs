using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Authenticate
{
    public class TokenLoginPortalDTO
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public int Expire { get; set; }
    }
}
