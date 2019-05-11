/**
 * Created by Marcelo Cabezas on 2019-May-01 3:14:37 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using ORM.Session.Handlers;

namespace ORM.Session.States
{
    public sealed class SessionStateOpen : ISessionState
    {
        public bool CanHandle(ISession aSession)
        {
            return aSession.IsOpen();
        }

        public void Open(IStateHandleable aSession, int connectionTimeout)
        {
            aSession.OpenDbConnectionWhenOpen();
        }

        public void Close(IStateHandleable aSession)
        {
            aSession.CloseDbConnectionWhenOpen();
        }
    }
}