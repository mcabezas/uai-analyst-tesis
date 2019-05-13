/**
 * Created by Marcelo Cabezas on 2019-Apr-21 11:28:08 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Commons.Generics;
using Commons.Generics.impl;

namespace DBW.DBWrapper.Result
{
    public sealed class ResultSet
    {
        public readonly IMCollection<DbRow> Rows = new MCollection<DbRow>();
    }
}