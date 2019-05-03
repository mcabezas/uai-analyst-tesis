/**
 * Created by Marcelo Cabezas on 2019-May-02 7:07:22 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Logger.States;
using Utilities.Generics;

namespace Logger.Configuration
{
    public class LoggerConfigurationHandler
    {
        private static readonly Collection<ConfigurationState> DebugStates = new Collection<ConfigurationState>
        {
            new LogDebugEnable(), 
            new LogDebugNotEnable(), 
        };
        
        private static readonly Collection<ConfigurationState> InfoStates = new Collection<ConfigurationState>
        {
            new LogInfoEnable(), 
            new LogInfoNotEnable(), 
        };
        
        private static readonly Collection<ConfigurationState> ErrorStates = new Collection<ConfigurationState>
        {
            new LogErrorEnable(), 
            new LogErrorNotEnable(), 
        };

        public static ConfigurationState ToHandleDebugState(Logger aLogger)
        {
            return DebugStates.Filter( state => state.CanHandle(aLogger)).GetFirst();
        }
        
        public static ConfigurationState ToHandleInfoState(Logger aLogger)
        {
            return InfoStates.Filter( state => state.CanHandle(aLogger)).GetFirst();
        }
        
        public static ConfigurationState ToHandleErrorState(Logger aLogger)
        {
            return ErrorStates.Filter( state => state.CanHandle(aLogger)).GetFirst();
        }
    }
}