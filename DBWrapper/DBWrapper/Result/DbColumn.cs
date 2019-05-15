/**
 * Created by Marcelo Cabezas on 2019-Apr-28 7:18:12 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;

namespace DBW.DBWrapper.Result
{
    public sealed class DbColumn
    {
        public DbColumn(Type type, string name, object value)
        {
            Type = type;
            Name = name;
            Value = value;
        }

        public Type Type { get;}
        public string Name { get;}
        public object Value { get;}
    }
}