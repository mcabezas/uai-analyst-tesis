/**
 * Created by Marcelo Cabezas on 2019-Apr-19 12:27:33 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using TesterSuite.Core.Enums;

namespace TesterSuite.Core.Exceptions
{
    public class TestResultStateException : Exception
    {

        #region Properties

        public virtual TestResultState TestResultState { get; set; }

        #endregion

        #region Constructor

        public TestResultStateException(string message, TestResultState state)
            : base(message)
        {
            TestResultState = state;
        }

        public TestResultStateException(string message, Exception inner, TestResultState state)
            : base(message, inner)
        {
            TestResultState = state;
        }

        #endregion

    }
}