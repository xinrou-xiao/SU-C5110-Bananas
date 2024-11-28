using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite;

namespace ProgramTests
{
    /// <summary>
    /// This class contains unit tests to validate the functionality and logic within the Program.cs.
    /// </summary>
    public class ProgramTests
    {
        #region TestSetup

        // Static instance of IHostBuilder used in tests, initialized in TestInitialize method.
        public static IHostBuilder hostBuilder;

        /// <summary>
        /// Sets up the test environment for Program tests by initializing 
        /// a mock IHostBuilder. This method is executed before each test.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Create a mock IHostBuilder to avoid the need for a real host builder instance
            var mockHostBuilder = new Mock<IHostBuilder>();

            // Initialize hostBuilder with mock IHostBuilder
            hostBuilder = mockHostBuilder.Object;
        }

        #endregion TestSetup

        #region CreateHostBuilder

        /// <summary>
        /// Tests the CreateHostBuilder() method of Program to ensure it returns a valid IHostBuilder.
        /// </summary>
        [Test]
        public void CreateHostBuilder_ValidArgs_Should_Return_IHostBuilder()
        {
            // Arrange - No specific setup needed for this test

            // Act - Call the CreateHostBuilder method with sample arguments
            var result = Program.CreateHostBuilder(new string[] { });

            // Assert - Check that the result is not null and is of type IHostBuilder
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<IHostBuilder>());
        }

        #endregion CreateHostBuilder
    }
}