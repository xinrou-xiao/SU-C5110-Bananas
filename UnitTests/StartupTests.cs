using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using Moq;

namespace UnitTests.Pages.Startup
{
    /// <summary>
    /// Unit tests for the Startup class in the ContosoCrafts.WebSite project.
    /// Verifies that the application configuration and services setup function as expected.
    /// </summary>
    public class StartupTests
    {
        #region TestSetup

        /// <summary>
        /// Initializes any resources or setup required before each test.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
        }

        /// <summary>
        /// Mock Startup class inheriting from the main application Startup class.
        /// Allows tests to interact with application configurations without starting the full application.
        /// </summary>
        public class Startup : ContosoCrafts.WebSite.Startup
        {
            public Startup(IConfiguration config) : base(config) { }
        }

        #endregion TestSetup

        #region ConfigureServices

        /// <summary>
        /// Tests if the services are configured correctly by the Startup class.
        /// Should pass if the web host is created without any issues.
        /// </summary>
        [Test]
        public void Startup_ConfigureServices_Valid_Defaut_Should_Pass()
        {
            // Arrange
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();

            // Act

            // Reset

            // Assert
            Assert.That(webHost, Is.Not.Null);
        }

        #endregion ConfigureServices

        #region Configure

        /// <summary>
        /// Tests if the application is configured properly when starting up.
        /// Should pass if the web host is created successfully.
        /// </summary>
        [Test]
        public void Startup_Configure_Valid_Defaut_Should_Pass()
        {
            // Arrange
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseStartup<Startup>().Build();

            // Act
            webHost.Start();

            // Reset
            webHost.Dispose();

            // Assert
            Assert.That(webHost, Is.Not.Null);
        }

        /// <summary>
        /// Tests if the application is configured properly when starting up.
        /// Should pass if the web host is created successfully.
        /// </summary>
        [Test]
        public void Startup_Configure_Not_Development_Should_Pass()
        {
            // Arrange
            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder().UseEnvironment("Development").UseStartup<Startup>().Build();

            // Act
            webHost.Start();

            // Reset
            webHost.Dispose();

            // Assert
            Assert.That(webHost, Is.Not.Null);
        }

        #endregion Configure

        #region Configuration 

        /// <summary>
        /// Tests that the Configuration property is set correctly in the Startup class.
        /// This verifies that the IConfiguration object passed into the constructor
        /// is assigned to the Configuration property as expected.
        /// </summary>
        [Test]
        public void Startup_Configuration_Should_Be_Set()
        {
            // Arrange - Create a mock IConfiguration
            var configMock = new Mock<IConfiguration>();

            // Act
            var startup = new Startup(configMock.Object);

            // Reset

            // Assert - Configuration in startup should be set to configMock 
            Assert.That(startup.Configuration, Is.EqualTo(configMock.Object));
        }

        #endregion Configuration
    }
}