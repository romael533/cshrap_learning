using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Test_Run_Controller.Controllers;
using Test_Run_Controller.Services;

namespace Test_Run_Controller.Tests
{
    public class ResultsControllerTests
    {
        [Fact]
        public void Get_NoResults_ReturnsOkWithEmptyList()
        {
            // Arrange
            var service = new TestResultService();
            var controller = new ResultsController(service, NullLogger<ResultsController>.Instance);

            // Act
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<TestResult>>(okResult.Value);
            Assert.Empty(data);
        }

        [Fact]
        public void Get_TwoResults_ReturnsOkWithBothResults()
        {
            // Arrange
            var service = new TestResultService();
            service.finalTestResults.AddRange(new List<TestResult> {
                new TestResult { Id = 1, Name = "prod", Suite = "quick alert1", Status = TestStatus.Passed, Duration = 90 },
                new TestResult { Id = 2, Name = "prod", Suite = "quick alert2", Status = TestStatus.Failed, Duration = 110 },
            });
            var controller = new ResultsController(service, NullLogger<ResultsController>.Instance);

            // Act
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var data = Assert.IsAssignableFrom<IEnumerable<TestResult>>(okResult.Value);
            Assert.Equal(2, data.Count());
        }

        [Fact]
        public void Post_EmptyList_ReturnsBadRequest()
        {
            // Arrange
            var service = new TestResultService();
            var controller = new ResultsController(service, NullLogger<ResultsController>.Instance);

            // Act
            var result = controller.Post(new List<TestResult>());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Get_ServiceThrows_Returns500()
        {
            // Arrange
            var controller = new ResultsController(new ThrowingService(), NullLogger<ResultsController>.Instance);

            // Act
            var result = controller.Get();

            // Assert
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        // Fake service whose Get method always fails, to simulate a service error.
        private class ThrowingService : ITestResultService
        {
            public void AddRange(List<TestResult> testResults) => throw new NotImplementedException();
            public IEnumerable<TestResult> Get(string? suite = null) => throw new InvalidOperationException("Service failure");
            public TestSummary GetSummary() => throw new NotImplementedException();
            public List<string> GetSlowest(int top) => throw new NotImplementedException();
        }
    }
}
