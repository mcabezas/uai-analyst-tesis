/**
 * Created by Marcelo Cabezas on 2019-Apr-21 11:12:57 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data.SqlClient;
using Commons.Generics;
using Commons.Generics.impl;
using DBW.DBWrapper.Core.impl;
using DBW.DBWrapper.Engine;
using TesterSuite.Core.Suites.impl;

namespace DBWrapperTest.DBWrapper
{
    public class SqlServerDatabaseTest : TestSuite
    {
        private IDatabase _database;

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

            _database = SqlServerDatabaseFactory.Instance.GetDatabase();
        }

        public override void CleanUpClass()
        {
        }

        protected override void SetUp()
        {
            _database.ExecuteInsert(SqlDropDummyTable, (command, newProperty) => {});
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
                _database.ExecuteInsert("Executing wrong query ...", (command, newProperty) => {});
                Assertion.Fail();
            }
            catch (SqlException) {
                Assertion.Pass();
            }
        }
        
        private void ExecuteNonQueryTest()
        {
            _database.ExecuteInsert(SqlCreateDummyTable, (command, newProperty) => {});
        }

        private void ExecuteQueryTest()
        {
            _database.ExecuteInsert(SqlCreateDummyTable, (command, newProperty) => {});

            _database.ExecuteInsert("INSERT INTO DUMMY(DUMMY1, DUMMY2) VALUES ('D', 4)", (command, newProperty) => {});
            _database.ExecuteInsert("INSERT INTO DUMMY(DUMMY1, DUMMY2) VALUES ('E', 5)", (command, newProperty) => {});

            var dbRows = _database.ExecuteNativeQuery("SELECT * FROM DUMMY;", (command, newParameter) => { });
            
            Assertion.AreEqual(2, dbRows.Count);
            Assertion.AreEqual(2, dbRows[0].Columns.Count);
        }
        
        protected override bool IgnoreSuite()
        {
            return true;
        }
    }
}