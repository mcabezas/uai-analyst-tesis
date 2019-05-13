/**
 * Created by Marcelo Cabezas on 2019-May-01 3:15:14 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using DBW.DBWrapper.Engine;

namespace DBW.DBWrapper.States.impl
{
    internal sealed class SessionStateClose : ISessionState
    {
        public bool CanHandle(IConnection aConnection)
        {
            return !aConnection.IsOpen();
        }

        public void Open(IStateHandleable aSession, int connectionTimeout)
        {
            aSession.OpenDbConnectionWhenNotOpen(connectionTimeout);
        }

        public void Close(IStateHandleable aSession)
        {
            aSession.CloseDbConnectionWhenClose();
        }
    }
}