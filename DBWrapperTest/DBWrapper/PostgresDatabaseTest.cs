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
using DBW.DBWrapper.Result;
using TesterSuite.Core.Suites.impl;

namespace DBWrapperTest.DBWrapper
{
    public class PostgresDatabaseTest : TestSuite
    {
        private IDatabase _database;

        private const string PostgresDropDummyTable = 
            "DROP TABLE IF EXISTS dummy;";
        private const string PostgresCreateDummyTable = 
            "CREATE TABLE DUMMY ( DUMMY1 VARCHAR(1), DUMMY2 NUMERIC )";


        public override void SetUpClass()
        {
            _database = PostgresDatabaseFactory.Instance.GetDatabase();
        }

        public override void CleanUpClass()
        {

        }

        protected override void SetUp()
        {
           _database.ExecuteNativeNonQuery(PostgresDropDummyTable);
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
                _database.ExecuteNativeNonQuery("Executing wrong query ...");
                Assertion.Fail();
            }
            catch (DbException) {
                Assertion.Pass();
            }
        }
        
        private void ExecuteNonQueryTest()
        {
            _database.ExecuteNativeNonQuery(PostgresCreateDummyTable);
        }

        private void ExecuteQueryTest()
        {
//            _sqlSession.ExecuteNativeNonQuery(SqlCreateDummyTable);
            _database.ExecuteNativeNonQuery(PostgresCreateDummyTable);

            _database.ExecuteNativeNonQuery("INSERT INTO DUMMY(DUMMY1, DUMMY2) VALUES ('D', 4)");
            _database.ExecuteNativeNonQuery("INSERT INTO DUMMY(DUMMY1, DUMMY2) VALUES ('E', 5)");

            ResultSet resultSet = _database.ExecuteNativeQuery("SELECT * FROM DUMMY;");
            
            Assertion.AreEqual(2, resultSet.Rows.Count);
            Assertion.AreEqual(2, resultSet.Rows[0].Columns.Count);
        }
    }
}