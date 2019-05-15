/**
 * Created by Marcelo Cabezas on 2019-May-12 10:57:46 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Numerics;
using Commons.Generics;
using Commons.Generics.impl;
using Security.Model;
using Security.Service;
using TesterSuite.Core.Suites.impl;

namespace SecurityTest.Service
{
    public class UserServiceTest : TestSuite
    {
        
        IService<User, BigInteger> service = new UserService();
        protected override IMCollection<Action> Tests()
        {
            return new MCollection<Action>()
            {
//                InsertUserTest
            };
        }

        private void InsertUserTest()
        {
            User user = new User("Marcelo", "Cabezas");
            User insertedUser = service.Insert(user);
            User foundUser = service.FindById(insertedUser.Id);
            Assertion.AreEqual(insertedUser.Id, foundUser.Id);
            Assertion.AreEqual(insertedUser.FirstName, foundUser.FirstName);
            Assertion.AreEqual(insertedUser.LastName, insertedUser.LastName);
        }
    }
}