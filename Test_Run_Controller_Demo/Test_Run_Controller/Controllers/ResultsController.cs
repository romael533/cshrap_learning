        using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_Run_Controller.Services;

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
        private readonly ITestResultService _service;

        public ResultsController(ITestResultService service) {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<TestResult> testResult)
        {
            if (testResult == null || testResult.Count == 0)
                return BadRequest("TestResult cannot be null or empty.");
            _service.AddRange(testResult);
            return Ok(testResult);
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
