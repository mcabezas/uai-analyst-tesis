/**
 * Created by Marcelo Cabezas on 2019-May-04 3:20:25 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Log4CS.Core;

namespace Log4CS.Logs.impl
{
    public sealed class Debug : ILoggable
    {
        public void Log(ILogger aLogger, string message)
        {
            aLogger.Log("[DEBUG] - " + message);
        }
    }
}