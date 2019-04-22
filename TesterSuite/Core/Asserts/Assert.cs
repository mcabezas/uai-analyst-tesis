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
    public class Assert
    {
        public Assert Fail(string message = null)
        {
            throw new NotImplementedException();
        }

        public Assert Pass(string message = null)
        {
            throw new NotImplementedException();
        }

        public Assert AreEqual(IComparable expected, IComparable actual, string message = null)
        {
            throw new NotImplementedException();
        }

        public Assert AreNotEqual(IComparable expected, IComparable actual, string message = null)
        {
            throw new NotImplementedException();
        }

        public virtual Assert IsTrue(bool value, string message = null)
        {
            if (!value)
            {
                throw new AssertException(ExpectedActualMessage(message, null, true, null, null, value, null));
            }

            return this;
        }

        public virtual Assert IsFalse(bool value, string message = null)
        {
            if (value)
            {
                throw new AssertException(ExpectedActualMessage(message, null, false, null, null, value, null));
            }

            return this;
        }

        public virtual Assert IsNull(object value, string message = null)
        {
            if (value != null)
            {
                throw new AssertException(ExpectedActualMessage(message, null, null, null, null, value, null));
            }

            return this;
        }

        public virtual Assert IsNotNull(object value, string message = null)
        {
            if (value == null)
            {
                throw new AssertException(ExpectedActualMessage(message, "Not ", null, null, null, null, null));
            }

            return this;
        }
        
        public virtual Assert AreSameReference(object expected, object actual, string message = null)
        {
            if (!ReferenceEquals(expected, actual))
            {
                throw new AssertException(ExpectedActualMessage(message, null, expected, null, null, actual, null));
            }

            return this;
        }
        
        public virtual Assert AreNotSameReference(object expected, object actual, string message = null)
        {
            if (ReferenceEquals(expected, actual))
            {
                throw new AssertException(ExpectedActualMessage(message, "Not ", expected, null, null, actual, null));
            }

            return this;
        }

        
        #region Protected Helpers
        protected virtual string ExpectedActualMessage(string preMessage, string preExpected, object expected, string postExpected, string preActual, object actual, string postActual)
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

        private string Safeguard(object value, string defaultValue = "")
        {
            return value == default(object)
                ? defaultValue
                : value.ToString();
        }


        private string BuildEnumerableString(IEnumerable items)
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