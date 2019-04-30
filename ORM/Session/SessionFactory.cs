/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;

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
            DatabaseProperties = new DatabaseProperties(
                "mcabezas.database.windows.net",
                "1433", 
                "mcabezas", 
                "_2053Pega_", 
                "mcabezas");
        }
        
        #endregion

        public DatabaseProperties DatabaseProperties { get; }
        private readonly Session _session = new Session();

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