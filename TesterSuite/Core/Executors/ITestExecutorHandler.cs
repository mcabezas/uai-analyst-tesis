/**
 * Created by Marcelo Cabezas on 2019-May-05 12:05:59 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Utilities.Generics;

namespace TesterSuite.Core.Executors
{
    public interface ITestExecutorHandler
    {
        IConfiguration ToHandleTestExecutor(IMCollection<string> testSuites);
    }
}