/**
 * Created by Marcelo Cabezas on 2019-May-01 3:10:44 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace ORM.Session.States
{
    public interface ISessionState
    {
        bool CanHandle(Session aSession);
        void Open(Session aSession, int connectionTimeout);
        void Close(Session aSession);
    }
}