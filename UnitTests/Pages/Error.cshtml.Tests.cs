using System.Diagnostics;

using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Error
{
    public class ErrorTests
    {
        #region TestSetup
        // This region is for setting up the testing environment before each test runs.
        public static ErrorModel pageModel;

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
        // Testing the OnGet method when there's a valid activity.
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange- Create and start an activity to simulate a real request.

            Activity activity = new Activity("activity");
            activity.Start();

            // Act- Call the OnGet method to test if it sets the RequestId from the activity.
            pageModel.OnGet();

            // Reset- Stop the activity to clean up after the test.
            activity.Stop();

            // Assert- Check if the model state is valid and if the RequestId matches the activity's ID.
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.RequestId, Is.EqualTo(activity.Id));
        }

        [Test]
        public void OnGet_InValid_Activity_Null_Should_Return_TraceIdentifier()
        {
            // Arrange- No activity is started here, simulating a null activity.

            // Act- Call the OnGet method to see if it handles a null activity.
            pageModel.OnGet();

            // Reset- Nothing to reset here.

            // Assert- Check if the model state is still valid, the RequestId is "trace" (as a fallback), and ShowRequestId is true.
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.RequestId, Is.EqualTo("trace"));
            Assert.That(pageModel.ShowRequestId, Is.EqualTo(true));
        }
        #endregion OnGet
    }
}