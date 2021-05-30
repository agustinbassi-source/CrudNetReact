using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Bassi.CrudBassi.Repository.VOs
{
    [Table("Receipt", Schema = "dbo")]
    public class ReceiptReturnVO : BaseEntityVO
    {


        public int? ClientId { get; set; }
        public ClientReturnVO Client  { get; set; }

    }
}
