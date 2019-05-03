/**
 * Created by Marcelo Cabezas on 2019-May-02 7:14:27 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Logger.States
{
    public class LogDebugEnable : ConfigurationState
    {
        public override bool CanHandle(Logger aLogger)
        {
            return aLogger.Configuration.IsDebugEnabled;
        }

        public override void Log(Logger aLogger, object message)
        {
            aLogger.LogDebug(message);
        }
    }
}