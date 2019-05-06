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

namespace TesterSuite.Core.Executors.impl
{
    public sealed class TestStatisticsService : ITestStatisticsService
    {
        private readonly ILogger _logger = new Logger(typeof(TestStatisticsService));
        private int SucceedTestsCounter { get; set; }
        private int FailedTestsCounter { get; set; }

        public void OnSucceedTest(object source, ITestEventArgs args)
        {
            SucceedTestsCounter++;
            _logger.Info("[SUCCESS] " + source + " => " + args.GetTestMethod().GetMethodInfo());
        }

        public void OnFailedTest(object source, ITestEventArgs args)
        {
            FailedTestsCounter++;
            _logger.Error("[FAILURE] " + source + " => " + args.GetTestMethod().GetMethodInfo());
        }

        public void Print()
        {
            _logger.Log("===============================================================================");
            _logger.Info(SucceedTestsCounter + FailedTestsCounter + " tests executed");
            _logger.Log( "Success: " + SucceedTestsCounter + " ; Failure: " + FailedTestsCounter);
        }
    }
}