using System;
using System.Collections.Generic;
using System.Text;
using Bassi.ADO.Utilities;
using System.Data;
using Bassi.API.Configuration;

namespace Bassi.API.Configuration.Logger
{
    public class ServiceLoggerRepository : IServiceLoggerRepository
    {
        readonly IAppSettings _configuration;

        public ServiceLoggerRepository(IAppSettings configuration)
        {
            _configuration = configuration;
        }

        public Guid RegisterLogger(Guid traceId, DateTime requestUTCTime, Guid applicationId, string serviceURL, string additionalInformation,
            string method, string request, string response, string exception)
        {
            SqlParameters parameters = new SqlParameters(8);

            parameters.Add("LogId", SqlDbType.UniqueIdentifier, DbType.Guid, traceId);
            parameters.Add("ReqTime", SqlDbType.DateTime, DbType.DateTime, requestUTCTime);
            parameters.Add("ApplicationId", SqlDbType.UniqueIdentifier, DbType.Guid, applicationId);
            parameters.Add("ServiceUrl", SqlDbType.VarChar, DbType.String, 500, serviceURL);
            parameters.Add("AdditionalInformations", SqlDbType.NVarChar, DbType.String, additionalInformation);
            parameters.Add("Method", SqlDbType.VarChar, DbType.String, 50, method);
            parameters.Add("Request", SqlDbType.NVarChar, DbType.String, request);
            if (response != null)
            {
                parameters.Add("Response", SqlDbType.NVarChar, DbType.String, response);
            }
            if(exception != null)
            {
                parameters.Add("Error", SqlDbType.NVarChar, DbType.String, exception);
            }

            int records;

            SqlStoreExcuter.ExecuteNonQuery(_configuration.GetConnectionString("utility_logger"), "[dbo].[LogErrorBasic]", parameters, (result) =>
            {
                records = result;
            });

            return traceId;
        }
    }
}
