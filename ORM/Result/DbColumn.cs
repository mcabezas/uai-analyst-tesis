/**
 * Created by Marcelo Cabezas on 2019-Apr-28 7:18:12 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;

namespace ORM.Result
{
    public class DbColumn
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
}