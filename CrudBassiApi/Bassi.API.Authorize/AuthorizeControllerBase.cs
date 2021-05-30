using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Authorize
{
    [Authorize]
    public class AuthorizeControllerBase : ControllerBase
    {
        UserGemsPrincipal _user;
        public new UserGemsPrincipal User
        {
            get
            {
                if (_user == null)
                {
                    _user = HttpContext.User as UserGemsPrincipal;
                }
                return _user;
            }
            set
            {
                _user = value;
            }
        }

    }
}
