/**
 * Created by Marcelo Cabezas on 2019-Apr-21 11:12:57 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data.SqlClient;
using ORM.Result;
using ORM.Session;
using TesterSuite.Core.Suites.impl;
using Utilities.Generics;
using Utilities.Generics.impl;

namespace TesterSuite.ORMTest
{
    public class SqlServerSessionTest : TestSuite
    {
        private ISession _session;

        private const string SqlDropDummyTable = 
            "IF OBJECT_ID('dbo.DUMMY', 'U') IS NOT NULL " +
            "DROP TABLE DUMMY; ";
        private const string SqlCreateDummyTable = 
            "CREATE TABLE DUMMY ( DUMMY1 VARCHAR(1), DUMMY2 NUMERIC )";

        public override void SetUpClass()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "mcabezas.database.windows.net";
            connectionStringBuilder.UserID = "mcabezas";
            connectionStringBuilder.Password = "_2053Pega_";
            connectionStringBuilder.InitialCatalog = "mcabezas";

            _session = SqlServerSessionFactory.Instance.GetSession();
            _session.Open();
        }

        public override void CleanUpClass()
        {
            _session?.Close();
        }

        protected override void SetUp()
        {
            _session.ExecuteNativeNonQuery(SqlDropDummyTable);
        }
        
        protected override IMCollection<Action> Tests()
        {
            return new MCollection<Action>
            {
                ExecuteNonQueryTest,
                ExecuteNonQueryNegativeTest,
                ExecuteQueryTest
            };
        }

        private void ExecuteNonQueryNegativeTest()
        {
            try
            {
                _session.ExecuteNativeNonQuery("Executing wrong query ...");
                Assertion.Fail();
            }
            catch (SqlException) {
                Assertion.Pass();
            }
        }
        
        private void ExecuteNonQueryTest()
        {
            _session.ExecuteNativeNonQuery(SqlCreateDummyTable);
        }

        private void ExecuteQueryTest()
        {
            _session.ExecuteNativeNonQuery(SqlCreateDummyTable);

            _session.ExecuteNativeNonQuery("INSERT INTO DUMMY(DUMMY1, DUMMY2) VALUES ('D', 4)");
            _session.ExecuteNativeNonQuery("INSERT INTO DUMMY(DUMMY1, DUMMY2) VALUES ('E', 5)");

            ResultSet resultSet = _session.ExecuteNativeQuery("SELECT * FROM DUMMY;");
            
            Assertion.AreEqual(2, resultSet.Rows.Count);
            Assertion.AreEqual(2, resultSet.Rows[0].Columns.Count);
        }
    }
}