/**
 * Created by Marcelo Cabezas on 2019-May-17 7:27:57 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Security.Model
{
    public class Idiom
    {
        public static readonly Idiom NullIdiom = new Idiom();

        public Idiom()
        {
            Id = -1;
            Description = "";
        }

        public int Id { get; set; }
        public string Description { get; set; }
    }
}