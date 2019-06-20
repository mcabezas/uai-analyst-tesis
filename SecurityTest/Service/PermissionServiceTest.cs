/**
 * Created by Marcelo Cabezas on 2019-May-30 7:40:48 PM
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
    public class PermissionServiceTest :  TestSuite
    {
        private readonly IService<Permission, int> _permissionService = new PermissionService();
        private readonly PermissionBuilder _permissionBuilder = new PermissionBuilder();

        protected override void SetUp()
        {
            _permissionService.DeleteAll();
        }

        protected override void CleanUp()
        {
            _permissionService.DeleteAll();
        }
        protected override IMCollection<Action> Tests()
        {
            return new MCollection<Action>
            {
                CanInsertAPermission,
                CanInsertMoreThanOnePermission,
                CanDeleteAPermission,
                FindByIdLazyModeTest,
            };
        }

        private void CanInsertAPermission()
        {
            _permissionService.Insert(_permissionBuilder.Build());
            Assertion.AreEqual(1, _permissionService.FindAll().Count);
        }

        private void CanInsertMoreThanOnePermission()
        {
            _permissionService.Insert(_permissionBuilder.Build());
            _permissionService.Insert(_permissionBuilder.Build());
            _permissionService.Insert(_permissionBuilder.Build());
            Assertion.AreEqual(3, _permissionService.FindAll().Count);
        }


        private void CanDeleteAPermission()
        {
            int insertedIdiomId = _permissionService.Insert(_permissionBuilder.Build());
            Assertion.AreEqual(1, _permissionService.FindAll().Count);
            _permissionService.DeleteById(insertedIdiomId);
            Assertion.AreEqual(0, _permissionService.FindAll().Count);
        }

        private void FindByIdLazyModeTest()
        {
            const string aDescription = "aDescription";
            Permission aPermission = _permissionBuilder
                .WithDescription(aDescription)
                .Build();
            int insertedPermissionId = _permissionService.Insert(aPermission);
            
            Permission aPermissionFound = _permissionService.FindByIdLazyMode(insertedPermissionId);
            Assertion.AreEqual(aDescription, aPermissionFound.Description);
        }
    }
}