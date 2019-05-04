/**
 * Created by Marcelo Cabezas on 2019-Apr-22 7:08:47 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using Logger.Logs;
using Logger.Printers;
using Utilities.Generics;

namespace Logger
{
    public class Logger : ILogger {

        private readonly Collection<IPrintable> _printers;
        private readonly Collection<ILoggable> _loggers;

        private readonly Type _sourceClass;

        public Logger(Type sourceClass)
        {
            _sourceClass = sourceClass;

            _printers = new Collection<IPrintable>{
                new ConsolePrinter(),
                new FilePrinter()
            };

            _loggers = new Collection<ILoggable> {
                new Info(),
                new Debug(),
                new Error()
            };
        }
        
        public Logger(Type sourceClass, Collection<ILoggable> loggers)
        {
            _sourceClass = sourceClass;
            _loggers = loggers;
        }

        public void Log(string message)
        {
            _printers.ForEach(printer => printer.Print(message));        
        }

        public void Info(string message)
        {
            Log<Info>(message);
        }
        
        public void Debug(string message)
        {
            Log<Debug>(message);
        }
        
        public void Error(string message)
        {
            Log<Error>(message);
        }

        private void  Log<T>(string message)
        {
            _loggers.ForEach(
                logType => logType is T,
                logType => logType.Log(this, 
                    _sourceClass + ": " + message)
            );
        }

    }
}