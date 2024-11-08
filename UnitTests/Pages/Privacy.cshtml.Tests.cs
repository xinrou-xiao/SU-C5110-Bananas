using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Privacy
{
    /// <summary>
    /// This class contains unit tests to validate the functionality and logic within the Privacy.cshtml.cs.
    /// </summary>
    public class PrivacyTests
    {
        #region TestSetup

        // Static instance of PrivacyModel used in tests, initialized in TestInitialize method.
        public static PrivacyModel pageModel;

        /// <summary>
        /// Sets up the test environment for PrivacyModel tests by initializing 
        /// a mock logger and configuring required page context and TempData properties.
        /// This method is executed before each test.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Create a mock logger for PrivacyModel to avoid the need for a real logger instance
            var MockLoggerDirect = Mock.Of<ILogger<PrivacyModel>>();

            // Initialize PrivacyModel with mock logger and necessary page context
            pageModel = new PrivacyModel(MockLoggerDirect)
            {
                // Provides context for the page, allowing simulation of HTTP requests
                PageContext = TestHelper.PageContext,
                // Sets up temporary data storage, simulating real application state persistence
                TempData = TestHelper.TempData,
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Tests the OnGet() method of PrivacyModel to ensure it executes correctly and sets a valid ModelState.
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange - No specific setup needed for this test

            // Act - Call the OnGet method, which should set up the page's data
            pageModel.OnGet();

            // Reset - No resources to clean up or reset after this test

            // Assert - Check that the ModelState is valid after calling OnGet
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
        }

        #endregion OnGet
    }
}