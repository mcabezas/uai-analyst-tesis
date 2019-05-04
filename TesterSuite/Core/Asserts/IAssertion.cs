/**
 * Created by Marcelo Cabezas on 2019-May-04 7:04:57 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;

namespace TesterSuite.Core.Asserts
{
    public interface IAssertion
    {
        void Fail(string message = null);
        void Pass();

        void AreEqual(IComparable expected, IComparable actual, string message = null);
        void AreNotEqual(IComparable expected, IComparable actual, string message = null);

        void IsTrue(bool value, string message = null);
        void IsFalse(bool value, string message = null);

        void IsNull(object value, string message = null);
        void IsNotNull(object value, string message = null);

        void AreSameReference(object expected, object actual, string message = null);
        void AreNotSameReference(object expected, object actual, string message = null);

    }
}