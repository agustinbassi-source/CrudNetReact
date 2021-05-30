using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Authorize.DTO
{
    /// <summary>
    /// Usuario Validado en el token
    /// </summary>
    public class AuthorizeUserTokenResponseDTO
    {
        /// <summary>
        /// Id usuario
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email del usuario
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Tipo de usuario (external, internal, application, etc...)
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// Codigo de la net del pais del usuario
        /// </summary>
        public short UserCountryACode { get; set; }

        /// <summary>
        /// Lista de roles del usuario
        /// </summary>
        public List<AuthorizeUserRolesTokenResponseDTO> Roles { get; set; }
    }
}
