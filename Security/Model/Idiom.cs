/**
 * Created by Marcelo Cabezas on 2019-May-17 7:27:57 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Layers.Model;

namespace Security.Model
{
    public class Idiom : Entity
    {
        public Idiom()
        {
            Description = "";
        }

        public string Description { get; set; }
    }
}