using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Authenticate
{
    /// <summary>
    /// Configuracion para ejecutar los metodos del portal
    /// </summary>
    public class ServicePortalConfigDTO
    {
        /// <summary>
        /// Url del servicio del portal
        /// </summary>
        public string PortalServiceURI { get; set; }
        /// <summary>
        /// Usuario para acceder a los servicios del portal
        /// </summary>
        public string PortalServiceUser { get; set; }
        /// <summary>
        /// Clave del usuario del serivicio del portal
        /// </summary>
        public string PortalServiceUserPassword { get; set; }
    }
}
