using Bassi.ADO.Utilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Bassi.API.Configuration
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName);
        }

        public string Getkey(string keyName)
        {
            string data = _configuration[keyName];

            if (data == null)
            {
                data = GetDbConfiguration(keyName);
                //busco en la base de datos
            }

            return data;
        }

        public string GetSection(string sectionName, string keyName)
        {
            string data = _configuration.GetSection(sectionName)[keyName];

            if (data == null)
            {
                data = GetDbConfiguration(keyName);
            }

            return data;
        }

        public IConfigurationSection GetSection(string sectionName)
        {
            return _configuration.GetSection(sectionName);
        }

        private string GetDbConfiguration(string keyName)
        {
            SqlParameters parameters = new SqlParameters(1);
            parameters.Add("key", SqlDbType.VarChar, DbType.String, 50, keyName);

            string data = null;

            SqlStoreExcuter.Execute(GetConnectionString("bassi"), "[utility].[GetApplicationSettingByKey]", parameters, (reader) =>
            {
                if (reader.Read())
                {
                    data = reader.GetString("value");
                }
            });

            return data;
        }
    }
}
