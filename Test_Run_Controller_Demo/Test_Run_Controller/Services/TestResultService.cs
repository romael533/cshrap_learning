using Microsoft.EntityFrameworkCore;
using Test_Run_Controller.Controllers;
using Test_Run_Controller.Data;

namespace Test_Run_Controller.Services
{
    
    public class TestResultService : ITestResultService
    {
        private readonly AppDbContext _db;
        public TestResultService(AppDbContext db)
        {
            _db = db;
        }
        public void AddRange(List<TestResult> testResults)
        {
            if (testResults == null || testResults.Count == 0)
                throw new ArgumentException("TestResults cannot be null or empty.");
            _db.TestResults.AddRange(testResults);
            _db.SaveChanges();
        }

        public IEnumerable<TestResult> Get(string? suite = null)
        {
            if (suite != null)
            {
                return _db.TestResults.Where(r => r.Suite == suite).ToList();
            }
            return _db.TestResults.ToList();
        }

        public TestSummary GetSummary()
        {
            int passedcount = 0;
            int failedcount = 0;
            foreach (var r in _db.TestResults)
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
            int totalcount = _db.TestResults.Count();
            int passpercentage = 0;
            if (totalcount != 0)
            {
                passpercentage = passedcount * 100 / totalcount;
            }
            return new TestSummary(totalcount, passedcount, failedcount, passpercentage);
        }

        public List<string> GetSlowest(int top)
        {
            var sorted = _db.TestResults.OrderByDescending(b => b.Duration).ToList();
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
