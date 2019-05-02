/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Threading;
using ORM.Session;
using TesterSuite.Core;
using Utilities.Generics;
using static TesterSuite.Core.Asserts.Assertion;

namespace TesterSuite.ORMTest
{

    public class SessionFactoryTest : TestSuite
    {
        private SessionFactory _sessionFactory;
        
        public override void SetUpClass()
        {
            _sessionFactory = SessionFactory.Instance;
        }

        protected override void CleanUp()
        {
            _sessionFactory?.GetSession()?.Close();
        }

        protected override Collection<Action> Test()
        {
            return new Collection<Action>
            {
                OpenSessionTest, 
                GetSessionTest, 
                CloseSessionTest,
                SessionTimeoutTest
            };
        }

        private void OpenSessionTest()
        {
            Session session = _sessionFactory.OpenSession();
            IsNotNull(session);
        }

        private void GetSessionTest()
        {
            Session session = _sessionFactory.OpenSession();
            Session session2 = _sessionFactory.GetSession();
            AreSameReference(session, session2);
        }
        
        private void CloseSessionTest()
        {
            Session session0 = _sessionFactory.GetSession();
            session0.Close();
            session0.Close();
            
            Session session1 = _sessionFactory.OpenSession();
            IsTrue(session1.IsOpen);

            session1.Close();
            IsFalse(session1.IsOpen);
            
            Session session2 = _sessionFactory.OpenSession();
            Session session3 = _sessionFactory.OpenSession();

            AreSameReference(session2, session3);
        }

        private void SessionTimeoutTest()
        {
            _sessionFactory.ConnectionStringBuilder.ConnectTimeout = 15;
            Session session = _sessionFactory.GetSession();

            session.Open();
            
            Thread.Sleep(_sessionFactory.ConnectionStringBuilder.ConnectTimeout * 2000);
            
            IsTrue(session.IsOpen);
        }
    }
}