/**
 * Created by Marcelo Cabezas on 2019-May-01 3:14:37 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace ORM.Session.States
{
    public sealed class SessionStateOpen : ISessionState
    {
        public bool CanHandle(ISession aSqlSession)
        {
            return aSqlSession.IsOpen();
        }

        public void Open(IStateHandleable aSqlSession, int connectionTimeout)
        {
            aSqlSession.OpenDbConnectionWhenOpen();
        }

        public void Close(IStateHandleable aSqlSession)
        {
            aSqlSession.CloseDbConnectionWhenOpen();
        }
    }
}