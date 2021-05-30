using Bassi.ADO.Utilities;
using Bassi.API.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Bassi.CrudBassi.Repository
{
    public class DBContext : IDBContext, IDisposable
    {

        public DBContext(IAppSettings configuration, string connectionString)
        {
            _configuration = configuration;
            SqlConnection = new SqlConnection(configuration.GetConnectionString(connectionString));
            SqlConnection.Open();
        }

        IAppSettings _configuration;
        public SqlConnection SqlConnection { get; }

        public SqlTransaction SqlTransaction { get; set; }

        public void Dispose()
        {
            SqlConnection.Close();
            SqlConnection.Dispose();
        }
    }
}
