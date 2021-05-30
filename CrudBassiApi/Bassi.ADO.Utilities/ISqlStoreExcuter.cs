using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Bassi.ADO.Utilities
{
    public interface ISqlStoreExcuter
    {
        ReturnVOT ExcuteSingle<ReturnVOT>(string storeName, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new();
        ReturnVOT ExcuteSingle<ReturnVOT>(string storeName, SqlParameters parameters, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new();
        void ExcuteSingle<T>(string storeName, SqlParameters parameters, T data, Action<SqlDataReader, T> action);
        List<ReturnVOT> Execute<ReturnVOT>(string storeName, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new();
        List<ReturnVOT> Execute<ReturnVOT>(string storeName, SqlParameters parameters, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new();
        void Execute<T>(string storeName, T data, Action<SqlDataReader, T> action, SqlTransaction transaction = null);
        void ExecuteNonQuery(string storeName, Action<int> action);
        void ExecuteNonQuery(string storeName, SqlParameters parameters, Action<int> action);
    }
}