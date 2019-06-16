/**
 * Created by Marcelo Cabezas on 2019-May-24 6:51:08 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Security.Model;
using static SecurityTest.Builder.RandomGenerator;

namespace SecurityTest.Builder
{
    public class UserBuilder
    {
        private string _firstName;
        private string _lastName;
        private Idiom _idiom;
        private readonly IdiomBuilder _idiomBuilder = new IdiomBuilder();

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

        public UserBuilder WithIdiom(Idiom idiom)
        {
            _idiom = idiom;
            return this;
        }

        public User Build()
        {
            return new User
            {
                FirstName = _firstName ?? RandomString(10, true), 
                LastName = _lastName ?? RandomString(10, true),
                Idiom = _idiom ?? _idiomBuilder.Build()
            };
        }
    }
    
}