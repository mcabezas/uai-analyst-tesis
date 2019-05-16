/**
 * Created by Marcelo Cabezas on 2019-May-12 3:30:42 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Data;
using System.Data.Common;
using Commons.Generics;
using DBW.DBWrapper.Result;

namespace DBW.DBWrapper.Engine
{
    public interface IDatabase
    {
        int ExecuteNativeNonQuery(string query, int commandTimeout = 30);

        IMCollection<DbRow> ExecuteNativeQuery(string query, Action<DbCommand, Func<string, DbType, DbParameter>> parametersConfiguration, int commandTimeout = 30);

    }
}