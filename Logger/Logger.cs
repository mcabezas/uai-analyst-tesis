/**
 * Created by Marcelo Cabezas on 2019-Apr-22 7:08:47 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;

namespace Logger
{
    public class Logger
    {
        private readonly Type _sourceClass;

        public Logger(Type sourceClass)
        {
            _sourceClass = sourceClass;
        }

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
            Print("["+ level.ToString().ToUpper() +"] - " + _sourceClass+ " : " + message);
        }

        private void Print(string message)
        {
            Console.WriteLine(message);
        }

    }
}