using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Authorize.DTO
{
    /// <summary>
    /// Role
    /// </summary>
    public class AuthorizeUserRolesTokenResponseDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public string Name { get; set; }
    }
}
