using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;

namespace UnitTests.Pages.Product.Read
{
    internal class ReadTests
    {
        #region TestSetup

        // Setting up necessary components for testing
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        // Instance of the ReadModel to test
        public static ReadModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            // Initializing default HTTP context
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            // Initialize the model state dictionary
            modelState = new ModelStateDictionary();

            // Creating action context with the default HTTP context and model state
            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            // Setting up page context with action context and view data
            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            // Mocking the web host environment for unit tests
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            // Mocking the logger for ReadModel
            var MockLoggerDirect = Mock.Of<ILogger<ReadModel>>();

            // Service to interact with product data
            JsonFileProductService productService;

            // Initializing the product service with the mocked environment
            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            // Creating an instance of ReadModel for testing
            pageModel = new ReadModel(productService)
            {
                // Add necessary initializations for the page model if needed
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Test pass the first product id in product.json to OnGet method.
        /// Product should be the one we retrieved before.
        /// </summary>
        [Test]
        public void OnGet_Valid_Id_Should_Set_Product_To_Correct_Product_And_Page_Is_Valid()
        {
            // Arrange
            // Get the first data from product.json
            ProductModel data = pageModel.ProductService.GetAllData().First();

            // Act
            pageModel.OnGet(data.Id);

            // Assert
            // Verifying that the model state is valid and the retrieved product matches
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.Product.ToString(), Is.EqualTo(data.ToString()));
        }

        /// <summary>
        /// Test passing a non-existing id to the OnGet method.
        /// Product should be null, and page should still be valid.
        /// </summary>
        [Test]
        public void OnGet_NotExists_Id_Should_Set_Product_To_Null_And_Page_Still_Valid()
        {
            // Act
            // Attempting to retrieve a product using an invalid ID
            pageModel.OnGet("test, test, I don't exist.");

            // Assert
            // Checking that the model state remains valid and product is null
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.Product, Is.EqualTo(null));
        }

        /// <summary>
        /// Test passing a null ID to the OnGet method.
        /// Product should be null, and page should still be valid.
        /// </summary>
        [Test]
        public void OnGet_Id_Null_Should_Set_Product_To_Null_And_Page_Still_Valid()
        {
            // Act
            // Calling OnGet with a null ID
            pageModel.OnGet(null);

            // Assert
            // Verifying that the model state is still valid and product is null
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.Product, Is.EqualTo(null));
        }

        #endregion OnGet
    }
}