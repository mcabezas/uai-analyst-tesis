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
using Security.Service.impl;
using SecurityTest.Builder;
using TesterSuite.Core.Suites.impl;
using static Security.Model.User;

namespace SecurityTest.Service
{
    public class UserServiceTest : TestSuite
    {
        private readonly IService<User, int> _userService = new UserService();
        private readonly IService<Idiom, int> _idiomService = new IdiomService();

        private readonly UserBuilder _userBuilder = new UserBuilder();
        private readonly IdiomBuilder _idiomBuilder = new IdiomBuilder();


        protected override void SetUp()
        {
            _userService.DeleteAll();
            _idiomService.DeleteAll();
        }

        protected override void CleanUp()
        {
            _userService.DeleteAll();
            _idiomService.DeleteAll();
        }

        protected override IMCollection<Action> Tests()
        {
            return new MCollection<Action>
            {
                CanInsertAUserTest,
                InsertUserDoesNotInsertAnIdiomTest,
                CanInsertMoreThanOneUserTest,
                CanFindUserWithAValidId,
                CanNotFoundUserWithAnInvalidId,
                CanInsertAUserWithAnIdiom,
                FindByIdLazyModeIsLazy
            };
        }

        private void CanInsertAUserTest()
        {
            Assertion.AreEqual(0, _userService.FindAll().Count);

            _userService.Insert(_userBuilder.Build());
            
            Assertion.AreEqual(1, _userService.FindAll().Count);
        }
        
        private void InsertUserDoesNotInsertAnIdiomTest()
        {
            Assertion.AreEqual(0, _idiomService.FindAll().Count);

            _userService.Insert(_userBuilder.Build());
            
            Assertion.AreEqual(0, _idiomService.FindAll().Count);
        }

        
        private void CanInsertMoreThanOneUserTest()
        {
            _userService.Insert(_userBuilder.Build());
            _userService.Insert(_userBuilder.Build());
            _userService.Insert(_userBuilder.Build());
            
            Assertion.AreEqual(3, _userService.FindAll().Count);
        }

        private void CanFindUserWithAValidId()
        {
            const string aFirstName = "Marcelo";
            const string aLastName = "Cabezas";
            const string anEmail = "mcabezas@outlook.com";
            
            User user = _userBuilder
                .WithFirstName(aFirstName)
                .WithLastName(aLastName)
                .WithEmail(anEmail)
                .Build();
            int insertedId = _userService.Insert(user);

            User userFoundById = _userService.FindByIdLazyMode(insertedId);
            
            Assertion.AreEqual(aFirstName, userFoundById.FirstName);
            Assertion.AreEqual(user.LastName, userFoundById.LastName);
            Assertion.AreEqual(user.Email, userFoundById.Email);
        }

        
        private void CanNotFoundUserWithAnInvalidId()
        {
            const int invalidId = 1234;
            User userFoundById = _userService.FindByIdLazyMode(invalidId);
            Assertion.AreSameReference(NullUser, userFoundById);
        }

        private void CanInsertAUserWithAnIdiom()
        {
            Idiom anIdiom = _idiomBuilder.Build();
            int anInsertedIdiomId = _idiomService.Insert(anIdiom);
            Idiom anInsertedIdiom = _idiomService.FindByIdLazyMode(anInsertedIdiomId);

            User aUser = _userBuilder.WithIdiom(anInsertedIdiom).Build();
            int insertedUserId = _userService.Insert(aUser);
            User anInsertedUser = _userService.FindByIdLazyMode(insertedUserId);
            
            Assertion.AreEqual(anInsertedIdiom.Id, anInsertedUser.Idiom.Id);
        }
        
        private void FindByIdLazyModeIsLazy()
        {
            Idiom anIdiom = _idiomBuilder.Build();
            int anInsertedIdiomId = _idiomService.Insert(anIdiom);
            Idiom anInsertedIdiom = _idiomService.FindByIdLazyMode(anInsertedIdiomId);

            User aUser = _userBuilder.WithIdiom(anInsertedIdiom).Build();
            int insertedUserId = _userService.Insert(aUser);
            User anInsertedUser = _userService.FindByIdLazyMode(insertedUserId);
            
            Assertion.AreEqual("", anInsertedUser.Idiom.Description);
        }

    }
}