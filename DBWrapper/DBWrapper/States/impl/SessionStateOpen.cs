/**
 * Created by Marcelo Cabezas on 2019-May-01 3:14:37 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using DBW.DBWrapper.Engine;

namespace DBW.DBWrapper.States.impl
{
    internal sealed class SessionStateOpen : ISessionState
    {
        public bool CanHandle(IConnection aConnection)
        {
            return aConnection.IsOpen();
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