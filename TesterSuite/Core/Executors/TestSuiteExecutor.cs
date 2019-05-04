/**
 * Created by Marcelo Cabezas on 2019-Apr-19 8:44:54 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using Utilities.Generics;

namespace TesterSuite.Core.Executors
{
    public class TestSuiteExecutor
    {

        public void ExecuteSuite(TestSuite suite)
        {
            PrepareSuite(statisticsService => {
                    RunSuite(suite, statisticsService);
            });
        }

        public void ExecuteSuite(ICollection<TestSuite> suites)
        {
            PrepareSuite(statisticsService => {
                suites.ForEach(suite => {
                    RunSuite(suite, statisticsService);
                });
            });
        }

        private static void PrepareSuite(Action<TestStatisticsService> action)
        {
            TestStatisticsService statisticsService= new TestStatisticsService();
            action(statisticsService);
            statisticsService.Print();
        }

        private static void RunSuite(TestSuite suite, TestStatisticsService statisticsService)
        {
            suite.SucceedTest += statisticsService.OnSucceedTest;
            suite.FailedTest += statisticsService.OnFailedTest;

            suite.SetUpClass();
            suite.ExecuteTests();
            suite.CleanUpClass();
        }
    }
}