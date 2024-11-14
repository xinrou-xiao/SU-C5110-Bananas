using System.Linq;

using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages.Index
{
    /// <summary>
    /// Unit test class for testing the Index page model (Index.cshtml.cs).
    /// This class verifies the behavior of the OnGet method in various scenarios.
    /// </summary>
    public class IndexTests
    {
        #region TestSetup

        // Setting up the test environment for IndexModel
        public static IndexModel pageModel;

        /// <summary>
        /// Sets up the test environment for IndexModel tests by initializing 
        /// a mock logger and configuring required page context and TempData properties.
        /// This method is executed before each test.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Creating a mock logger for IndexModel to avoid needing a real logger
            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();

            // Initializing the IndexModel with mock logger and test product service
            pageModel = new IndexModel(MockLoggerDirect, TestHelper.ProductService)
            {
                // Using TestHelper.ProductService to simulate product data
            };
        }

        #endregion TestSetup

        #region OnGet

        // Test to check if OnGet() method in IndexModel works as expected
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange- No additional setup needed here

            // Act- Call the OnGet method to populate Products
            pageModel.OnGet();

            // Assert- Check if the ModelState is valid after OnGet is called
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));

            // Check if the Products list is populated (has at least one item)
            Assert.That(pageModel.Products.ToList().Any(), Is.EqualTo(true));
        }

        #endregion OnGet
    }
}