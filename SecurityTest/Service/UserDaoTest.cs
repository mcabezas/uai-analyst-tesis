/**
 * Created by Marcelo Cabezas on 2019-May-12 10:57:46 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using Commons.Generics;
using Commons.Generics.impl;
using Security.DAO;
using Security.DAO.impl;
using TesterSuite.Core.Suites.impl;

namespace SecurityTest.Service
{
    public class UserDaoTest : TestSuite
    {
        protected override IMCollection<Action> Tests()
        {
            return new MCollection<Action>()
            {
                InsertUserTest
            };
        }

        private void InsertUserTest()
        {
            UserDao dao = new UserDao();
        }
    }
}