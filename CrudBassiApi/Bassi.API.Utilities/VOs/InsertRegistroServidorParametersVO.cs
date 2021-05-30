using System;
using System.ComponentModel.DataAnnotations;

namespace Bassi.Gem.Utilities.VOs
{
    public class InsertRegistroServidorParametersVO
    {
        public int AplicacionId { get; set; }
        public string Ip { get; set; }
        public int Puerto { get; set; }
        public string Dns { get; set; }
        public bool Activo { get; set; }
        public bool SSLActivo { get; set; }
    }
}
