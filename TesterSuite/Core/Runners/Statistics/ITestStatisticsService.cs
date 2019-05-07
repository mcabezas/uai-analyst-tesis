/**
 * Created by Marcelo Cabezas on 2019-Apr-19 8:51:29 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using TesterSuite.Core.Suites;

namespace TesterSuite.Core.Runners.Statistics
{
    public interface ITestStatisticsService
    {
        void OnSucceedTest(object source, ITestEventArgs args);
        
        void OnFailedTest(object source, ITestEventArgs args);
        
        void Print();
    }
}