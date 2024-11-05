using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private Mock<JsonFileProductService> _mockProductService;
        private ProductsController _controller;

        [SetUp]
        public void Setup()
        {
            // Initialize the mock service
            _mockProductService = new Mock<JsonFileProductService>();

            // Create the controller with the mocked service
            _controller = new ProductsController(_mockProductService.Object);
        }
    }
}
