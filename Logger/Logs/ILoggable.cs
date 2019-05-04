/**
 * Created by Marcelo Cabezas on 2019-May-04 2:06:11 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Logger.Logs
{
    public interface ILoggable
    {
        void Log(ILogger aLogger, string message);
    }
}