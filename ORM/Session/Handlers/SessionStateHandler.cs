/**
 * Created by Marcelo Cabezas on 2019-May-01 3:22:33 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using ORM.Session.States;
using Utilities.Generics;

namespace ORM.Session.Handlers
{
    public static class SessionStateHandler
    {
        private static readonly Collection<SessionState> States = new Collection<SessionState>
        {
            new SessionStateOpen(), 
            new SessionStateClose()
        };
        
        public static SessionState ToHandle(Session session)
        {
            return States.Filter(state => state.CanHandle(session)).GetFirst();
        }
    }
}