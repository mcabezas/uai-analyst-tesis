/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using Commons;
using Commons.Generics;
using Log4CS.Core;
using Log4CS.Core.impl;
using TesterSuite.Core.Asserts;
using TesterSuite.Core.Asserts.impl;

namespace TesterSuite.Core.Suites.impl
{
    public abstract class TestSuite : ITestSuite
    {
        private readonly ILogger _logger = new Logger(typeof(TestSuite));
        protected readonly IAssertion Assertion = new Assertion();
        public virtual void SetUpClass() {}
        public virtual void CleanUpClass() {}
        protected virtual void SetUp() {}
        protected virtual void CleanUp() {}

        protected abstract IMCollection<Action> Tests();

        #region events
        
        #region OnSucceedTest

        public event EventHandler<ITestEventArgs> SucceedTest;

        private void OnSucceedTest(Action testMethod)
        {
            SucceedTest?.Invoke(this, new TestEventArgs(testMethod));
        }
        
        #endregion
        
        #region OnFailedTest
        
        public event EventHandler<ITestEventArgs> FailedTest;

        private void OnFailedTest(Action testMethod)
        {
            FailedTest?.Invoke(this, new TestEventArgs(testMethod));
        }
        
        #endregion
        #endregion

        public void ExecuteTests()
        {
            IMCollection<Action> testMethods = Tests();
            if(Predefined.IsEmpty(testMethods)) {
                _logger.Log("[IGNORED] "+ this +" There are no tests to be executed here...");
                return;
            }

            if (IgnoreSuite())
            {
                _logger.Log("[IGNORED] "+ this +" This suite is marked to be ignored");
                return;
            }

            testMethods.ForEach(testMethod => {
                try {
                    SetUp();
                    testMethod();
                    CleanUp();
                    OnSucceedTest(testMethod);
                } catch (Exception e) {
                    OnFailedTest(testMethod);
                    _logger.Error(e.Message);
                }
            });
        }

        protected virtual bool IgnoreSuite() {
            return false;
        }

    }

    public class TestEventArgs : ITestEventArgs
    {
        private readonly Action _testMethod;

        public TestEventArgs(Action testMethod)
        {
            _testMethod = testMethod;
        }

        public Action GetTestMethod()
        {
            return _testMethod;
        }
    }
    
}