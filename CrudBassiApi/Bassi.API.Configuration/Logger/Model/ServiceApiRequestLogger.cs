using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Configuration.Model
{
    class ServiceApiRequestLogger
    {
        public DateTime RequestITime { get; set; }
        public string RequestHeader { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public string Exception { get; set; }
    }
}
