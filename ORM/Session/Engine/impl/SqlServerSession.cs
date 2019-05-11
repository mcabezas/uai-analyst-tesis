/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System.Data.Common;
using System.Data.SqlClient;
using ORM.Session.Engine.impl;

namespace ORM.Session
{
    public sealed class SqlServerSession : AbstractSession 
    {
        public SqlServerSession(string connectionString) : base(connectionString)
        {
        }

        public override bool CanHandle(DatabaseEngine databaseEngine)
        {
            return databaseEngine == DatabaseEngine.SqlServer;
        }

        protected override DbConnection ConnectionFactory(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        protected override DbCommand CommandFactory(string query)
        {
            return new SqlCommand(query, (SqlConnection)Connection);
        }
    }
}