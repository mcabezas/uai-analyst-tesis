/**
 * Created by Marcelo Cabezas on 2019-May-30 6:53:46 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;
using Commons.Generics.impl;
using Security.Model;
using static SecurityTest.Builder.RandomGenerator;

namespace SecurityTest.Builder
{
    public class GroupBuilder
    {
        private PermissionBuilder _permissionBuilder = new PermissionBuilder();
        private string _description;
        private IMCollection<Permission> _permissions;

        public GroupBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public GroupBuilder WithPermissions(IMCollection<Permission> permissions)
        {
            _permissions = permissions;
            return this;
        }

        public Group Build()
        {
            Group aGroup = new Group
            {
                Description = _description ?? RandomString(10, true),
                Permissions = _permissions ?? new MCollection<Permission>
                {
                    _permissionBuilder.Build(),
                    _permissionBuilder.Build(),
                    _permissionBuilder.Build()
                }
            };
            return aGroup;
        }
    }
}