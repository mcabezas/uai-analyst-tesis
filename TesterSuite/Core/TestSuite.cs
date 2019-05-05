/**
 * Created by Marcelo Cabezas on 2019-Apr-19 10:57:27 AM
 * Student 11211 of UAI University
 *
 * Copyright 2019 - 2020 UAI Projects   
 */

using System;
using Logger;
using TesterSuite.Core.Asserts;
using Utilities;
using Utilities.Generics;

namespace TesterSuite.Core
{
    public abstract class TestSuite : ITestSuite
    {
        private readonly ILogger _logger = new Logger.Logger(typeof(TestSuite));
        protected readonly IAssertion Assertion = new Assertion();
        public virtual void SetUpClass() {}
        public virtual void CleanUpClass() {}
        public virtual void SetUp() {}
        public virtual void CleanUp() {}

        public abstract ICollection<Action> Tests();

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
            ICollection<Action> testMethods = Tests();
            if(Predefined.IsEmpty(testMethods)) {
                _logger.Info("[IGNORED] "+ this +" There are no tests to be executed here...");
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