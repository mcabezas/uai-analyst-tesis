/**
 * Created by Marcelo Cabezas on 2019-Apr-19 8:44:54 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using Utilities.Generics;

namespace TesterSuite.Core.Executors.implementation
{
    public class CustomConfiguration : IConfiguration
    {
        public bool CanHandle(IMCollection<string> tests)
        {
            return !tests.IsEmpty();
        }

        public IMCollection<ITestSuite> GetTestsSuites(IConfigurable aConfigurable)
        {
            return aConfigurable.GetTestsByConfiguration();
        }
        
    }
}