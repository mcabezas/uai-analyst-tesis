/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;

namespace TesterSuite.Core.Suites
{
    public interface ITestSuite
    {
        void SetUpClass();
        void CleanUpClass();

        void ExecuteTests();

        event EventHandler<ITestEventArgs> SucceedTest;
        event EventHandler<ITestEventArgs> FailedTest;


    }
    
    public interface ITestEventArgs
    {
        Action GetTestMethod();
    }
}