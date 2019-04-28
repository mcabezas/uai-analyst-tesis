/**
 * Created by Marcelo Cabezas on 2019-Apr-19 12:23:43 PM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Collections;
using System.Text;
using TesterSuite.Core.Exceptions;

namespace TesterSuite.Core.Asserts
{
    public static class Assertion
    {
        public static void Fail(string message = null)
        {
            throw new AssertException(
                string.IsNullOrWhiteSpace(message)
                    ? "Test marked as failed"
                    : message);
        }

        public static void Pass()
        {
            //Dummy ... semantic method
        }

        public static void AreEqual(IComparable expected, IComparable actual, string message = null)
        {
            throw new NotImplementedException();
        }

        public static void AreNotEqual(IComparable expected, IComparable actual, string message = null)
        {
            throw new NotImplementedException();
        }

        public static void IsTrue(bool value, string message = null)
        {
            if (!value) {
                throw new AssertException(ExpectedActualMessage(message, null, true, null, null, false, null));
            }
        }

        public static void IsFalse(bool value, string message = null)
        {
            if (value) {
                throw new AssertException(ExpectedActualMessage(message, null, false, null, null, true, null));
            }
        }

        public static void IsNull(object value, string message = null)
        {
            if (value != null) {
                throw new AssertException(ExpectedActualMessage(message, null, null, null, null, value, null));
            }
        }

        public static void IsNotNull(object value, string message = null)
        {
            if (value == null) {
                throw new AssertException(ExpectedActualMessage(message, "Not ", null, null, null, null, null));
            }
        }
        
        public static void AreSameReference(object expected, object actual, string message = null)
        {
            if (!ReferenceEquals(expected, actual)) {
                throw new AssertException(ExpectedActualMessage(message, null, expected, null, null, actual, null));
            }

        }
        
        public static void AreNotSameReference(object expected, object actual, string message = null)
        {
            if (ReferenceEquals(expected, actual))
            {
                throw new AssertException(ExpectedActualMessage(message, "Not ", expected, null, null, actual, null));
            }
        }

        
        #region Protected Helpers

        private static string ExpectedActualMessage(string preMessage, string preExpected, object expected, string postExpected, string preActual, object actual, string postActual)
        {
            if (expected != default(object) && !(expected is string) && (expected is IEnumerable))
            {
                expected = BuildEnumerableString((IEnumerable)expected);
            }

            if (actual != default(object) && !(actual is string) && (actual is IEnumerable))
            {
                actual = BuildEnumerableString((IEnumerable)actual);
            }

            return string.Format("{1}{0}Expected:\t{2}{0}But was:\t{3}",
                Environment.NewLine,
                string.IsNullOrWhiteSpace(preMessage) ? "TestSuite assertion failed" : preMessage,
                Safeguard(preExpected) + Safeguard(expected, "NULL") + Safeguard(postExpected),
                Safeguard(preActual) + Safeguard(actual, "NULL") + Safeguard(postActual));
        }

        private static string Safeguard(object value, string defaultValue = "")
        {
            return value == default(object)
                ? defaultValue
                : value.ToString();
        }


        private static string BuildEnumerableString(IEnumerable items)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in items)
            {
                builder.Append(item + ", ");
            }

            return builder.ToString().Replace(Environment.NewLine, string.Empty).Trim('\n', ',', ' ', '\r');
        }

        #endregion
    }
}