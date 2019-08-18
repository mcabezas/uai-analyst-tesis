/**
 * Created by Marcelo Cabezas on 2019-May-12 10:57:46 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using Commons.Generics;
using Commons.Generics.impl;
using Layers.Service;
using Security.Model;
using Security.Service;
using SecurityTest.Builder;
using TesterSuite.Core.Suites.impl;

namespace SecurityTest.Service
{
    public class UserServiceTest : TestSuite
    {
        private readonly IService<User, int> _userService = new UserService();
        private readonly IService<Idiom, int> _idiomService = new IdiomService();
        private readonly IService<Permission, int> _permissionService = new PermissionService();
        private readonly IService<Group, int> _groupService = new GroupService();

        protected override void SetUp()
        {
            CleanUp();
        }

        protected override void CleanUp()
        {
            _userService.DeleteAll();
            _idiomService.DeleteAll();
            _groupService.DelseteAll();
            _permissionService.DeleteAll();
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
                FindByIdLazyModeIsLazy,
                InsertUserWithInsertedPermissionsTest,
                InsertUserWithNonInsertedPermissionsTest,
                InsertUserWithInsertedGroupsTest,
                InsertUserWithNonInsertedGroupsTest
            };
        }

        private void CanInsertAUserTest()
        {
            Assertion.AreEqual(0, _userService.FindAll().Count);
            
            _userService.Insert(new UserBuilder().Build());
            
            Assertion.AreEqual(1, _userService.FindAll().Count);
        }
        
        private void InsertUserDoesNotInsertAnIdiomTest()
        {
            Assertion.AreEqual(0, _idiomService.FindAll().Count);

            _userService.Insert(new UserBuilder().Build());
            
            Assertion.AreEqual(0, _idiomService.FindAll().Count);
        }

        
        private void CanInsertMoreThanOneUserTest()
        {
            _userService.Insert(new UserBuilder().Build());
            _userService.Insert(new UserBuilder().Build());
            _userService.Insert(new UserBuilder().Build());
            
            Assertion.AreEqual(3, _userService.FindAll().Count);
        }

        private void CanFindUserWithAValidId()
        {
            const string aFirstName = "Marcelo";
            const string aLastName = "Cabezas";
            const string anEmail = "mcabezas@outlook.com";
            
            User user = new UserBuilder()
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
            Assertion.AreEqual(0, userFoundById.Id);
        }

        private void CanInsertAUserWithAnIdiom()
        {
            Idiom anIdiom = new IdiomBuilder().Build();
            int anInsertedIdiomId = _idiomService.Insert(anIdiom);
            Idiom anInsertedIdiom = _idiomService.FindByIdLazyMode(anInsertedIdiomId);

            User aUser = new UserBuilder().WithIdiom(anInsertedIdiom).Build();
            int insertedUserId = _userService.Insert(aUser);
            User anInsertedUser = _userService.FindByIdLazyMode(insertedUserId);
            
            Assertion.AreEqual(anInsertedIdiom.Id, anInsertedUser.Idiom.Id);
        }
        
        private void FindByIdLazyModeIsLazy()
        {
            Idiom anIdiom = new IdiomBuilder().Build();
            int anInsertedIdiomId = _idiomService.Insert(anIdiom);
            Idiom anInsertedIdiom = _idiomService.FindByIdLazyMode(anInsertedIdiomId);

            User aUser = new UserBuilder().WithIdiom(anInsertedIdiom).Build();
            int insertedUserId = _userService.Insert(aUser);
            User anInsertedUser = _userService.FindByIdLazyMode(insertedUserId);
            
            Assertion.AreEqual("", anInsertedUser.Idiom.Description);
        }

        private void InsertUserWithInsertedPermissionsTest()
        {
            Assertion.AreEqual(0, _permissionService.FindAll().Count);

            _permissionService.Insert(new PermissionBuilder().Build());
            _permissionService.Insert(new PermissionBuilder().Build());
            User aUser = new UserBuilder().WithPermissions(_permissionService.FindAll()).Build();

            int insertedUserId = _userService.Insert(aUser);

            User insertedUser = _userService.FindById(insertedUserId);
            Assertion.AreEqual(2, insertedUser.Permissions.Count);
        }
        
        private void InsertUserWithNonInsertedPermissionsTest()
        {
            Assertion.AreEqual(0, _permissionService.FindAll().Count);

            User aUser = new UserBuilder()
                .WithPermissions( new MCollection<Permission> {
                    new PermissionBuilder().Build(),
                    new PermissionBuilder().Build(),
                    new PermissionBuilder().Build() } )
                .Build();

            int insertedUserId = _userService.Insert(aUser);

            User insertedUser = _userService.FindById(insertedUserId);
            Assertion.AreEqual(3, insertedUser.Permissions.Count);
        }
        
        private void InsertUserWithInsertedGroupsTest()
        {
            Assertion.AreEqual(0, _groupService.FindAll().Count);

            _groupService.Insert(new GroupBuilder().Build());
            _groupService.Insert(new GroupBuilder().Build());
            User aUser = new UserBuilder().WithGroups(_groupService.FindAll()).Build();

            int insertedUserId = _userService.Insert(aUser);

            User insertedUser = _userService.FindById(insertedUserId);
            Assertion.AreEqual(2, insertedUser.Groups.Count);
        }
        
        private void InsertUserWithNonInsertedGroupsTest()
        {
            Assertion.AreEqual(0, _groupService.FindAll().Count);

            User aUser = new UserBuilder()
                .WithGroups( new MCollection<Group> {
                    new GroupBuilder().Build(),
                    new GroupBuilder().Build(),
                    new GroupBuilder().Build() } )
                .Build();

            int insertedUserId = _userService.Insert(aUser);

            User insertedUser = _userService.FindById(insertedUserId);
            Assertion.AreEqual(3, insertedUser.Groups.Count);
        }

    }
}