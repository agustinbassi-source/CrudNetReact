using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Configuration.Model
{
    public class ServiceApiResponseException
    {
        public int InternalStatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
