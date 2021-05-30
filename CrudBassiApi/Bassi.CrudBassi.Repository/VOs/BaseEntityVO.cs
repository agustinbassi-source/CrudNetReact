using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bassi.CrudBassi.Repository.VOs
{
    public class BaseEntityVO
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime ITime { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
        [Required]
        public int IdStatus { get; set; }
    }

    public enum EntityStatus : int
    {
        New = 25000,
        Modified = 25001,
        Deleted = 25002
    }
}
