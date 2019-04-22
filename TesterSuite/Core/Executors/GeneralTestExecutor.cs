/**
 * Created by Marcelo Cabezas on 2019-Apr-19 8:44:54 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System.Collections.Generic;
using TesterSuite.Core.Executors.Statistics;
using static TesterSuite.Core.Utilities.VType;

namespace TesterSuite.Core.Executors
{
    public class GeneralTestExecutor : IExecutor
    {
        public void Run()
        {
            StatisticsService statisticsService = new StatisticsService();
            IEnumerable<TestSuite> testSuites = GetAllTestSuites();
            foreach (TestSuite suite in testSuites)
            {
                suite.SucceedTest += statisticsService.OnSucceedTest;
                suite.FailedTest += statisticsService.OnFailedTest;

                suite.SetUpClass();
                suite.ExecuteTests();
                suite.CleanUpClass();
            }
            statisticsService.Print();
        }
    }
}