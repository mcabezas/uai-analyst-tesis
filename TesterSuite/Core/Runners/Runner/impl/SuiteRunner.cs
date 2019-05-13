/**
 * Created by Marcelo Cabezas on 2019-Apr-19 8:44:54 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Collections.Generic;
using Commons.Generics;
using Commons.Generics.impl;
using Log4CS.Core;
using Log4CS.Core.impl;
using TesterSuite.Core.Runners.Configuration;
using TesterSuite.Core.Runners.Configuration.impl;
using TesterSuite.Core.Runners.Statistics;
using TesterSuite.Core.Runners.Statistics.impl;
using TesterSuite.Core.Runners.SuiteFinder;
using TesterSuite.Core.Runners.SuiteFinder.impl;
using TesterSuite.Core.Suites;

namespace TesterSuite.Core.Runners.Runner.impl
{
    public sealed class SuiteRunner : ISuiteRunner, IConfigurable
    {
        private readonly ILogger _logger = new Logger(typeof(SuiteRunner));

        private readonly IMCollection<string> _configuration;

        private readonly string _module;
        public SuiteRunner(string module, IEnumerable<string> tests)
        {
            _configuration = new MCollection<string>().From(tests);
            _module = module;
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
            IConfigurationHandler configurationHandler = new ConfigurationHandler();
            
            IConfiguration configuration = configurationHandler.ToHandleTestConfiguration(_configuration);
            
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
            ISuiteFinder finder = new SuiteAssemblyFinder();
            return finder.GetAllTestSuites(_module);
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