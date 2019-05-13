/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data.SqlClient;
using DBW.DBWrapper.Engine;
using DBW.DBWrapper.Engine.impl;

namespace DBW.DBWrapper.Core.impl
{
    public sealed class SqlServerDatabaseFactory : IDatabaseFactory
    {
        
        #region Singleton
        
        private static readonly Lazy<SqlServerDatabaseFactory> Lazy = new Lazy<SqlServerDatabaseFactory>
            (() => new SqlServerDatabaseFactory());

        public static SqlServerDatabaseFactory Instance => Lazy.Value;

        private SqlServerDatabaseFactory()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "mcabezas.database.windows.net";
            connectionStringBuilder.UserID = "mcabezas";
            connectionStringBuilder.Password = "_2053Pega_";
            connectionStringBuilder.InitialCatalog = "mcabezas";
            _database = new SqlServerDatabase(connectionStringBuilder.ConnectionString);
        }
        
        #endregion

        private readonly IDatabase _database;

        public IDatabase GetDatabase()
        {
            return _database;
        }
    }
}