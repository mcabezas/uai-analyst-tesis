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
        private Session _session;

        private void OnClosedSession(object source, EventArgs args)
        {
            _session.Dispose();
            _session = null;
        }

        public Session OpenSession()
        {
            if (_session == null) { _session = new Session(); }
            _session.Open();
            _session.ClosedSession += OnClosedSession;
            return _session;
        }

        public Session GetSession()
        {
            return _session ?? (_session = new Session());
        }
    }
}