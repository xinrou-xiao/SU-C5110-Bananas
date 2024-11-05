using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using ContosoCrafts.WebSite.Pages;
using System;

namespace UnitTests.Pages.Privacy
{
    public class PrivacyTests
    {
        #region TestSetup
        // Setting up test environment for PrivacyModel tests
        public static PrivacyModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            // Create a mock logger for PrivacyModel so we don’t need a real logger
            var MockLoggerDirect = Mock.Of<ILogger<PrivacyModel>>();

            // Initializing the PrivacyModel with mock data for testing
            pageModel = new PrivacyModel(MockLoggerDirect)
            {
                // Provides context for the page
                PageContext = TestHelper.PageContext,
                // Sets up temporary data for the page
                TempData = TestHelper.TempData,
            };
        }

        #endregion TestSetup

        #region OnGet
        // Test to ensure that OnGet() method works correctly in PrivacyModel

        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange- No specific setup needed for this test


            // Act- Call the OnGet method, which should set up the page's data
            pageModel.OnGet();

            // Reset- No resources to clean up or reset after this test

            // Assert- Check that the ModelState is valid after calling OnGet
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
        }

        #endregion OnGet
    }
}