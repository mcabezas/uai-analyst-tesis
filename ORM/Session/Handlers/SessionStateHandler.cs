/**
 * Created by Marcelo Cabezas on 2019-May-01 3:22:33 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using ORM.Session.States;
using Utilities.Generics;
using Utilities.Generics.impl;

namespace ORM.Session.Handlers
{
    public sealed class SessionStateHandler
    {
        private static readonly IMCollection<ISessionState> States = new MCollection<ISessionState>
        {
            new SessionStateOpen(), 
            new SessionStateClose()
        };
        
        public static ISessionState ToHandleSessionState(SqlSession aSqlSession)
        {
            return States.Filter(state => state.CanHandle(aSqlSession)).GetFirst();
        }
    }
}