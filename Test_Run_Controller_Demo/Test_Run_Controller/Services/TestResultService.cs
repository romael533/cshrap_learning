using Test_Run_Controller.Controllers;
using static Test_Run_Controller.Services.TestResultService;

namespace Test_Run_Controller.Services
{
    public record TestSummary(int totalCount, int passedCount, int failedCount, int passPercentage);
    public interface ITestResultService
    {
        void AddRange(List<TestResult> testResult);
        IEnumerable<TestResult> Get(string? suite = null);
        TestSummary GetSummary();
        List<string> GetSlowest(int top);
    }
    public class TestResultService : ITestResultService
    {
        private readonly List<TestResult> _results = new();        

        public void AddRange(List<TestResult> testResult)
        {
            if (testResult == null || testResult.Count == 0)
                throw new ArgumentException("TestResult cannot be null or empty.");
            _results.AddRange(testResult);
        }

        public IEnumerable<TestResult> Get(string? suite = null)
        {
            if (suite != null)
            {
                return _results.Where(r => r.Suite == suite).ToList();
            }
            return _results.ToList();
        }

        public TestSummary GetSummary()
        {
            int passedcount = 0;
            int failedcount = 0;
            foreach (var r in _results)
            {
                switch (r.Status)
                {
                    case TestStatus.Passed:
                        passedcount++;
                        break;
                    case TestStatus.Failed:
                        failedcount++;
                        break;
                }
            }
            int totalcount = _results.Count;
            int passpercentage = 0;
            if (totalcount != 0)
            {
                passpercentage = passedcount * 100 / totalcount;
            }
            return new TestSummary(totalcount, passedcount, failedcount, passpercentage);
        }

        public List<string> GetSlowest(int top)
        {
            var sorted = _results.OrderByDescending(b => b.Duration).ToList();
            if (top > sorted.Count)
            {
                top = sorted.Count;
            }
            var slowestlist = new List<string>();
            for (int i = 0; i < top; i++)
            {
                slowestlist.Add(sorted[i].Suite);
            }
            return slowestlist;
        }
    }
}
