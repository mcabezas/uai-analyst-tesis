/**
 * Created by Marcelo Cabezas on 2019-May-24 6:51:08 PM
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
    public class UserBuilder
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private Idiom _idiom;
        private IMCollection<Permission> _permissions;
        private IMCollection<Group> _groups;

        public UserBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public UserBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }
        
        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithIdiom(Idiom idiom)
        {
            _idiom = idiom;
            return this;
        }

        public UserBuilder WithPermissions(IMCollection<Permission> permissions)
        {
            _permissions = permissions;
            return this;
        }
        
        public UserBuilder WithGroups(IMCollection<Group> groups)
        {
            _groups = groups;
            return this;
        }


        public User Build()
        {
            return new User
            {
                FirstName = _firstName ?? RandomString(10, true), 
                LastName = _lastName ?? RandomString(10, true),
                Email = _email 
                        ?? RandomString(5, true) 
                        + "@"
                        +RandomString(5, true) 
                        + ".com",
                Idiom = _idiom ?? new Idiom(),
                Permissions = _permissions ?? new MCollection<Permission>(),
                Groups = _groups ?? new MCollection<Group>()
            };
        }
    }
    
}