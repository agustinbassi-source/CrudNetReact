using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Api.Model
{
    public class BasePaginationParameter
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string OrderBy { get; set; }
        public bool Ascending { get; set; }
    }
}
