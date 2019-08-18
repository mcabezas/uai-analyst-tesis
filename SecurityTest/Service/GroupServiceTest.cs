/**
 * Created by Marcelo Cabezas on 2019-Jun-16 11:57:36 AM
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
    public class GroupServiceTest : TestSuite
    {
        private readonly IService<Group, int> _groupService = new GroupService();
        private readonly IService<Permission, int> _permissionService = new PermissionService();
        private readonly IService<User, int> _userService = new UserService();

        private readonly GroupBuilder _groupBuilder = new GroupBuilder();
        private readonly PermissionBuilder _permissionBuilder = new PermissionBuilder();

        protected override void SetUp()
        {
            CleanUp();
        }
        
        protected override void CleanUp()
        {
            _userService.DeleteAll();
            _groupService.DeleteAll();
            _permissionService.DeleteAll();
        }
        
        protected override IMCollection<Action> Tests()
        {
            return new MCollection<Action>
            {
                CanInsertAGroup,
                CanInsertAGroupWithNotInsertedPermissions,
                CanInsertMoreThanAGroup,
                FindByIdLazyIsLazyTest,
                FindByIdTest
            };
        }

        private void CanInsertAGroup()
        {
            _groupService.Insert(_groupBuilder.Build());
            Assertion.AreEqual(1, _groupService.FindAll().Count);
        }
        
        private void CanInsertAGroupWithNotInsertedPermissions()
        {
            Group aGroup = _groupBuilder
                .WithPermissions(new MCollection<Permission>{_permissionBuilder.Build()})
                .Build();
            int insertedId = _groupService.Insert(aGroup);
            Group insertedGroup = _groupService.FindById(insertedId);
            Assertion.AreEqual(1, insertedGroup.Permissions.Count);
        }

        private void CanInsertMoreThanAGroup()
        {
            _groupService.Insert(_groupBuilder.Build());
            _groupService.Insert(_groupBuilder.Build());
            _groupService.Insert(_groupBuilder.Build());

            Assertion.AreEqual(3, _groupService.FindAll().Count);
        }

        private void FindByIdLazyIsLazyTest()
        {
            const string aDescription = "description";

            Permission permission1 = _permissionBuilder.Build();
            Permission permission2 = _permissionBuilder.Build();
            _permissionService.Insert(permission1);
            _permissionService.Insert(permission2);
            
            Group aGroup = _groupBuilder
                .WithDescription(aDescription)
                .WithPermissions(_permissionService.FindAll())
                .Build();

            int insertedGroupId = _groupService.Insert(aGroup);
            Group foundByIdLazyMode = _groupService.FindByIdLazyMode(insertedGroupId);
            Assertion.AreEqual(1, _groupService.FindAll().Count);
            Assertion.AreEqual(aDescription, foundByIdLazyMode.Description);
            Assertion.AreEqual(0, foundByIdLazyMode.Permissions.Count);
        }
        
        private void FindByIdTest()
        {
            const string aDescription = "description";

            Permission permission1 = _permissionBuilder.Build();
            Permission permission2 = _permissionBuilder.Build();
            
            Group aGroup = _groupBuilder
                .WithDescription(aDescription)
                .WithPermissions(new MCollection<Permission>{permission1, permission2})
                .Build();

            int insertedGroupId = _groupService.Insert(aGroup);
            Group foundByIdLazyMode = _groupService.FindById(insertedGroupId);
            Assertion.AreEqual(1, _groupService.FindAll().Count);
            Assertion.AreEqual(aDescription, foundByIdLazyMode.Description);
            Assertion.AreEqual(2, foundByIdLazyMode.Permissions.Count);
        }

    }
}