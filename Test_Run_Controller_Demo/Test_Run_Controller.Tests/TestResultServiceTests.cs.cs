using System;
using System.Collections.Generic;
using System.Text;
using Test_Run_Controller.Services;
using Test_Run_Controller.Controllers;

namespace Test_Run_Controller.Tests
{
    public class TestResultServiceTests
    {
        [Fact]
        public void TestResultService_AddRange_ShouldAddResults()
        {
            // Arrange
            var service = new TestResultService();
            var testResults = new List<TestResult>
            {
                new TestResult { Suite = "Suite1", Status = TestStatus.Passed, Duration = 10 },
                new TestResult { Suite = "Suite2", Status = TestStatus.Failed, Duration = 20 }
            };
            // Act
            service.AddRange(testResults);
            var results = service.Get();
            // Assert
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public void TestResultService_Get_ShouldReturnAllResults_WhenNoSuiteIsProvided()
        { 
            //Arrange
            var service = new TestResultService();
            var testResults = new List<TestResult> {
            new TestResult { Id ='1', Name = "prod", Suite = "quick alert1", Status = TestStatus.Passed, Duration = 90  },
            new TestResult { Id ='2', Name = "prod", Suite = "quick alert2", Status = TestStatus.Passed, Duration = 110  },
            new TestResult { Id ='3', Name = "prod", Suite = "quick alert3", Status = TestStatus.Passed, Duration = 75 },
            };
            service.finalTestResults.AddRange(testResults);

            //Act
            var results = service.Get();

            //Assert
            Assert.Equal(3, results.Count());
        }

        [Fact]
        public void TestResultService_Get_ShouldReturnFilteredResults_WhenSuiteIsProvided()
        {
            //Arrange
            var service = new TestResultService();
            var testResults = new List<TestResult> {
            new TestResult { Id ='1', Name = "prod", Suite = "quick alert1", Status = TestStatus.Passed, Duration = 90  },
            new TestResult { Id ='2', Name = "prod", Suite = "quick alert2", Status = TestStatus.Passed, Duration = 110  },
            new TestResult { Id ='3', Name = "prod", Suite = "quick alert3", Status = TestStatus.Passed, Duration = 75 },
            };
            service.finalTestResults.AddRange(testResults);

            //Act
            var results = service.Get("quick alert1");

            //Assert
            Assert.Equal(1, results.Count());
            Assert.Equal("quick alert1", results.First().Suite);
        }

        [Fact]
        public void TestResultService_Get_ShouldReturnEmpty_WhenSuiteDoesNotExist()
        {
            //Arrange
            var service = new TestResultService();
            var testResults = new List<TestResult> {
            new TestResult { Id ='1', Name = "prod", Suite = "quick alert1", Status = TestStatus.Passed, Duration = 90  },
            new TestResult { Id ='2', Name = "prod", Suite = "quick alert2", Status = TestStatus.Passed, Duration = 110  }
            };
            service.finalTestResults.AddRange(testResults);

            //Act
            var results = service.Get("quick alert3");

            //Assert
            Assert.Equal(0, results.Count());
        }

        [Fact]
        public void TestResultService_GetSummary_ShouldReturnCorrectSummary()
        {
            //Arrange
            var service = new TestResultService();
            var testResults = new List<TestResult> {
            new TestResult { Id ='1', Name = "prod", Suite = "quick alert1", Status = TestStatus.Passed, Duration = 90  },
            new TestResult { Id ='2', Name = "prod", Suite = "quick alert2", Status = TestStatus.Failed, Duration = 110  },
            new TestResult { Id ='3', Name = "prod", Suite = "quick alert3", Status = TestStatus.Passed, Duration = 75 },
            };
            service.finalTestResults.AddRange(testResults);
            //Act
            var summary = service.GetSummary();
            //Assert
            Assert.Equal(3, summary.totalCount);
            Assert.Equal(2, summary.passedCount);
            Assert.Equal(1, summary.failedCount);
            Assert.Equal(66, summary.passPercentage);
        }
        [Fact]
        public void TestResultService_GetSummary_ShouldReturnZeroPassPercentage_WhenNoTestsPassed()
        {
            //Arrange
            var service = new TestResultService();
            var testResults = new List<TestResult> {
            new TestResult { Id ='1', Name = "prod", Suite = "quick alert1", Status = TestStatus.Failed, Duration = 90  },
            new TestResult { Id ='2', Name = "prod", Suite = "quick alert2", Status = TestStatus.Failed, Duration = 110  },
            new TestResult { Id ='3', Name = "prod", Suite = "quick alert3", Status = TestStatus.Failed, Duration = 75 },
            };
            service.finalTestResults.AddRange(testResults);
            //Act
            var summary = service.GetSummary();
            //Assert
            Assert.Equal(3, summary.totalCount);
            Assert.Equal(0, summary.passedCount);
            Assert.Equal(3, summary.failedCount);
            Assert.Equal(0, summary.passPercentage);
        }
        [Fact]
        public void TestResultService_GetSlowest_ShouldReturnSlowestTestResult()
        {
            //Arrange
            var service = new TestResultService();
            var testResults = new List<TestResult> {
            new TestResult { Id ='1', Name = "prod", Suite = "quick alert1", Status = TestStatus.Failed, Duration = 90  },
            new TestResult { Id ='2', Name = "prod", Suite = "quick alert2", Status = TestStatus.Passed, Duration = 110  },
            new TestResult { Id ='3', Name = "prod", Suite = "quick alert3", Status = TestStatus.Passed, Duration = 75 },
            };
            service.finalTestResults.AddRange(testResults);
            //Act
            var summary = service.GetSlowest(2);
            //Assert
            Assert.Equal(2, summary.Count());
            Assert.Equal(["quick alert2", "quick alert1"], summary);
        }

        [Fact]
        public void TestResultService_GetSlowest_ShouldNotFailWhenSearchValueMoreThanAutualResult()
        {
            //Arrange
            var service = new TestResultService();
            var testResults = new List<TestResult> {
            new TestResult { Id ='1', Name = "prod", Suite = "quick alert1", Status = TestStatus.Failed, Duration = 90  },
            new TestResult { Id ='2', Name = "prod", Suite = "quick alert2", Status = TestStatus.Failed, Duration = 110  },
            new TestResult { Id ='3', Name = "prod", Suite = "quick alert3", Status = TestStatus.Passed, Duration = 75 },
            new TestResult { Id ='4', Name = "prod", Suite = "quick alert4", Status = TestStatus.Passed, Duration = 290 },
            };
            service.finalTestResults.AddRange(testResults);
            //Act
            var summary = service.GetSlowest(5);
            //Assert
            Assert.Equal(4, summary.Count());
            Assert.Equal(["quick alert4", "quick alert2", "quick alert1", "quick alert3"], summary);
        }
    }
}
