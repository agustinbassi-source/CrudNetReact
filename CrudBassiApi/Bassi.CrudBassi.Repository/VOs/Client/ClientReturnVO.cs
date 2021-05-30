using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Bassi.CrudBassi.Repository.VOs
{
    [Table("Client", Schema = "dbo")]
    public class ClientReturnVO : BaseEntityVO
    {


       [MaxLength(50)]
          public string Name { get; set; }

    }
}
