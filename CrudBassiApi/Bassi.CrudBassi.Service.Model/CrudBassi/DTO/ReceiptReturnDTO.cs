using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Bassi.CrudBassi.DTO
{
    public class ReceiptReturnDTO
    {
        [Key]
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public ClientReturnDTO Client  { get; set; }

        public List<ReceiptDetailReturnDTO> ReceiptDetail { get; set; }
    }
}
