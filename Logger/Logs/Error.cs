/**
 * Created by Marcelo Cabezas on 2019-May-04 3:20:25 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Logger.Logs
{
    public class Error : ILoggable
    {
        public void Log(ILogger aLogger, string message)
        {
            aLogger.Log("[INFO] - " + message);
        }
    }
}