using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace Bassi.ADO.Utilities
{
    public class SqlParameters
    {
        private List<SqlParameter> _sqlParameters;

        public SqlParameters()
        {
            _sqlParameters = new List<SqlParameter>();
        }


        public SqlParameters(int capacity)
        {
            _sqlParameters = new List<SqlParameter>(capacity);
        }

        public int Count { get { return _sqlParameters.Count; } }

      

        public void Add(string name, SqlDbType sqlDbType, DbType dbType, object value)
        {
            Add(name, sqlDbType, dbType, 0, ParameterDirection.Input, value, null);
        }

        public void Add(string name, SqlDbType sqlDbType, DbType dbType, ParameterDirection direction, object value)
        {
            Add(name, sqlDbType, dbType, 0, direction, value, null);
        }

        public void Add(string name, SqlDbType sqlDbType, DbType dbType, int size, object value)
        {
            Add(name, sqlDbType, dbType, size, ParameterDirection.Input, value, null);
        }

        public void Add(string name, SqlDbType sqlDbType, DbType dbType, int size, ParameterDirection direction, object value, string TypeName)
        {
            if (!Exsist(name))
            {
                _sqlParameters.Add(Parameter(name, sqlDbType, dbType, size, direction, value, TypeName));
            }
            else
            {
                throw new DuplicateNameException($"Duplicate parameter {name}");
            }
        }

        public SqlParameter Last
        {
            get { return _sqlParameters.Last(); }
        }

        public SqlParameter GetParameterByName(string name)
        {
            name = name.Contains("@") ? name : $"@{name}";

            return _sqlParameters.Find(f => f.ParameterName.ToLower() == name.ToLower());
        }

        public SqlParameter[] ToArray()
        {
            return _sqlParameters.ToArray();
        }

        public bool Exsist(string paramenterName)
        {
            paramenterName = paramenterName.Contains("@") ? paramenterName : $"@{paramenterName}";
            return _sqlParameters.Exists(f => f.ParameterName.ToLower() == paramenterName.ToLower());
        }

        //private SqlParameter Parameter(string name, SqlDbType sqlDbType, DbType dbType, object value)
        //{

        //    return Parameter(name, sqlDbType, dbType, 0, value);
        //}

        //private SqlParameter Parameter(string name, SqlDbType sqlDbType, DbType dbType, int size, object value)
        //{
        //    return Parameter(name, sqlDbType, dbType, 0, ParameterDirection.Input, value);
        //}

        private SqlParameter Parameter(string name, SqlDbType sqlDbType, DbType dbType, int size, ParameterDirection direction, object value, string TypeName)
        {

            SqlParameter parameter = new SqlParameter()
            {
                ParameterName = name.Contains('@') ? name : $"@{name}",
                SqlDbType = sqlDbType,
                DbType = dbType,
                Value = ObjectToSqlValue(sqlDbType, value),
                Direction = direction,
                TypeName = TypeName
            };

            if (size != 0)
            {
                parameter.Size = size;
            }
            return parameter;

        }

        private object ObjectToSqlValue(SqlDbType type, object value) => type switch
        {
            SqlDbType.VarChar => new SqlString((string)value),
            SqlDbType.NVarChar => new SqlString((string)value),
            SqlDbType.SmallInt => new SqlInt16((short)value),
            SqlDbType.Int => new SqlInt32((int)value),
            SqlDbType.BigInt => new SqlInt64((long)value),
            SqlDbType.Bit => new SqlBoolean((bool)value),
            SqlDbType.Char => new SqlChars((string)value),
            SqlDbType.Date => new SqlDateTime((DateTime)value),
            SqlDbType.DateTime => new SqlDateTime((DateTime)value),
            SqlDbType.Decimal => new SqlDecimal((decimal)value),
            SqlDbType.UniqueIdentifier => new SqlGuid((Guid)value),
            _ => value,
        };
    }
}
