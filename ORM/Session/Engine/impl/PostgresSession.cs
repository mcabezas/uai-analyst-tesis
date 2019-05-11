/**
 * Created by Marcelo Cabezas on 2019-May-09 8:58:23 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System.Data.Common;
using Npgsql;
using ORM.Session.Engine.impl;

namespace ORM.Session
{
    public class PostgresSession : AbstractSession
    {
        public PostgresSession(string connectionString) : base(connectionString)
        {
        }

        public override bool CanHandle(DatabaseEngine databaseEngine)
        {
            return databaseEngine == DatabaseEngine.Postgres;
        }

        protected override DbConnection ConnectionFactory(string connectionString)
        {
            return new NpgsqlConnection(connectionString);
        }

        protected override DbCommand CommandFactory(string query)
        {
            return new NpgsqlCommand(query, (NpgsqlConnection)Connection);
        }
    }
}