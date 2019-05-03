/**
 * Created by Marcelo Cabezas on 2019-May-02 7:13:58 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Logger.States
{
    public class LogInfoEnable : ConfigurationState
    {
        public override bool CanHandle(Logger aLogger)
        {
            return aLogger.Configuration.IsInfoEnabled;
        }

        public override void Log(Logger aLogger, object message)
        {
            aLogger.LogInfo(message);
        }
    }
}