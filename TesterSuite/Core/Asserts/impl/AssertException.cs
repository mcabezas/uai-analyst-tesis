/**
 * Created by Marcelo Cabezas on 2019-Apr-19 12:26:50 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;

namespace TesterSuite.Core.Asserts.impl
{
    public sealed class AssertException : Exception
    {

        #region Constructor

        public AssertException(string message)
            : base(message) { }

        #endregion

    }
}