/**
 * Created by Marcelo Cabezas on 2019-Jun-16 11:45:55 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;
using Commons.Generics.impl;

namespace Security.Model
{
    public class Group : Entity
    {
        public static readonly Group NullGroup = new Group();

        public Group()
        {
            Description = "";
            Permissions = new MCollection<Permission>();
        }

        public Group(string description, IMCollection<Permission> permissions)
        {
            Description = description;
            Permissions = permissions;
        }
        
        public string Description { get; set; }
        public IMCollection<Permission> Permissions { get; set; }
    }
}