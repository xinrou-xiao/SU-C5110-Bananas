using ContosoCrafts.WebSite.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class ReadControllerTests
    {
        private ReadController _controller;

        [SetUp]
        public void Setup()
        {
            // Initialize the ReadController before each test
            _controller = new ReadController();
        }
        [Test]
        public void Read_Should_Return_ViewResult()
        {
            // Act
            var result = _controller.Read();

            // Assert
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }
    }
}
