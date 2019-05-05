/**
 * Created by Marcelo Cabezas on 2019-May-05 12:05:59 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Utilities.Generics;
using Utilities.Generics.impl;

namespace TesterSuite.Core.Executors.impl
{
    public class TestExecutorHandler : ITestExecutorHandler
    {
        private static readonly IMCollection<IConfiguration> Executors = new MCollection<IConfiguration>
        {
            new DefaultConfiguration(), 
            new CustomConfiguration()
        };
        
        public IConfiguration ToHandleTestExecutor(IMCollection<string> args)
        {
            return Executors.Filter(executor => executor.CanHandle(args)).GetFirst();
        }
    }
}