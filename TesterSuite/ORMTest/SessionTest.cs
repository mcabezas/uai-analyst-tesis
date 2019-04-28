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
using TesterSuite.Core;
using Utilities.Generics;
using static TesterSuite.Core.Asserts.Assertion;

namespace TesterSuite.ORMTest
{
    public class SessionTest : TestSuite
    {
        private Session _session;

        private const string DropDummyTable = 
            "IF OBJECT_ID('dbo.DUMMY', 'U') IS NOT NULL " +
            "DROP TABLE dbo.DUMMY; ";
        private const string CreateDummyTable = 
            "CREATE TABLE dbo.DUMMY ( DUMMY1 VARCHAR(1), DUMMY2 NUMERIC )";


        public override void SetUpClass()
        {
            _session = new Session();
            _session.Open();
        }

        public override void CleanUpClass()
        {
            _session.Dispose();
        }

        public override void SetUp()
        {
           _session.ExecuteNativeNonQuery(DropDummyTable);
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
            _session.ExecuteNativeNonQuery(CreateDummyTable);
        }

        private void ExecuteQueryTest()
        {
            _session.ExecuteNativeNonQuery(CreateDummyTable);

            _session.ExecuteNativeNonQuery("INSERT INTO dbo.DUMMY(DUMMY1, DUMMY2) VALUES ('D', 4)");
            _session.ExecuteNativeNonQuery("INSERT INTO dbo.DUMMY(DUMMY1, DUMMY2) VALUES ('E', 5)");

            ResultSet resultSet = _session.ExecuteNativeQuery("SELECT * FROM dbo.DUMMY;");
            
            AreEqual(2, resultSet.Rows.Count);
            AreEqual(2, resultSet.Rows[0].Columns.Count);
        }
    }
}