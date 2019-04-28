/**
 * Created by Marcelo Cabezas on 2019-Apr-28 7:10:24 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */
using Utilities.Generics;

namespace ORM.Result
{
    public class DbRow
    {
        public readonly Collection<DbColumn> Columns = new Collection<DbColumn>();
    }
}