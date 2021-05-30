using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;

namespace Bassi.Gem.Utilities.VOs
{
    public class GetAllRegistroServidorReturnVO
    {
        [Key]
        public int Id { get; set; }
        public int AplicacionId { get; set; }
        [MaxLength(20)]
        public string Ip { get; set; }
        public int Puerto { get; set; }
        public DateTime FechaRegistro { get; set; }
        [MaxLength(500)]
        public string Dns { get; set; }
        public bool Activo { get; set; }
        public bool SSLActivo { get; set; }
    }
}
