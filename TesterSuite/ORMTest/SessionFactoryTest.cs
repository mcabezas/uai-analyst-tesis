/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Threading;
using ORM.Session;
using TesterSuite.Core.Suites.impl;
using Utilities.Generics;
using Utilities.Generics.impl;

namespace TesterSuite.ORMTest
{

    public class SessionFactoryTest : TestSuite
    {
        private PostgresSessionFactory _postgresSessionFactory;
        private SqlServerSessionFactory _sqlServerSessionFactory;
        
        public override void SetUpClass()
        {
            _postgresSessionFactory = PostgresSessionFactory.Instance;
            _sqlServerSessionFactory = SqlServerSessionFactory.Instance;
        }

        protected override void CleanUp()
        {
            _postgresSessionFactory?.GetSession()?.Close();
            _sqlServerSessionFactory?.GetSession()?.Close();
        }

        protected override IMCollection<Action> Tests()
        {
            return new MCollection<Action>
            {
                OpenSessionSqlServerTest,
                OpenSessionPostgresTest,
                GetSessionSqlServerTest,
                GetSessionPostgresTest,
                CloseSessionSqlServerTest,
                CloseSessionPostgresTest
//                SessionTimeoutTest
            };
        }

        private void OpenSessionSqlServerTest()
        {
            ISession sqlSession = _sqlServerSessionFactory.OpenSession();
            Assertion.IsNotNull(sqlSession);
        }
        
        private void OpenSessionPostgresTest()
        {
            ISession postgresSession = _postgresSessionFactory.OpenSession();
            Assertion.IsNotNull(postgresSession);
        }


        private void GetSessionSqlServerTest()
        {
            ISession sqlSession = _sqlServerSessionFactory.OpenSession();
            ISession sqlSession2 = _sqlServerSessionFactory.GetSession();
            Assertion.AreSameReference(sqlSession, sqlSession2);
        }

        private void GetSessionPostgresTest()
        {
            ISession postgresSession = _postgresSessionFactory.OpenSession();
            ISession postgresSession2 = _postgresSessionFactory.GetSession();
            Assertion.AreSameReference(postgresSession, postgresSession2);
        }

        private void CloseSessionSqlServerTest()
        {
            CloseSessionHelperTest(_sqlServerSessionFactory);
        }
        
        private void CloseSessionPostgresTest()
        {
            CloseSessionHelperTest(_postgresSessionFactory);
        }

        private void CloseSessionHelperTest(ISessionFactory sessionFactory)
        {
            ISession session0 = sessionFactory.GetSession();
            session0.Close();
            session0.Close();
            
            ISession session1 = sessionFactory.OpenSession();
            Assertion.IsTrue(session1.IsOpen());

            session1.Close();
            Assertion.IsFalse(session1.IsOpen());
            
            ISession session2 = sessionFactory.OpenSession();
            ISession session3 = sessionFactory.OpenSession();

            Assertion.AreSameReference(session2, session3);
        }

        private void SessionTimeoutTest()
        {
//            _sessionFactory.ConnectionStringBuilder.ConnectTimeout = 15;
            ISession sqlSession = _postgresSessionFactory.GetSession();

            sqlSession.Open();
            
//            Thread.Sleep(_sessionFactory.ConnectionStringBuilder.ConnectTimeout * 2000);
            
            Assertion.IsTrue(sqlSession.IsOpen());
        }
    }
}