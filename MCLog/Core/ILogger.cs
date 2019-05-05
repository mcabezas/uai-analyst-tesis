/**
 * Created by Marcelo Cabezas on 2019-May-04 11:47:57 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Log4CS.Core
{
    public interface ILogger
    {
        void Log(string message);
        void Info(string message);
        void Debug(string message);
        void Error(string message);
    }
}