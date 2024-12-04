using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Services;

namespace UnitTests.Controllers
{
    /// <summary>
    /// Unit tests for the ProductsController class.
    /// </summary>
    [TestFixture]
    public class ProductsControllerTests
    {
        private ProductsController _controller;  // The controller instance being tested.
        private JsonFileProductService _productService;  // The product service used by the controller to manage product data.
        private string _testWebRootPath;  // The path to a temporary WebRoot directory used for testing purposes.

        #region Setup

        /// <summary>
        /// Initializes the test setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Create a temporary directory to act as WebRootPath
            _testWebRootPath = Path.Combine(Path.GetTempPath(), "TestWebRoot");
            Directory.CreateDirectory(_testWebRootPath);

            // Create a data directory
            string dataDirectory = Path.Combine(_testWebRootPath, "data");
            Directory.CreateDirectory(dataDirectory);

            // Create a test products.json file with sample data
            string productsJsonPath = Path.Combine(dataDirectory, "products.json");
            File.WriteAllText(productsJsonPath, @"
            [
                {
                    ""Id"": ""1"",
                    ""Title"": ""Product 1"",
                    ""Description"": ""Description of Product 1"",
                    ""Url"": ""https://example.com/product1"",
                    ""Image"": ""product1.jpg"",
                    ""Trophys"": 10,
                    ""FoundingYear"": 1990,
                    ""Ratings"": [4, 5]
                },
                {
                    ""Id"": ""2"",
                    ""Title"": ""Product 2"",
                    ""Description"": ""Description of Product 2"",
                    ""Url"": ""https://example.com/product2"",
                    ""Image"": ""product2.jpg"",
                    ""Trophys"": 20,
                    ""FoundingYear"": 1970,
                    ""Ratings"": [3, 4]
                }
            ]");

            // Mock IWebHostEnvironment to return the test WebRootPath
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment.Setup(m => m.WebRootPath).Returns(_testWebRootPath);

            // Initialize JsonFileProductService with the mock environment
            _productService = new JsonFileProductService(mockEnvironment.Object);

            // Initialize the controller with the product service
            _controller = new ProductsController(_productService);
        }

        /// <summary>
        /// Cleans up resources created during the test setup.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            // Clean up the temporary directory after the tests
            if (Directory.Exists(_testWebRootPath))
            {
                Directory.Delete(_testWebRootPath, true);
            }
        }

        #endregion Setup

        #region Constructor

        /// <summary>
        /// Verifies that the ProductsController is correctly initialized with the provided product service.
        /// </summary>
        [Test]
        public void Constructor_Initialization_State_Should_Initialize_ProductService()
        {
            // Assert
            Assert.That(_controller.ProductService, Is.EqualTo(_productService), "Expected ProductService to be initialized with the provided service.");
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Tests that the <see cref="ProductsController.Get"/> method returns all available products.
        /// </summary>
        [Test]
        public void Get_AnyCondition_State_Should_Return_All_Products()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2), "Expected Get() to return two products.");
        }

        #endregion Get

        #region Patch

        /// <summary>
        /// Tests that the <see cref="ProductsController.Patch"/> method handles a valid rating request correctly.
        /// </summary>
        [Test]
        public void Patch_ValidRequest_State_Should_Return_Ok_And_AddRating()
        {
            // Arrange
            var request = new ProductsController.RatingRequest
            {
                ProductId = "1",
                Rating = 5
            };

            // Act
            var result = _controller.Patch(request);

            // Assert
            Assert.That(result, Is.TypeOf<OkResult>(), "Expected Patch() to return Ok result.");

            // Verify that the rating was added
            var products = _productService.GetAllData();
            var product = products.FirstOrDefault(p => p.Id == "1");

            // Check if the product with Id "1" exists
            Assert.That(product, Is.Not.Null, "Product with Id '1' should exist.");

            // Check if the product's Ratings are not null
            Assert.That(product.Ratings, Is.Not.Null, "Product Ratings should not be null.");

            // Check if the Ratings array contains the new rating
            Assert.That(product.Ratings, Contains.Item(5), "Product Ratings should contain the new rating.");
        }
        #endregion Patch
    }
}