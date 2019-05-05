/**
 * Created by Marcelo Cabezas on 2019-Apr-19 8:44:54 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */


using System.Collections.Generic;

namespace TesterSuite.Core.Executors
{
    public interface IExecutor
    {
        void ExecuteSuites();
        void Configure(IEnumerable<string> tests);
    }
}