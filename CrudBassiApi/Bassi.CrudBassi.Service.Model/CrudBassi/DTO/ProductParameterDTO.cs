using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Bassi.CrudBassi.DTO
{
    public class ProductParameterDTO
    {
        [Key]
        public int Id { get; set; }
       [MaxLength(50)]
          public string Name { get; set; }

    }
}
