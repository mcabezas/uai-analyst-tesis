/**
 * Created by Marcelo Cabezas on 2019-Apr-21 11:12:57 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Collections.Generic;
using ORM;
using ORM.Session;
using TesterSuite.Core;

namespace TesterSuite.ORMTest
{
    public class SessionTest : TestSuite
    {
        private Session _session;
        public override void SetUpClass()
        {
            _session = new Session();
        }

        protected override List<Action> Test()
        {
            return new List<Action> {ExecuteQueryTest};
        }

        private void ExecuteQueryTest()
        {
            _session.Open();
            ResultSet resultSet = _session.ExecuteNativeQuery(null);
//            AssertFactory.Instance.IsNotNull(resultSet);
        }
    }
}