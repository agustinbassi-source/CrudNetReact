using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Portal.Service.DTO
{
    /// <summary>
    /// Credenciales para la ejecucion del metodo o funcion del portal
    /// </summary>
    public class CredentialDTO
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
