using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Bassi.CrudBassi.DTO
{
    public class ReceiptDetailReturnDTO
    {
        [Key]
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public int? ProductId { get; set; }
        public ProductReturnDTO Product  { get; set; }

          public int? Amount { get; set; }

    }
}
