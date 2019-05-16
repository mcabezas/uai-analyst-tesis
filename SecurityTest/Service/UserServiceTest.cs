/**
 * Created by Marcelo Cabezas on 2019-May-12 10:57:46 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using Commons.Generics;
using Commons.Generics.impl;
using Security.Model;
using Security.Service;
using TesterSuite.Core.Suites.impl;

namespace SecurityTest.Service
{
    public class UserServiceTest : TestSuite
    {
        private readonly IService<User, int> _userService = new UserService();

        protected override void SetUp()
        {
            _userService.DeleteAll();
        }

        protected override IMCollection<Action> Tests()
        {
            return new MCollection<Action>()
            {
                CanInsertAUserTest,
                CanInsertMoreThanOneUserTest
            };
        }

        private void CanInsertAUserTest()
        {
            Assertion.AreEqual(0, _userService.FindAll().Count);

            User user = new User("Marcelo", "Cabezas");
            _userService.Insert(user);
            
            Assertion.AreEqual(1, _userService.FindAll().Count);
        }
        
        private void CanInsertMoreThanOneUserTest()
        {
            Assertion.AreEqual(0, _userService.FindAll().Count);

            User user = new User("Marcelo", "Cabezas");
            User user2 = new User("Marcelo2", "Cabezas");
            User user3 = new User("Marcelo3", "Cabezas");
            _userService.Insert(user);
            _userService.Insert(user2);
            _userService.Insert(user3);
            
            Assertion.AreEqual(3, _userService.FindAll().Count);
        }
    }
}