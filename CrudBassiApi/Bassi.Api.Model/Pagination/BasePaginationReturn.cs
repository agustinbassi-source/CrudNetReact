using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Api.Model
{
    public class BasePaginationReturn<T> where T: class
    {
        public List<T> Results { get; set; }

        public int TotalResults { get; set; }
    }
}
