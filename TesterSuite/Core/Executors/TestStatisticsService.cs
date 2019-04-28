/**
 * Created by Marcelo Cabezas on 2019-Apr-19 8:51:29 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System.Reflection;
using Logger;

namespace TesterSuite.Core.Executors
{
    public class TestStatisticsService
    {
        private readonly Logger.Logger _logger = LoggerFactory.Instance.GetLogger(typeof(TestStatisticsService));
        private int SucceedTestsCounter { get; set; }
        private int FailedTestsCounter { get; set; }

        public void OnSucceedTest(object source, TestEventArgs args)
        {
            SucceedTestsCounter++;
            _logger.Info("[SUCCESS] " + source + " => " + args.TestMethod.GetMethodInfo());
        }

        public void OnFailedTest(object source, TestEventArgs args)
        {
            FailedTestsCounter++;
            _logger.Error("[FAILURE] " + source + " => " + args.TestMethod.GetMethodInfo());
        }

        public void Print()
        {
            Logger.Logger.PrintLog("===============================================================================");
            _logger.Info(SucceedTestsCounter + FailedTestsCounter + " tests executed");
            _logger.Log( FailedTestsCounter > 0 ? LogLevel.Error:LogLevel.Info, "Success: " + SucceedTestsCounter + " ; Failure: " + FailedTestsCounter);
        }
    }
}