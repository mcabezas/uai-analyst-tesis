/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using ORM.Session;
using TesterSuite.Core;
using TesterSuite.Core.Asserts;
using Utilities.Generics;

namespace TesterSuite.ORMTest
{

    public class SessionaFactoryTest : TestSuite
    {
        private SessionFactory _sessionFactory;
        
        public override void SetUpClass()
        {
            _sessionFactory = SessionFactory.Instance;
        }

        public override void CleanUp()
        {
            _sessionFactory?.GetSession()?.Close();
        }

        protected override Collection<Action> Test()
        {
            return new Collection<Action>
            {
                OpenSessionTest, 
                GetSessionTest, 
                CloseSessionTest
            };
        }

        private void OpenSessionTest()
        {
            Session session = _sessionFactory.OpenSession();
            AssertFactory.Instance.IsNotNull(session);
        }

        private void GetSessionTest()
        {
            Session session = _sessionFactory.OpenSession();
            Session session2 = _sessionFactory.GetSession();
            AssertFactory.Instance.AreSameReference(session, session2);
        }
        
        private void CloseSessionTest()
        {
            Session session = _sessionFactory.OpenSession();
            AssertFactory.Instance.IsTrue(session.IsOpen, "1");

            session.Close();
            AssertFactory.Instance.IsFalse(session.IsOpen, "2");
            
            Session session2 = _sessionFactory.OpenSession();
            AssertFactory.Instance.AreNotSameReference(session, session2, "3");
        }

    }
}