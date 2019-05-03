/**
 * Created by Marcelo Cabezas on 2019-May-02 7:13:32 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

namespace Logger.States
{
    public abstract class ConfigurationState
    {
        public abstract bool CanHandle(Logger aLogger);
        public abstract void Log(Logger aLogger, object message);
    }
}