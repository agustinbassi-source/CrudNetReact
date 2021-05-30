using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Bassi.CrudBassi.Repository.VOs
{
    [Table("ReceiptDetail", Schema = "dbo")]
    public class ReceiptDetailReturnVO : BaseEntityVO
    {

        public int ReceiptId { get; set; }

        public int? ProductId { get; set; }
        public ProductReturnVO Product  { get; set; }

          public int? Amount { get; set; }

    }
}
