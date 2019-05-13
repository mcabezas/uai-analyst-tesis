/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data.SqlClient;
using Commons.Generics;
using Commons.Generics.impl;
using DBW.DBWrapper.Engine;
using DBW.DBWrapper.Engine.impl;
using TesterSuite.Core.Suites.impl;

namespace DBWrapperTest.DBWrapper
{

    public class ConnectionTest : TestSuite
    {
        private IConnection _postgresConnection;
        private IConnection _sqlServerConnection;
        
        public override void SetUpClass()
        {
            const string connectionString = "Server=localhost;Port=5432;" +
                                            "User Id=postgres;Password=postgres;Database=test;Timeout=300";

            _postgresConnection = new PostgresDatabase(connectionString);
            
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "mcabezas.database.windows.net";
            connectionStringBuilder.UserID = "mcabezas";
            connectionStringBuilder.Password = "_2053Pega_";
            connectionStringBuilder.InitialCatalog = "mcabezas";
            _sqlServerConnection = new SqlServerDatabase(connectionStringBuilder.ConnectionString);
        }

        protected override void CleanUp()
        {
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
            IConnection sqlConnection = _sqlServerConnection.Open();
            Assertion.IsNotNull(sqlConnection);
        }
        
        private void OpenSessionPostgresTest()
        {
            IConnection postgresConnection = _postgresConnection.Open();
            Assertion.IsNotNull(postgresConnection);
        }


        private void GetSessionSqlServerTest()
        {
            IConnection sqlConnection = _sqlServerConnection.Open();
            IConnection sqlConnection2 = _sqlServerConnection.Open();
            Assertion.AreSameReference(sqlConnection, sqlConnection2);
        }

        private void GetSessionPostgresTest()
        {
            IConnection postgresConnection = _postgresConnection.Open();
            IConnection postgresConnection2 = _postgresConnection.Open();
            Assertion.AreSameReference(postgresConnection, postgresConnection2);
        }

        private void CloseSessionSqlServerTest()
        {
            CloseSessionHelperTest(_sqlServerConnection);
        }
        
        private void CloseSessionPostgresTest()
        {
            CloseSessionHelperTest(_postgresConnection);
        }

        private void CloseSessionHelperTest(IConnection databaseFactory)
        {
            IConnection session0 = databaseFactory.Open();
            session0.Close();
            session0.Close();
            
            IConnection session1 = databaseFactory.Open();
            Assertion.IsTrue(session1.IsOpen());

            session1.Close();
            Assertion.IsFalse(session1.IsOpen());
            
            IConnection session2 = databaseFactory.Open();
            IConnection session3 = databaseFactory.Open();

            Assertion.AreSameReference(session2, session3);
        }
//        private void SessionTimeoutTest()
//        {
//            _sessionFactory.ConnectionStringBuilder.ConnectTimeout = 15;
//            ISession sqlSession = _postgresSessionFactory.GetSession();
//
//            sqlSession.Open();
//            
//            Thread.Sleep(_sessionFactory.ConnectionStringBuilder.ConnectTimeout * 2000);
//            
//            Assertion.IsTrue(sqlSession.IsOpen());
//        }
    }
}