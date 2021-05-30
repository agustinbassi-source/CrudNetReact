using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Bassi.CrudBassi.Repository.VOs
{
    [Table("Product", Schema = "dbo")]
    public class ProductParameterVO : BaseEntityVO
    {


       [MaxLength(50)]
          public string Name { get; set; }

    }
}
