using System;
using System.Collections.Generic;
using System.Text;
using Bassi.Portal.Service;
using Bassi.Portal.Service.DTO;
using Bassi.Authenticate;

namespace Bassi.Authenticate.Portal
{
    public class AuthenticatePortalManager
    {
        private string _urlServicePortal;
        private string _userPortal;
        private string _userPortalPassword;
        private TokenConfigDTO _tokenConfig;

        public AuthenticatePortalManager(ServicePortalConfigDTO servicePortalConfig, TokenConfigDTO tokenConfig)
        {
            _urlServicePortal = servicePortalConfig.PortalServiceURI;
            _userPortal = servicePortalConfig.PortalServiceUser;
            _userPortalPassword = servicePortalConfig.PortalServiceUserPassword;
            _tokenConfig = tokenConfig;
        }

        public AuthenticatePortalResponse Login(string user, string password, string module)
        {
            AuthenticatePortalResponse authenticatePortal = new AuthenticatePortalResponse();

            PortalService portal = new PortalService(_urlServicePortal);
            CredentialDTO credential = new CredentialDTO()
            {
                User = _userPortal,
                Password = _userPortalPassword
            };

            var loginUserPortal = portal.Login(user, password, module, credential);
            
            authenticatePortal.StatusCode = loginUserPortal.InternalStatus;
            authenticatePortal.Message = loginUserPortal.Message;

            if (loginUserPortal.IsValid)
            {
                Dictionary<string, string> claims = new Dictionary<string, string>();
                claims.Add("UserId", loginUserPortal.User.IUser.ToString());
                claims.Add("Module", module);
                try
                {
                    authenticatePortal.token = JwtTokenManager.CreateToken(_tokenConfig, claims);
                }
                catch(Exception ex) 
                {
                    authenticatePortal.StatusCode = 99;
                    authenticatePortal.Message = ex.Message;
                };
            }

            return authenticatePortal;
        }

        public object ValidateToken(string securityKey, string token)
        {
            return JwtTokenManager.ValidateToken(securityKey, token);
        }
    }
}
