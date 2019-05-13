/**
 * Created by Marcelo Cabezas on 2019-May-09 8:58:23 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System.Data.Common;
using Npgsql;

namespace DBW.DBWrapper.Engine.impl
{
    public class PostgresDatabase : AbstractDatabase
    {
        public PostgresDatabase(string connectionString) : base(connectionString)
        {
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