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
        private static readonly IMCollection<SessionState> States = new MCollection<SessionState>
        {
            new SessionStateOpen(), 
            new SessionStateClose()
        };
        
        public static SessionState ToHandleSessionState(Session aSession)
        {
            return States.Filter(state => state.CanHandle(aSession)).GetFirst();
        }
    }
}