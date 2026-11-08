        using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test_Run_Controller.Controllers
{
    public enum TestStatus { 
    Passed, Failed
    }
    public class TestResult {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Suite { get; set; } = "";
        public TestStatus Status { get; set; }
        public int Duration { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private static readonly List<TestResult> result = new();

        [HttpPost]
        public IActionResult Post([FromBody] TestResult testResult)
        {
            result.Add(testResult);
            return Ok(testResult);
        }

        [HttpGet]
        public IActionResult Get(string suite = null)
        {
            if (suite != null)
            {
                var finalresult = result.Where(r => r.Suite == suite).ToList();
                return Ok(finalresult);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpGet("/summary")]
        public IActionResult GetSummary()
        {
            int passedcount = 0;
            int failedcount = 0;
            for (int i = 0; i < result.Count; i++)
            {
                switch (result[i].Status)
                {
                    case TestStatus.Passed:
                        passedcount++;
                        break;
                    case TestStatus.Failed:
                        failedcount++;
                        break;
                }
            }
            int totalcount = result.Count;
            int passpercentage = 0;
            if (passedcount != 0)
            {
                passpercentage = passedcount * 100 / totalcount;
            }
            return Ok(new { TotalTestcases = totalcount, PassedTestcases = passedcount, FailedTestcases = failedcount, PassPercentage = passpercentage });
        }

        [HttpGet("slowest")]
        public IActionResult GetSlowest(int top = 3)
        {
            var sorted = result.OrderByDescending(b => b.Duration).ToList();
            var slowestlist = new List<String>();
            if (top > result.Count)
            {
                top = result.Count;
            }
            for (int i = 0; i < top; i++)
            {
                slowestlist.Add(sorted[i].Suite);
            }
            return Ok(slowestlist);           
        }
    }
}
