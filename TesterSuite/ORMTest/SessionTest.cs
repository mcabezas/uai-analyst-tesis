/**
 * Created by Marcelo Cabezas on 2019-Apr-21 11:12:57 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data.SqlClient;
using ORM;
using ORM.Query;
using ORM.Result;
using ORM.Session;
using TesterSuite.Core;
using Utilities.Generics;
using static TesterSuite.Core.Asserts.Assertion;

namespace TesterSuite.ORMTest
{
    public class SessionTest : TestSuite
    {
        private Session _session;
        public override void SetUpClass()
        {
            _session = new Session();
            _session.Open();
        }

        public override void CleanUpClass()
        {
            _session.Dispose();
        }

        protected override Collection<Action> Test()
        {
            return new Collection<Action>
            {
                ExecuteNonQueryTest,
                ExecuteNonQueryNegativeTest,
                ExecuteQueryTest
            };
        }

        private void ExecuteNonQueryNegativeTest()
        {
            try {
                _session.ExecuteNativeNonQuery("Executing wrong query ...");
                Fail();
            }
            catch (SqlException) {
                Pass();
            }
        }
        
        private void ExecuteNonQueryTest()
        {
            const string query = "IF OBJECT_ID('dbo.DUMMY', 'U') IS NOT NULL " +
                                 "DROP TABLE dbo.DUMMY; " +
                                 "CREATE TABLE dbo.DUMMY ( DUMMY1 VARCHAR(1), DUMMY2 NUMERIC )";
            _session.ExecuteNativeNonQuery(query);
        }

        private void ExecuteQueryTest()
        {
            _session.ExecuteNativeNonQuery("INSERT INTO dbo.DUMMY(DUMMY1, DUMMY2) VALUES ('A', 1)");
            _session.ExecuteNativeNonQuery("INSERT INTO dbo.DUMMY(DUMMY1, DUMMY2) VALUES ('B', 2)");
            _session.ExecuteNativeNonQuery("INSERT INTO dbo.DUMMY(DUMMY1, DUMMY2) VALUES ('C', 3)");
            _session.ExecuteNativeNonQuery("INSERT INTO dbo.DUMMY(DUMMY1, DUMMY2) VALUES ('D', 4)");
            _session.ExecuteNativeNonQuery("INSERT INTO dbo.DUMMY(DUMMY1, DUMMY2) VALUES ('E', 5)");

            ResultSet resultSet = _session.ExecuteNativeQuery("SELECT * FROM dbo.DUMMY;");
            
        }
    }
}