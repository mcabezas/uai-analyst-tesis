﻿using Commons.Generics;
using Commons.Generics.impl;
using Layers.Model;
using static Security.Model.Idiom;

namespace Security.Model
{
    public class User : Entity
    {
        public User()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Idiom = new Idiom();
            Groups = new MCollection<Group>();
            Permissions = new MCollection<Permission>();
        }

        public User(string firstName, string lastName, Idiom idiom)
        {
            FirstName = firstName;
            LastName = lastName;
            Idiom = idiom;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Idiom Idiom { get; set; }
        public IMCollection<Group> Groups { get; set; }
        public IMCollection<Permission> Permissions { get; set; }
    }
}