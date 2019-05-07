/**
 * Created by Marcelo Cabezas on 2019-Apr-19 8:51:29 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System.Reflection;
using Log4CS.Core;
using Log4CS.Core.impl;
using TesterSuite.Core.Suites;
using Utilities.Generics;
using Utilities.Generics.impl;

namespace TesterSuite.Core.Runners.Statistics.impl
{
    public sealed class TestStatisticsService : ITestStatisticsService
    {
        private readonly ILogger _logger = new Logger(typeof(TestStatisticsService));
        private readonly IMCollection<ITestCaseState> _testCasesStatistics;

        public TestStatisticsService()
        {
            _testCasesStatistics = new MCollection<ITestCaseState>();
        }

        public void OnSucceedTest(object source, ITestEventArgs args)
        {
            _testCasesStatistics.Add(new TestCaseStateSucceed());
            _logger.Info("[SUCCESS] " + source + " => " + args.GetTestMethod().GetMethodInfo());
        }

        public void OnFailedTest(object source, ITestEventArgs args)
        {
            _testCasesStatistics.Add(new TestCaseStateFailure());
            _logger.Error("[FAILURE] " + source + " => " + args.GetTestMethod().GetMethodInfo());
        }

        public void Print()
        {
            int failureTestCases = _testCasesStatistics.Filter(testCase => !testCase.IsSucceed()).Count;
            int succeedTestCases = _testCasesStatistics.Filter(testCase => testCase.IsSucceed()).Count;

            _logger.Log("===============================================================================");
            _logger.Log("[" + _testCasesStatistics.Count + " tests executed]");
            _logger.Log( "Success: " + succeedTestCases + " ; Failure: " + failureTestCases );
        }
    }
}