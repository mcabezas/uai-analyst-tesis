/**
 * Created by Marcelo Cabezas on 2019-May-01 3:22:33 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;
using Commons.Generics.impl;
using DBW.DBWrapper.Engine;

namespace DBW.DBWrapper.States.impl
{
    internal sealed class ConnectionStateHandler
    {
        
        public static ISessionState ToHandleConnectionState(IConnection aConnection)
        {
            IMCollection<ISessionState> states = new MCollection<ISessionState>
            {
                new SessionStateOpen(), 
                new SessionStateClose()
            };

            return states.Filter(state => state.CanHandle(aConnection)).GetFirst();
        }
    }
}