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

        protected override IMCollection<Action> Tests()
        {
            return new MCollection<Action>
            {
                OpenSessionTest, 
                GetSessionTest, 
                CloseSessionTest,
//                SessionTimeoutTest
            };
        }

        private void OpenSessionTest()
        {
            Session session = _sessionFactory.OpenSession();
            Assertion.IsNotNull(session);
        }

        private void GetSessionTest()
        {
            Session session = _sessionFactory.OpenSession();
            Session session2 = _sessionFactory.GetSession();
            Assertion.AreSameReference(session, session2);
        }
        
        private void CloseSessionTest()
        {
            Session session0 = _sessionFactory.GetSession();
            session0.Close();
            session0.Close();
            
            Session session1 = _sessionFactory.OpenSession();
            Assertion.IsTrue(session1.IsOpen);

            session1.Close();
            Assertion.IsFalse(session1.IsOpen);
            
            Session session2 = _sessionFactory.OpenSession();
            Session session3 = _sessionFactory.OpenSession();

            Assertion.AreSameReference(session2, session3);
        }

        private void SessionTimeoutTest()
        {
            _sessionFactory.ConnectionStringBuilder.ConnectTimeout = 15;
            Session session = _sessionFactory.GetSession();

            session.Open();
            
            Thread.Sleep(_sessionFactory.ConnectionStringBuilder.ConnectTimeout * 2000);
            
            Assertion.IsTrue(session.IsOpen);
        }
    }
}