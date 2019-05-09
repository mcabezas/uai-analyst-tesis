/**
 * Created by Marcelo Cabezas on 2019-May-09 7:29:00 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using ORM.Result;

namespace ORM.Session
{
    public interface ISession : IDisposable
    {
        ISession Open(int connectionTimeout = 30);
        void Close();
        bool IsOpen();
        int ExecuteNativeNonQuery(string query, int commandTimeout = 30);
        ResultSet ExecuteNativeQuery(string query, int commandTimeout = 30);
    }
}