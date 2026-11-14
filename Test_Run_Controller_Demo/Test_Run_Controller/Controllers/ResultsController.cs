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
        private readonly ILogger<ResultsController> _logger;

        public ResultsController(ITestResultService service, ILogger<ResultsController> logger) {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<TestResult> testResults)
        {
            if (testResults == null || testResults.Count == 0)
                return BadRequest("TestResults cannot be null or empty.");
            try
            {
                _service.AddRange(testResults);
                return Ok(testResults);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add {Count} test results.", testResults.Count);
                return Problem(
                    title: "Failed to save test results.",
                    detail: "An unexpected error occurred while saving test results. Please try again later.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult Get(string? suite = null)
        {
            try
            {
                return Ok(_service.Get(suite));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve test results for suite {Suite}.", suite);
                return Problem(
                    title: "Failed to retrieve test results.",
                    detail: "An unexpected error occurred while retrieving test results. Please try again later.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("/summary")]
        public IActionResult GetSummary()
        {
            try
            {
                return Ok(_service.GetSummary());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve test summary.");
                return Problem(
                    title: "Failed to retrieve test summary.",
                    detail: "An unexpected error occurred while retrieving the test summary. Please try again later.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("slowest")]
        public IActionResult GetSlowest(int top = 3)
        {
            try
            {
                return Ok(_service.GetSlowest(top));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve the {Top} slowest tests.", top);
                return Problem(
                    title: "Failed to retrieve slowest tests.",
                    detail: "An unexpected error occurred while retrieving the slowest tests. Please try again later.",
                    statusCode: StatusCodes.Status500InternalServerError);
            }           
        }
    }
}
