/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using TesterSuite.Core.Executors;
using TesterSuite.Core.Utilities;
using TesterSuite.ORMTest;
using Utilities.Generics;
using static TesterSuite.Core.Utilities.TestSuiteAssemblyFinder;

namespace TesterSuite
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            //Collection<string> objects = Collection<string>.From(args);
            //suiteExecutor.ExecuteSuite(new SessionFactoryTest());
            ITestSuiteFinder suiteFinder = new TestSuiteAssemblyFinder();
            
            TestSuiteExecutor suiteExecutor = new TestSuiteExecutor();
            suiteExecutor.ExecuteSuite(suiteFinder.GetAllTestSuites());
        }
    }
}