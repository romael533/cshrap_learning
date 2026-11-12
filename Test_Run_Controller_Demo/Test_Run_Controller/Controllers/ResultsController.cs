        using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_Run_Controller.Services;

namespace Test_Run_Controller.Controllers
{
    public enum TestStatus { 
    Passed, Failed, UnDetermined
    }
    public class TestResult {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Suite { get; set; } = string.Empty;
        public TestStatus Status { get; set; }
        public int Duration { get; set; }
    }
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ITestResultService _service;

        public ResultsController(ITestResultService service) {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<TestResult> testResults)
        {
            if (testResults == null || testResults.Count == 0)
                return BadRequest("TestResults cannot be null or empty.");
            _service.AddRange(testResults);
            return Ok(testResults);
        }

        [HttpGet]
        public IActionResult Get(string? suite = null)
        {
           return Ok(_service.Get(suite));
        }


        [HttpGet("/summary")]
        public IActionResult GetSummary()
        {
            return Ok(_service.GetSummary());
        }

        [HttpGet("slowest")]
        public IActionResult GetSlowest(int top = 3)
        {
            return Ok(_service.GetSlowest(top));           
        }
    }
}
