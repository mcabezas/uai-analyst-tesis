/**
 * Created by Marcelo Cabezas on 2019-May-01 3:15:14 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace ORM.Session.States
{
    public class SessionStateClose : SessionState
    {
        public override bool CanHandle(Session aSession)
        {
            return !aSession.IsOpen;
        }

        public override void Open(Session aSession, int connectionTimeout)
        {
            aSession.OpenDbConnectionWhenNotOpen(connectionTimeout);
        }

        public override void Close(Session aSession)
        {
            Session.CloseDbConnectionWhenClose();
        }
    }
}