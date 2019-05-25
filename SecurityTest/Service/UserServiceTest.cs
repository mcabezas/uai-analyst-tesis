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
using static Security.Model.Idiom;
using static Security.Model.User;

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
                CanInsertMoreThanOneUserTest,
                CanFindUserWithAValidId,
                CanNotFoundUserWithAnInvalidId
            };
        }

        private void CanInsertAUserTest()
        {
            Assertion.AreEqual(0, _userService.FindAll().Count);

            User user = new User("Marcelo", "Cabezas", NullIdiom);
            _userService.Insert(user);
            
            Assertion.AreEqual(1, _userService.FindAll().Count);
        }
        
        private void CanInsertMoreThanOneUserTest()
        {
            User user = new User("Marcelo", "Cabezas", NullIdiom);
            User user2 = new User("Marcelo2", "Cabezas", NullIdiom);
            User user3 = new User("Marcelo3", "Cabezas", NullIdiom);
            _userService.Insert(user);
            _userService.Insert(user2);
            _userService.Insert(user3);
            
            Assertion.AreEqual(3, _userService.FindAll().Count);
        }

        private void CanFindUserWithAValidId()
        {
            const string aFirstName = "MarceloIDLoco";
            
            User user = new User(aFirstName, "Cabezas", NullIdiom);
            int insertedId = (int) _userService.Insert(user);

            User userFoundById = _userService.FindById(insertedId);
            
            Assertion.AreEqual(aFirstName, userFoundById.FirstName);
        }

        
        private void CanNotFoundUserWithAnInvalidId()
        {
            const int invalidId = 1234;
            User userFoundById = _userService.FindById(invalidId);
            Assertion.AreSameReference(NullUser, userFoundById);
        }

        private void CanFoundUserWithIdiomAggregation()
        {
            const int invalidId = 1234;
            User userFoundById = _userService.FindById(invalidId);
            Assertion.AreSameReference(NullUser, userFoundById);
        }
    }
}