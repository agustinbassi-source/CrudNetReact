using Bassi.API.Utilities.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Utilities
{
    public class DefaultControllerBase : ControllerBase
    {
        GemPrincipal _user;
        public new GemPrincipal User
        {
            get
            {
                if (_user == null)
                {
                    _user = HttpContext.User as GemPrincipal;
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
