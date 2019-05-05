/**
 * Created by Marcelo Cabezas on 2019-Apr-19 8:44:54 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Collections.Generic;
using Log4CS.Core;
using Log4CS.Core.impl;
using TesterSuite.Core.Suites;
using Utilities.Generics;
using Utilities.Generics.impl;

namespace TesterSuite.Core.Executors.impl
{
    public class TestSuiteExecutor : IExecutor, IConfigurable
    {
        private readonly ILogger _logger = new Logger(typeof(TestSuiteExecutor));

        private IMCollection<string> _configuration;

        public void Configure(IEnumerable<string> tests)
        {
            _configuration = new MCollection<string>().From(tests);
        }
        
        public void ExecuteSuites()
        {
            IMCollection<ITestSuite> suites = GetTestSuites();

            PrepareSuite(statisticsService => {
                suites.ForEach(suite => {
                    RunSuite(suite, statisticsService);
                });
            });
        }

        private IMCollection<ITestSuite> GetTestSuites()
        {
            ITestExecutorHandler configurationHandler = new TestExecutorHandler();
            
            IConfiguration configuration = configurationHandler.ToHandleTestExecutor(_configuration);
            
            return configuration.GetTestsSuites(this);
        }

        private static void PrepareSuite(Action<ITestStatisticsService> action)
        {
            ITestStatisticsService statisticsService= new TestStatisticsService();
            
            action(statisticsService);
            
            statisticsService.Print();
        }

        private static void RunSuite(ITestSuite suite, ITestStatisticsService statisticsService)
        {
            suite.SucceedTest += statisticsService.OnSucceedTest;
            suite.FailedTest += statisticsService.OnFailedTest;

            suite.SetUpClass();
            suite.ExecuteTests();
            suite.CleanUpClass();
        }

        public IMCollection<ITestSuite> GetDefaultTests()
        {
            ITestSuiteFinder testFinder = new TestSuiteAssemblyFinder();
            return testFinder.GetAllTestSuites();
        }

        public IMCollection<ITestSuite> GetTestsByConfiguration()
        {
            IMCollection<ITestSuite> suites = new MCollection<ITestSuite>();

            _configuration.ForEach(suiteName => {
                try {
                    Type type = Type.GetType(suiteName);
                    suites.Add((ITestSuite) Activator.CreateInstance(type));
                } catch {
                    _logger.Error("TestSuite [" + suiteName + "] Not found");
                }
            });
            return suites;
        }
    }
}