using Bassi.API.Authorize.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bassi.API.Authorize.Interface
{
    public interface ICustomAuthenticate
    {
        /// <summary>
        /// Autentica 
        /// </summary>
        /// <param name="userName">usuario</param>
        /// <param name="password">password</param>
        /// <param name="urlAuthenticate">Url del servicio de autenticacion</param>
        /// <returns></returns>

        AuthenticateServiceResponseDTO Authenticate(string userName, string password);

        /// <summary>
        /// Valida un token y devuelve los datos del usuario
        /// </summary>
        /// <param name="authorizationToken">token</param>
        /// <param name="urlAuthenticate">url para validar el token</param>
        /// <returns></returns>
        AuthorizationServiceResponseDTO ValidateToken(string authorizationToken);
    }
}
