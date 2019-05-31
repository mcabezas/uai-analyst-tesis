/**
 * Created by Marcelo Cabezas on 2019-May-30 6:53:46 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Security.Model;
using static SecurityTest.Builder.RandomGenerator;

namespace SecurityTest.Builder
{
    public class IdiomBuilder
    {
        private string _description;

        public IdiomBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public Idiom Build()
        {
            Idiom anIdiom = new Idiom
            {
                Description = _description ?? RandomString(10, true)
            };
            return anIdiom;
        }
    }
}