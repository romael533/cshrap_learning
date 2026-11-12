using Test_Run_Controller.Controllers;

namespace Test_Run_Controller.Services
{
    
    public class TestResultService : ITestResultService
    {
        private readonly List<TestResult> finalTestResults = new();        

        public void AddRange(List<TestResult> testResults)
        {
            if (testResults == null || testResults.Count == 0)
                throw new ArgumentException("TestResults cannot be null or empty.");
            finalTestResults.AddRange(testResults);
        }

        public IEnumerable<TestResult> Get(string? suite = null)
        {
            if (suite != null)
            {
                return finalTestResults.Where(r => r.Suite == suite).ToList();
            }
            return finalTestResults.ToList();
        }

        public TestSummary GetSummary()
        {
            int passedcount = 0;
            int failedcount = 0;
            foreach (var r in finalTestResults)
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
            int totalcount = finalTestResults.Count;
            int passpercentage = 0;
            if (totalcount != 0)
            {
                passpercentage = passedcount * 100 / totalcount;
            }
            return new TestSummary(totalcount, passedcount, failedcount, passpercentage);
        }

        public List<string> GetSlowest(int top)
        {
            var sorted = finalTestResults.OrderByDescending(b => b.Duration).ToList();
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
