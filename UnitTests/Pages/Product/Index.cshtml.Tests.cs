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

namespace UnitTests.Pages.Product.Index
{
    /// <summary>
    /// Unit tests for the Index page of the Product section.
    /// </summary>
    public class IndexTests
    {
        #region TestSetup
        // Components required for setting up the test environment
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        // PageModel instance for testing the Index page
        public static IndexModel pageModel;

        /// <summary>
        /// Initializes the necessary setup before each test.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Create a new HttpContext instance to simulate HTTP requests
            httpContextDefault = new DefaultHttpContext();

            // Initialize a new ModelStateDictionary instance
            modelState = new ModelStateDictionary();

            // Setup ActionContext with HttpContext and route data
            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            // Initialize a model metadata provider and set up ViewData and TempData
            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            // Setup PageContext with the ActionContext and ViewData
            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            // Mock IWebHostEnvironment to simulate a hosting environment
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            // Mock ILogger for the IndexModel
            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();
            JsonFileProductService productService;

            // Initialize JsonFileProductService with the mocked environment
            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            // Instantiate the IndexModel with the product service
            pageModel = new IndexModel(productService);
        }

        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Tests that OnGet() fetches products correctly and verifies the first product's ID.
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Products_First_Product_Id_Should_Be_Correct()
        {
            // Arrange: No additional setup is needed as OnGet() doesn’t take parameters

            // Act: Call the OnGet method to fetch products
            pageModel.OnGet();

            // Assert: Verify that the model state is valid and the first product's ID is as expected
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.Products.First().Id, Is.EqualTo("jenlooper-cactus"));
        }
        #endregion OnGet
    }
}