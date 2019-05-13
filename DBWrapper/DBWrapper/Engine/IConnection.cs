/**
 * Created by Marcelo Cabezas on 2019-May-09 7:29:00 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace DBW.DBWrapper.Engine
{
    public interface IConnection
    {
        IConnection Open(int connectionTimeout = 30);
        void Close();
        bool IsOpen();
    }
}