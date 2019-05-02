/**
 * Created by Marcelo Cabezas on 2019-May-01 3:10:44 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace ORM.Session.States
{
    public abstract class SessionState
    {
        public abstract bool CanHandle(Session aSession);
        public abstract void Open(Session aSession, int connectionTimeout);
        public abstract void Close(Session aSession);
    }
}