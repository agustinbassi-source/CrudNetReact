using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Authorize.DTO
{
    /// <summary>
    /// Repusta del servicio para validar a un usuario
    /// </summary>
    public class AuthorizationServiceResponseDTO
    {
        public int internalStatusCode { get; set; }

        public string Message { get; set; }

        public AuthorizeUserTokenResponseDTO Data { get; set; }
    }
}
