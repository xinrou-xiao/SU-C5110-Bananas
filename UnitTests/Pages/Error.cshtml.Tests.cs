using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Error
{
    /// <summary>
    /// Unit test class for testing the Error page model (Error.cshtml.cs).
    /// This class verifies the behavior of the OnGet method in various scenarios.
    /// </summary>
    public class ErrorTests
    {
        #region TestSetup

        // Holds an instance of ErrorModel, initialized in the setup method before each test.
        public static ErrorModel pageModel;

        /// <summary>
        /// Sets up the test environment for ErrorModel by initializing a mock logger and assigning required page context and TempData.
        /// This method runs before each test case.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Setting up a mock logger for the ErrorModel.
            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();

            // Initializing the ErrorModel page with the mock logger.
            pageModel = new ErrorModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Tests the OnGet method when a valid activity is set.
        /// Ensures that the RequestId is set to match the activity's ID, and ModelState is valid.
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange - Create and start an activity to simulate a real request.
            Activity activity = new Activity("activity");
            activity.Start();

            // Act - Call the OnGet method to test if it sets the RequestId from the activity.
            pageModel.OnGet();

            // Reset - Stop the activity to clean up after the test.
            activity.Stop();

            // Assert - Check if the model state is valid and if the RequestId matches the activity's ID.
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.RequestId, Is.EqualTo(activity.Id));
        }

        /// <summary>
        /// Tests the OnGet method when there is no active activity (simulating a null activity).
        /// Ensures that RequestId defaults to "trace" and that ShowRequestId is set to true.
        /// </summary>
        [Test]
        public void OnGet_InValid_Activity_Null_Should_Return_TraceIdentifier()
        {
            // Arrange - No activity is started here, simulating a null activity.

            // Act - Call the OnGet method to see if it handles a null activity.
            pageModel.OnGet();

            // Reset - Nothing to reset here.

            // Assert - Check if the model state is still valid, the RequestId is "trace" (as a fallback), and ShowRequestId is true.
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.RequestId, Is.EqualTo("trace"));
            Assert.That(pageModel.ShowRequestId, Is.EqualTo(true));
        }

        #endregion OnGet
    }
}