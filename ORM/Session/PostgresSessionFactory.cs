/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;

namespace ORM.Session
{
    public sealed class PostgresSessionFactory : ISessionFactory
    {
        
        #region Singleton
        
        private static readonly Lazy<PostgresSessionFactory> Lazy = new Lazy<PostgresSessionFactory>
            (() => new PostgresSessionFactory());

        public static PostgresSessionFactory Instance => Lazy.Value;

        private PostgresSessionFactory()
        {
            const string connectionString = "Server=localhost;Port=5432;" +
                                            "User Id=postgres;Password=postgres;Database=test;Timeout=300";
            _session = new PostgresSession(connectionString);
        }
        
        #endregion

        private readonly ISession _session;

        public ISession OpenSession()
        {
            return _session.Open();
        }

        public ISession GetSession()
        {
            return _session;
        }
    }
}