/**
 * Created by Marcelo Cabezas on 2019-Apr-19 12:26:50 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using TesterSuite.Core.Enums;

namespace TesterSuite.Core.Exceptions
{
    public class AssertException : TestResultStateException
    {

        #region Constructor

        public AssertException(string message, TestResultState state = TestResultState.Failure)
            : base(message, state) { }

        public AssertException(string message, Exception inner, TestResultState state = TestResultState.Failure)
            : base(message, inner, state) { }

        #endregion

    }
}