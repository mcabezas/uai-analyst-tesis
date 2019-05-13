/**
 * Created by Marcelo Cabezas on 2019-May-01 3:10:44 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using DBW.DBWrapper.Engine;

namespace DBW.DBWrapper.States
{
    internal interface ISessionState
    {
        bool CanHandle(IConnection aSqlConnection);
        void Open(IStateHandleable aSqlSession, int connectionTimeout);
        void Close(IStateHandleable aSqlSession);
    }
}