using TesterSuite.Core.Runners.Runner;
using TesterSuite.Core.Runners.Runner.impl;

namespace SecurityTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            ISuiteRunner suiteRunner = new SuiteRunner("SecurityTest", args);
            suiteRunner.ExecuteSuites();
        }
    }
}