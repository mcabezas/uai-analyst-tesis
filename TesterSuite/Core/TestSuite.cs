/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using System.Collections.Generic;
using Logger;
using TesterSuite.Core.Exceptions;

namespace TesterSuite.Core
{
    public abstract class TestSuite
    {
        private readonly Logger.Logger _logger = LoggerFactory.Instance.GetLogger(typeof(TestSuite));
        public virtual void SetUpClass() {}
        public virtual void CleanUpClass() {}
        public virtual void SetUp() {}
        public virtual void CleanUp() {}

        protected abstract List<Action> Test();

        #region events
        
        #region OnSucceedTest
        
        public delegate void SucceedTestEventHandler(object source, TestEventArgs args);
        public event SucceedTestEventHandler SucceedTest;
        protected virtual void OnSucceedTest(Action testMethod)
        {
            SucceedTest?.Invoke(this, new TestEventArgs() {TestMethod = testMethod});
        }
        
        #endregion
        
        #region OnFailedTest
        
        public delegate void FailedTestEventHandler(object source, TestEventArgs args);
        public event FailedTestEventHandler FailedTest;
        protected virtual void OnFailedTest(Action testMethod)
        {
            FailedTest?.Invoke(this, new TestEventArgs() {TestMethod = testMethod});
        }
        
        #endregion
        #endregion

        public void ExecuteTests()
        {
            List<Action> testMethods = Test();
            if (testMethods == null || testMethods.Count==0)
            {
                _logger.Info("[IGNORED] "+ this +" There are no tests to be executed here...");
                return;
            }

            foreach (Action testMethod in testMethods)
            {
                try {
                    SetUp();
                    testMethod();
                    CleanUp();
                    OnSucceedTest(testMethod);
                } catch (AssertException e) {
                    OnFailedTest(testMethod);
                    _logger.Error(e.Message);
                }
            }
        }
    }

    public class TestEventArgs : EventArgs
    {
        public Action TestMethod { get; set; }
    }
}