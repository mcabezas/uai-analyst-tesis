/**
 * Created by Marcelo Cabezas on 2019-Apr-19 6:58:04 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using Logger.Configuration;

namespace Logger
{
    public class LoggerFactory
    {
        
        #region Singleton
        
        private static readonly Lazy<LoggerFactory> Lazy = new Lazy<LoggerFactory>
        (() => new LoggerFactory());

        public static LoggerFactory Instance => Lazy.Value;

        private LoggerFactory()
        {
        }

        #endregion

        public Logger GetLogger(Type sourceClass)
        {
            return new Logger(sourceClass);
        }
        
        public Logger GetLogger(Type sourceClass, LoggerConfiguration configuration)
        {
            return new Logger(sourceClass, configuration);
        }

        
    }
}