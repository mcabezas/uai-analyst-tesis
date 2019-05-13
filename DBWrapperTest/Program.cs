using TesterSuite.Core.Runners.Runner;
using TesterSuite.Core.Runners.Runner.impl;

namespace DBWrapperTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            ISuiteRunner suiteRunner = new SuiteRunner("DBWrapperTest", args);
            suiteRunner.ExecuteSuites();
        }
    }
}