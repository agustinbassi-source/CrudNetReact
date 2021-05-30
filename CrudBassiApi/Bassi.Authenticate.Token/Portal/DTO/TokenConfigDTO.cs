using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Authenticate
{
    /// <summary>
    /// Configuracion para crear un token
    /// </summary>
    public class TokenConfigDTO
    {
        /// <summary>
        /// Clave de seguridad del token
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// Clave para encriptar los datos del token
        /// </summary>
        public string SecurityEncrypKey { get; set; }
        /// <summary>
        /// tiempo en minutos que expira el token default 1 dia (24 hs)
        /// </summary>
        public int Expire { get; set; } = 24 * 60;
    }
}
