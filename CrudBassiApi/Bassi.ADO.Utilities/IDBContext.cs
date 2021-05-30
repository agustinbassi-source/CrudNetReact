using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Bassi.ADO.Utilities
{
    public interface IDBContext
    {
        SqlConnection SqlConnection { get; }

        SqlTransaction SqlTransaction { get; set; }

    }
}
