using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Models;
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
        [Test]
        public void Constructor_Should_Initialize_ProductService()
        {
            // Assert
            Assert.That(_controller.ProductService, Is.Not.Null);
        }
        [Test]
        public void Get_Should_Return_All_Products()
        {
            // Arrange
            var expectedProducts = new List<ProductModel>
            {
                new ProductModel { Id = "1", Title = "Product 1" },
                new ProductModel { Id = "2", Title = "Product 2" }
            };

            _mockProductService.Setup(service => service.GetAllData()).Returns(expectedProducts);

            // Act
            var result = _controller.Get();

            // Assert
            Assert.That(result, Is.EqualTo(expectedProducts));
        }
    }
}
