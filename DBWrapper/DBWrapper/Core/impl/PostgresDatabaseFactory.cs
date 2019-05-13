/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using DBW.DBWrapper.Engine;
using DBW.DBWrapper.Engine.impl;

namespace DBW.DBWrapper.Core.impl
{
    public sealed class PostgresDatabaseFactory : IDatabaseFactory
    {
        
        #region Singleton
        
        private static readonly Lazy<PostgresDatabaseFactory> Lazy = new Lazy<PostgresDatabaseFactory>
            (() => new PostgresDatabaseFactory());

        public static PostgresDatabaseFactory Instance => Lazy.Value;

        private PostgresDatabaseFactory()
        {
            const string connectionString = "Server=localhost;Port=5432;" +
                                            "User Id=postgres;Password=postgres;Database=test;Timeout=300";
            _database = new PostgresDatabase(connectionString);
        }
        
        #endregion

        private readonly IDatabase _database;

        public IDatabase GetDatabase()
        {
            return _database;
        }
    }
}