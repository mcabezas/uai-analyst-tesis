/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DBW.DBWrapper.Engine.impl
{
    public sealed class SqlServerDatabase : AbstractDatabase 
    {
        public SqlServerDatabase(string connectionString) : base(connectionString)
        {
        }

        protected override string PrepareInsertOutput(string query)
        {
            string[] strings = query.Split("VALUES");
            return strings[0] + " OUTPUT INSERTED.ID VALUES " + strings[1];
        }

        protected override DbParameter NewParameter(string aParameterName, DbType aDbType)
        {
            return new SqlParameter(aParameterName, aDbType);
        }

        protected override DbConnection ConnectionFactory(string aConnectionString)
        {
            return new SqlConnection(aConnectionString);
        }

        protected override DbCommand CommandFactory(string query)
        {
            return new SqlCommand(query, (SqlConnection)Connection);
        }
    }
}