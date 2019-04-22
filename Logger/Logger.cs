/**
 * Created by Marcelo Cabezas on 2019-Apr-19 6:58:04 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;

namespace Logger
{
    public class Logger
    {
        
        #region Singleton
        
        private static readonly Lazy<Logger> Lazy = new Lazy<Logger>
        (() => new Logger());

        public static Logger Instance => Lazy.Value;

        private Logger()
        {
        }

        #endregion
        
        public void Info(object message)
        {
            Log(LogLevel.Info, message);

        }
        
        public void Debug(object message)
        {
            Log(LogLevel.Debug, message);
        }
        
        public void Error(object message)
        {
            Log(LogLevel.Error, message);
        }
        
        public void Log(LogLevel level, object message)
        {
            Print("["+ level.ToString().ToUpper() +"] - " + message);
        }

        private void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}