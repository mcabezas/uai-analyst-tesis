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
    public class SessionFactory
    {
        
        #region Singleton
        
        private static readonly Lazy<SessionFactory> Lazy = new Lazy<SessionFactory>
            (() => new SessionFactory());

        public static SessionFactory Instance => Lazy.Value;

        private SessionFactory()
        {
            ConnectionStringBuilder = new SqlConnectionStringBuilder();
            ConnectionStringBuilder.DataSource = "mcabezas.database.windows.net";
            ConnectionStringBuilder.UserID = "mcabezas";
            ConnectionStringBuilder.Password = "_2053Pega_";
            ConnectionStringBuilder.InitialCatalog = "mcabezas";
            
            _session = new Session(ConnectionStringBuilder);
       }
        
        #endregion

        public SqlConnectionStringBuilder ConnectionStringBuilder { get; }
        private readonly Session _session;

        public Session OpenSession()
        {
            return _session.Open();
        }

        public Session GetSession()
        {
            return _session;
        }
    }
}