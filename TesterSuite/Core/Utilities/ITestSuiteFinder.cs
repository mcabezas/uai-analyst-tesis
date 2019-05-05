/**
 * Created by Marcelo Cabezas on 2019-May-04 8:26:15 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Utilities.Generics;

namespace TesterSuite.Core.Utilities
{
    public interface ITestSuiteFinder
    {
        IMCollection<ITestSuite> GetAllTestSuites();
    }
}