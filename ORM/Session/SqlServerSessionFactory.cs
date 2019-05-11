/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data.SqlClient;

namespace ORM.Session
{
    public sealed class SqlServerSessionFactory : ISessionFactory
    {
        
        #region Singleton
        
        private static readonly Lazy<SqlServerSessionFactory> Lazy = new Lazy<SqlServerSessionFactory>
            (() => new SqlServerSessionFactory());

        public static SqlServerSessionFactory Instance => Lazy.Value;

        private SqlServerSessionFactory()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "mcabezas.database.windows.net";
            connectionStringBuilder.UserID = "mcabezas";
            connectionStringBuilder.Password = "_2053Pega_";
            connectionStringBuilder.InitialCatalog = "mcabezas";
            _session = new SqlServerSession(connectionStringBuilder.ConnectionString);
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