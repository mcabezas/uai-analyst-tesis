/**
 * Created by Marcelo Cabezas on 2019-Apr-21 11:12:57 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data.Common;
using Commons.Generics;
using Commons.Generics.impl;
using DBW.DBWrapper.Core.impl;
using DBW.DBWrapper.Engine;
using TesterSuite.Core.Suites.impl;

namespace DBWrapperTest.DBWrapper
{
    public class PostgresDatabaseTest : TestSuite
    {
        private IDatabase _database;

        private const string PostgresDropDummyTable = 
            "DROP TABLE IF EXISTS dummy;";
        private const string PostgresCreateDummyTable = 
            "CREATE TABLE DUMMY ( id SERIAL PRIMARY KEY, DUMMY1 VARCHAR(1), DUMMY2 NUMERIC )";


        public override void SetUpClass()
        {
            _database = PostgresDatabaseFactory.Instance.GetDatabase();
        }

        public override void CleanUpClass()
        {

        }

        protected override void SetUp()
        {
           _database.ExecuteScalar(PostgresDropDummyTable);
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
                _database.ExecuteScalar("Executing wrong query ...");
                Assertion.Fail();
            }
            catch (DbException) {
                Assertion.Pass();
            }
        }
        
        private void ExecuteNonQueryTest()
        {
            _database.ExecuteScalar(PostgresCreateDummyTable);
        }

        private void ExecuteQueryTest()
        {
            _database.ExecuteScalar(PostgresCreateDummyTable);

            _database.ExecuteScalar("INSERT INTO DUMMY(DUMMY1, DUMMY2) VALUES ('D', 4)");
            _database.ExecuteScalar("INSERT INTO DUMMY(DUMMY1, DUMMY2) VALUES ('E', 5)");

            var dbRows = _database.ExecuteNativeQuery("SELECT * FROM DUMMY;", (command, newParameter) => { });
            
            Assertion.AreEqual(2, dbRows.Count);
            Assertion.AreEqual(3, dbRows[0].Columns.Count);
        }
    }
}