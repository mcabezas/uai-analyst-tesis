/**
 * Created by Marcelo Cabezas on 2019-May-13 8:51:16 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Numerics;
using Commons.Generics;
using Commons.Generics.impl;
using DBW.DBWrapper.Result;
using DBW.DBWrapper.Result.impl;
using TesterSuite.Core.Suites.impl;

namespace DBWrapperTest.DBWrapper.Result
{
    public class ResultTransformerTest : TestSuite
    {
        private const string DummyStringValue = "dummyValue";
        private const int DummyIntValue = 1;
        private readonly BigInteger _dummyBigIntegerValue = 2;


        protected override IMCollection<Action> Tests()
        {
            return new MCollection<Action>
            {
                CanTransformAnEmptyResultSetTest,
                CanTransformAResultSetWithMoreThanOneItemTest,
                CanTransformWithNullableValuesTest,
                CanTransformWithNotRecognizedColumnsTest,
                CanTransformIgnoringCaseInColumnNameTest,
                CanTransformIgnoringUnderscoreInColumnNameTest,
                CanTransformAStringTypeTest,
                CanTransformAnIntTypeTest,
                CanTransformABigIntegerTypeTest
            };
        }

        private void CanTransformAnEmptyResultSetTest()
        {
            IResultTransformer<DummyClassToBeTransformed> resultTransformer = new ResultTransformer<DummyClassToBeTransformed>(new MCollection<DbRow>());

            Assertion.AreEqual(0, resultTransformer.Transform().Count);
        }

        private void CanTransformAResultSetWithMoreThanOneItemTest()
        {
            DbRow dbRow1 = new DbRow();
            dbRow1.Columns.Add(new DbColumn(typeof(string), "aColumn", null));
            
            DbRow dbRow2 = new DbRow();
            dbRow2.Columns.Add(new DbColumn(typeof(string), "aColumn", null));

            IMCollection<DbRow> dbRows = new MCollection<DbRow> {dbRow1, dbRow2};

            IResultTransformer<DummyClassToBeTransformed> resultTransformer = new ResultTransformer<DummyClassToBeTransformed>(dbRows);

            Assertion.AreEqual(2, resultTransformer.Transform().Count);
        }
        
        private void CanTransformWithNullableValuesTest()
        {
            DbRow dbRow = new DbRow();
            dbRow.Columns.Add(new DbColumn(typeof(string), "aColumn", null));
            
            IMCollection<DbRow> dbRows = new MCollection<DbRow> {dbRow};

            IResultTransformer<DummyClassToBeTransformed> resultTransformer = new ResultTransformer<DummyClassToBeTransformed>(dbRows);

            IMCollection<DummyClassToBeTransformed> transformed = resultTransformer.Transform();
            
            Assertion.AreEqual(1, transformed.Count);
            Assertion.AreEqual(null, transformed[0].AName);
        }

        private void CanTransformWithNotRecognizedColumnsTest()
        {
            const string dummyColumnName = "dummyName";

            DbRow dbRow = new DbRow();
            dbRow.Columns.Add(new DbColumn(typeof(string), dummyColumnName, DummyStringValue));
            
            IMCollection<DbRow> dbRows = new MCollection<DbRow> {dbRow};

            IResultTransformer<DummyClassToBeTransformed> resultTransformer = new ResultTransformer<DummyClassToBeTransformed>(dbRows);

            IMCollection<DummyClassToBeTransformed> transformed = resultTransformer.Transform();

            Assertion.AreEqual(null, transformed[0].AName);
        }

        private void CanTransformIgnoringCaseInColumnNameTest()
        {
            const string columnName = "aNaME";

            DbRow dbRow = new DbRow();
            dbRow.Columns.Add(new DbColumn(typeof(string), columnName, DummyStringValue));
            
            IMCollection<DbRow> dbRows = new MCollection<DbRow> {dbRow};

            IResultTransformer<DummyClassToBeTransformed> resultTransformer = new ResultTransformer<DummyClassToBeTransformed>(dbRows);

            IMCollection<DummyClassToBeTransformed> transformed = resultTransformer.Transform();

            Assertion.AreEqual(DummyStringValue, transformed[0].AName);
        }

        private void CanTransformIgnoringUnderscoreInColumnNameTest()
        {
            const string columnName = "A_NAME";

            DbRow dbRow = new DbRow();
            dbRow.Columns.Add(new DbColumn(typeof(string), columnName, DummyStringValue));
            
            IMCollection<DbRow> dbRows = new MCollection<DbRow> {dbRow};

            IResultTransformer<DummyClassToBeTransformed> resultTransformer = new ResultTransformer<DummyClassToBeTransformed>(dbRows);

            IMCollection<DummyClassToBeTransformed> transformed = resultTransformer.Transform();

            Assertion.AreEqual(DummyStringValue, transformed[0].AName);

        }
        
        private void CanTransformAStringTypeTest()
        {
            const string columnName = "aNaME";

            DbRow dbRow = new DbRow();
            dbRow.Columns.Add(new DbColumn(typeof(string), columnName, DummyStringValue));
            
            IMCollection<DbRow> dbRows = new MCollection<DbRow> {dbRow};

            IResultTransformer<DummyClassToBeTransformed> resultTransformer = new ResultTransformer<DummyClassToBeTransformed>(dbRows);

            IMCollection<DummyClassToBeTransformed> transformed = resultTransformer.Transform();

            Assertion.AreEqual(DummyStringValue, transformed[0].AName);
        }

        private void CanTransformAnIntTypeTest()
        {
            const string columnName = "aInt";

            DbRow dbRow = new DbRow();
            dbRow.Columns.Add(new DbColumn(typeof(int), columnName, DummyIntValue));
            
            IMCollection<DbRow> dbRows = new MCollection<DbRow> {dbRow};

            IResultTransformer<DummyClassToBeTransformed> resultTransformer = new ResultTransformer<DummyClassToBeTransformed>(dbRows);

            IMCollection<DummyClassToBeTransformed> transformed = resultTransformer.Transform();

            Assertion.AreEqual(DummyIntValue, transformed[0].AInt);
        }
        
        private void CanTransformABigIntegerTypeTest()
        {
            const string columnName = "aBigInteger";

            DbRow dbRow = new DbRow();
            dbRow.Columns.Add(new DbColumn(typeof(BigInteger), columnName, _dummyBigIntegerValue));
            
            IMCollection<DbRow> dbRows = new MCollection<DbRow> {dbRow};

            IResultTransformer<DummyClassToBeTransformed> resultTransformer = new ResultTransformer<DummyClassToBeTransformed>(dbRows);

            IMCollection<DummyClassToBeTransformed> transformed = resultTransformer.Transform();

            Assertion.AreEqual(_dummyBigIntegerValue, transformed[0].ABigInteger);
        }



        private class DummyClassToBeTransformed {
            public string AName { get; set; }
            public int AInt { get; set; }
            public BigInteger ABigInteger { get; set; }
        }
    }
}