/**
 * Created by Marcelo Cabezas on 2019-May-24 6:51:08 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Security.Model;

namespace SecurityTest.Builder
{
    public class UserBuilder
    {
        private string _firstName;
        private string _lastName;
        private Idiom _idiom;

        public UserBuilder()
        {
            
        }

        public void WithFirstName(string firstName)
        {
            _firstName = firstName;
        }


        public void WithLastName(string lastName)
        {
            _lastName = lastName;
        }

        public void WithIdiom(Idiom idiom)
        {
            _idiom = idiom;
        }

        public User Build()
        {
            User aUser = new User();
            
            return aUser;
        }

    }
    
}