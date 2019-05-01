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
        public override bool CanHandle(Session session)
        {
            return !session.IsOpen;
        }

        public override void Open(Session session, int connectionTimeout)
        {
            session.OpenDbConnectionWhenNotOpen(connectionTimeout);
        }
    }
}