using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace Bassi.ADO.Utilities
{
    public class SqlStoreExcuter : ISqlStoreExcuter
    {
        IDBContext _context;
        public SqlStoreExcuter(IDBContext context)
        {
            _context = context;
        }

        public ReturnVOT ExcuteSingle<ReturnVOT>(string storeName, SqlParameters parameters, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new()
        {
            return ExecuteSingle(_context.SqlConnection, storeName, parameters, mapReturn, _context.SqlTransaction);
        }

        public ReturnVOT ExcuteSingle<ReturnVOT>(string storeName, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new()
        {
            return ExcuteSingle(storeName, null, mapReturn);
        }

        public void ExcuteSingle<T>(string storeName, SqlParameters parameters, T data, Action<SqlDataReader, T> action)
        {
            Executer(_context.SqlConnection, storeName, parameters, (command) =>
            {
                var reader = command.ExecuteReader();
                try
                {
                    if (reader.Read())
                    {
                        action(reader, data);
                    }
                }
                catch { throw; }
                finally { reader.Close(); }
            });
        }

        public List<ReturnVOT> Execute<ReturnVOT>(string storeName, SqlParameters parameters, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new()
        {
            return Execute(_context.SqlConnection, storeName, parameters, mapReturn, _context.SqlTransaction);
        }

        public List<ReturnVOT> Execute<ReturnVOT>(string storeName, Func<SqlDataReader, ReturnVOT> mapReturn) where ReturnVOT : new()
        {
            return Execute(storeName, null, mapReturn);
        }

        public void ExecuteNonQuery(string storeName, Action<int> action)
        {
            ExecuteNonQuery(_context.SqlConnection, storeName, null, action, _context.SqlTransaction);
        }

        public void ExecuteNonQuery(string storeName, SqlParameters parameters, Action<int> action)
        {
            ExecuteNonQuery(_context.SqlConnection, storeName, parameters, action, _context.SqlTransaction);
        }

        public static ReturnVOT ExecuteSingle<ReturnVOT>(SqlConnection connection, string storeName, SqlParameters parameters, Func<SqlDataReader, ReturnVOT> mapReturn, SqlTransaction transaction = null) where ReturnVOT : new()
        {
            return ExecuteReader(connection, storeName, parameters,
                (reader) =>
                {
                    ReturnVOT returnVOT = default(ReturnVOT);

                    if (reader.Read())
                    {
                        returnVOT = mapReturn(reader);
                    }

                    return returnVOT;
                }
                , transaction);
        }

        public static List<ReturnVOT> Execute<ReturnVOT>(SqlConnection connection, string storeName, SqlParameters parameters, Func<SqlDataReader, ReturnVOT> mapReturn, SqlTransaction transaction = null) where ReturnVOT : new()
        {
            return ExecuteReader(connection, storeName, parameters,
                (reader) =>
                {
                    List<ReturnVOT> returnVOT = new List<ReturnVOT>();

                    while (reader.Read())
                    {
                        returnVOT.Add(mapReturn(reader));
                    }

                    return returnVOT;
                }
                , transaction);
        }

        public void Execute<T>(string storeName, T data, Action<SqlDataReader, T> action, SqlTransaction transaction = null)
        {
            Executer(_context.SqlConnection, storeName, new SqlParameters(), (command) =>
            {
                var reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        action(reader, data);
                    }
                }
                catch { throw; }
                finally { reader.Close(); }
            }, transaction);

        }

        public static void Execute<T>(string connectionString, string storeName, T data, Action<SqlDataReader, T> action, SqlTransaction transaction = null)
        {
            try
            {
                Execute(connectionString, storeName, null, data, action, transaction);
            }
            catch { throw; }
        }

        public static void Execute<T>(string connectionString, string storeName, SqlParameters parameters, T data, Action<SqlDataReader, T> action, SqlTransaction transaction = null)
        {
            try
            {
                Executer(connectionString, storeName, parameters, (command) =>
                {
                    var reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            action(reader, data);
                        }
                    }
                    catch { throw; }
                    finally { reader.Close(); }
                }, transaction);
            }
            catch { throw; }

        }

        public static void Execute(string connectionString, string storeName, SqlParameters parameters, Action<SqlDataReader> action, SqlTransaction transaction = null)
        {
            try
            {
                Executer(connectionString, storeName, parameters, (command) =>
                {
                    var reader = command.ExecuteReader();
                    try
                    {
                        action(reader);
                    }
                    catch { throw; }
                    finally { reader.Close(); }
                }, transaction);
            }
            catch { throw; }
        }

        /// <summary>
        /// Ejecuta un comando sql 
        /// </summary>
        /// <param name="connectionString">string de conexion</param>
        /// <param name="storeName">store procedure</param>
        /// <param name="parameters">parametros que recice el store</param>
        /// <param name="action">ejecuta la accion pos ejecucion del query, devuelve la cantidad de registros afectados</param>
        /// <param name="transaction">Opcional</param>
        public static void ExecuteNonQuery(string connectionString, string storeName, SqlParameters parameters, Action<int> action, SqlTransaction transaction = null)
        {
            Executer(connectionString, storeName, parameters, (command) =>
            {
                try
                {
                    var records = command.ExecuteNonQuery();
                    action(records);
                }
                catch { throw; }
            }, transaction);

        }

        /// <summary>
        /// Ejecuta un comando sql
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="storeName"></param>
        /// <param name="parameters"></param>
        /// <param name="action"></param>
        /// <param name="transaction"></param>
        public static void ExecuteNonQuery(SqlConnection sqlConnection, string storeName, SqlParameters parameters, Action<int> action, SqlTransaction transaction = null)
        {
            try
            {
                Executer(sqlConnection, storeName, parameters, (command) =>
                {
                    try
                    {
                        var records = command.ExecuteNonQuery();
                        action(records);
                    }
                    catch { }
                }, transaction);
            }
            catch { }
        }

        private static ReturnT ExecuteReader<ReturnT>(SqlConnection connection, string storeName, SqlParameters parameters, Func<SqlDataReader, ReturnT> functionReturn, SqlTransaction transaction = null) where ReturnT : new()
        {
            ReturnT returnT = default;

            returnT = Executer(connection, storeName, parameters, (command) =>
            {
                ReturnT returnVO = default;

                var reader = command.ExecuteReader();
                try
                {
                    returnVO = functionReturn(reader);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    reader.Close();
                }

                return returnVO;

            }, transaction);

            return returnT;
        }

        private static ReturnT Executer<ReturnT>(SqlConnection connection, string storeName, SqlParameters parameters, Func<SqlCommand, ReturnT> functionReturn, SqlTransaction transaction = null) where ReturnT : new()
        {
            ReturnT returnT;

            var command = new SqlCommand(storeName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            if (parameters != null && parameters.Count > 0)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            returnT = functionReturn(command);

            return returnT;
        }

        private static void Executer(SqlConnection connection, string storeName, SqlParameters parameters, Action<SqlCommand> functionAction, SqlTransaction transaction = null)
        {
            var command = new SqlCommand(storeName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            if (parameters != null && parameters.Count > 0)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }

            functionAction(command);

        }

        private static void Executer(string connectionString, string storeName, SqlParameters parameters, Action<SqlCommand> functionAction, SqlTransaction transaction = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = new SqlCommand(storeName, connection);
                command.CommandType = CommandType.StoredProcedure;

                if (transaction != null)
                {
                    command.Transaction = transaction;
                }

                if (parameters != null && parameters.Count > 0)
                {
                    command.Parameters.AddRange(parameters.ToArray());
                }

                functionAction(command);
            }
        }
    }
}
