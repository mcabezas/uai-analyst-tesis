/**
 * Created by Marcelo Cabezas on 2019-May-05 12:05:59 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Utilities.Generics;
using Utilities.Generics.impl;

namespace TesterSuite.Core.Runners.Configuration.impl
{
    public sealed class ConfigurationHandler : IConfigurationHandler
    {
        private static readonly IMCollection<IConfiguration> Executors = new MCollection<IConfiguration>
        {
            new DefaultConfiguration(), 
            new CustomConfiguration()
        };
        
        public IConfiguration ToHandleTestConfiguration(IMCollection<string> args)
        {
            return Executors.Filter(configuration => configuration.CanHandle(args)).GetFirst();
        }
    }
}