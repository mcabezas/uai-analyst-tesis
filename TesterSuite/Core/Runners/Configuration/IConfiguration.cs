/**
 * Created by Marcelo Cabezas on 2019-May-05 12:42:43 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using TesterSuite.Core.Runners.Runner;
using TesterSuite.Core.Suites;
using Utilities.Generics;

namespace TesterSuite.Core.Runners.Configuration
{
    public interface IConfiguration
    {
        bool CanHandle(IMCollection<string> configuration);
        IMCollection<ITestSuite> GetTestsSuites(IConfigurable aConfigurable);
    }
}