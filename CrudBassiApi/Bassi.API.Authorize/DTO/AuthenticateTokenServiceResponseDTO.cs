using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Authorize.DTO
{
    public class AuthenticateTokenServiceResponseDTO
    {
        /// <summary>
        /// Token de autenticacion
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Tipo de token
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// Tiempo de expiracion del token
        /// </summary>
        public int Expire { get; set; }

    }
}
