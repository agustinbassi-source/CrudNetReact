using System;
using System.Collections.Generic;
using System.Text;

namespace Bassi.API.Configuration.Logger
{
    public interface IServiceLoggerRepository
    {
        Guid RegisterLogger(Guid traceId, DateTime requestUTCTime, Guid applicationId, string serviceURL, string additionalInformation,
            string method, string request, string response, string exception);
    }
}
