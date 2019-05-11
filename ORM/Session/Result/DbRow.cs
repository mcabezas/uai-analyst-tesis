/**
 * Created by Marcelo Cabezas on 2019-Apr-28 7:10:24 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */
using Utilities.Generics;
using Utilities.Generics.impl;

namespace ORM.Result
{
    public sealed class DbRow
    {
        public readonly IMCollection<DbColumn> Columns = new MCollection<DbColumn>();
    }
}