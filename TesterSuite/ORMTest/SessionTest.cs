/**
 * Created by Marcelo Cabezas on 2019-Apr-21 11:12:57 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using ORM;
using ORM.Query;
using ORM.Session;
using TesterSuite.Core;
using Utilities.Generics;

namespace TesterSuite.ORMTest
{
    public class SessionTest : TestSuite
    {
        private Session _session;
        public override void SetUpClass()
        {
            _session = new Session();
        }

        protected override Collection<Action> Test()
        {
            return new Collection<Action>
            {
                ExecuteQueryTest
            };
        }

        private void ExecuteQueryTest()
        {
            _session.Open();
            ResultSet resultSet = _session.ExecuteNativeQuery(null);
//            AssertFactory.Instance.IsNotNull(resultSet);
        }
    }
}