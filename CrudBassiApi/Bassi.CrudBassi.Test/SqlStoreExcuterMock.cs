using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Bassi.ADO.Utilities
{
    public class SqlStoreExcuterMock : ISqlStoreExcuter
    {
        public ReturnVOT ExcuteSingle<ReturnVOT>(string storeName, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new()
        {
            return new ReturnVOT();
        }

        public ReturnVOT ExcuteSingle<ReturnVOT>(string storeName, SqlParameters parameters, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new()
        {
            foreach (var item in parameters.ToArray())
            {
                if (item.Direction == System.Data.ParameterDirection.Output)
                {
                    item.Value = 1;
                }
            }

            return new ReturnVOT();
        }

        public void ExcuteSingle<T>(string storeName, SqlParameters parameters, T data, Action<SqlDataReader, T> action)
        {
            foreach (var item in parameters.ToArray())
            {
                if (item.Direction == System.Data.ParameterDirection.Output)
                {
                    item.Value = 1;
                }
            }
        }

        public List<ReturnVOT> Execute<ReturnVOT>(string storeName, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new()
        {
            List<ReturnVOT> response = new List<ReturnVOT>();

            response.Add(new ReturnVOT());

            response.Add(new ReturnVOT());

            return response;
        }

        public List<ReturnVOT> Execute<ReturnVOT>(string storeName, SqlParameters parameters, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new()
        {

            foreach (var item in parameters.ToArray())
            {
                if (item.Direction == System.Data.ParameterDirection.Output)
                {
                    item.Value = 1;
                }
            }

            List<ReturnVOT> response = new List<ReturnVOT>();

            if (storeName.IndexOf("Select]") > 0 && parameters.GetParameterByName("Id").Value.ToString() == "2")
                return response;

            response.Add(new ReturnVOT());

            response.Add(new ReturnVOT());


            return response;
        }

        public void Execute<T>(string storeName, T data, Action<SqlDataReader, T> action, SqlTransaction transaction = null)
        {
            
        }

        public void ExecuteNonQuery(string storeName, Action<int> action)
        {
           
        }

        public void ExecuteNonQuery(string storeName, SqlParameters parameters, Action<int> action)
        {
            foreach (var item in parameters.ToArray())
            {
                if (item.Direction ==  System.Data.ParameterDirection.Output)
                {
                    item.Value = 1;
                }
            }
        }
    }
}
