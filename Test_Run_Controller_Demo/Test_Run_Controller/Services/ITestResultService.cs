using Test_Run_Controller.Controllers;

namespace Test_Run_Controller.Services
{
    public interface ITestResultService
    {
        void AddRange(List<TestResult> testResults);
        IEnumerable<TestResult> Get(string? suite = null);
        TestSummary GetSummary();
        List<string> GetSlowest(int top);

    }
}
