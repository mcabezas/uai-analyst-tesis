/**
 * Created by Marcelo Cabezas on 2019-Apr-22 7:08:47 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using Logger.Configuration;
using static Logger.Configuration.LoggerConfigurationHandler;

namespace Logger
{
    public class Logger
    {
        public const string INFO = "INFO";
        public const string ERROR = "ERROR";
        public const string DEBUG = "DEBUG";

        private readonly Type _sourceClass;
        public readonly LoggerConfiguration Configuration;

        public Logger(Type sourceClass)
        {
            _sourceClass = sourceClass;
            Configuration = new LoggerConfiguration();
        }
        
        public Logger(Type sourceClass, LoggerConfiguration configuration)
        {
            _sourceClass = sourceClass;
            Configuration = configuration;
        }

        public void Info(object message)
        {
            ToHandleInfoState(this).Log(this, message);
        }
        
        public void Debug(object message)
        {
            ToHandleDebugState(this).Log(this, message);
        }
        
        public void Error(object message)
        {
            ToHandleErrorState(this).Log(this, message);
        }


        internal void LogInfo(object message)
        {
            Log(INFO, message);
        }

        internal void LogDebug(object message)
        {
            Log(DEBUG, message);
        }

        internal void LogError(object message)
        {
            Log(ERROR, message);
        }
        
        public void Log(string level, object message)
        {
            PrintLog("["+ level +"] - " + _sourceClass+ " : " + message);
        }

        public static void PrintLog(string message)
        {
            Console.WriteLine(message);
        }

        internal static void LogWhenNotEnable()
        {
        }

    }
}