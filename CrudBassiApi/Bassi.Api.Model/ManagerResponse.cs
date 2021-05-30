using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.Api.Model
{
    public class ManagerResponse<T>
    {
        public int InternalStatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
