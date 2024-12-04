using ContosoCrafts.WebSite.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace UnitTests.Controllers
{
    /// <summary>
    /// Unit test for ReadController
    /// </summary>
    [TestFixture]
    public class ReadControllerTests
    {
        private ReadController _controller;

        #region Setup

        /// <summary>
        /// Initializes the test setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Initialize the ReadController before each test
            _controller = new ReadController();
        }

        #endregion Setup

        #region Read

        /// <summary>
        /// Tests that the <see cref="ProductsController.Read"/> method returns a <see cref="ViewResult"/>.
        /// </summary>
        [Test]
        public void Read_AnyCondition_State_Should_Return_ViewResult()
        {
            // Act
            var result = _controller.Read();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        #endregion Read
    }
}