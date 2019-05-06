/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using TesterSuite.Core.Executors;
using TesterSuite.Core.Executors.impl;

namespace TesterSuite
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            IExecutor suiteExecutor = new TestSuiteExecutor(args);
            suiteExecutor.ExecuteSuites();
        }
    }
}