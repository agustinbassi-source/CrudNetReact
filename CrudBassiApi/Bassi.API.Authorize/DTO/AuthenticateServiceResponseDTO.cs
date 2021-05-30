using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Authorize.DTO
{
    /// <summary>
    /// Respuesta del servicio de autenticacion
    /// </summary>
    public class AuthenticateServiceResponseDTO
    {
        /// <summary>
        /// codigo de estado de la respuesta
        /// </summary>
        public int InternalStatusCode { get; set; }

        /// <summary>
        /// mensaje de al respuesta
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// token devuelto por el servicio
        /// </summary>
        public AuthenticateTokenServiceResponseDTO Data { get; set; }

    }
}
