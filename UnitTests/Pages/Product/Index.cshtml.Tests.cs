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
    public class IndexTests
    {
        #region TestSetup
        // Declared the necessary components for setting up the test environment
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        // Define a PageModel instance for testing the Index page
        public static IndexModel pageModel;

        // This method initializes the necessary setup before each test
        [SetUp]
        public void TestInitialize()
        {

            // Create a new HttpContext instance for simulating HTTP requests
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            // Initialize a new ModelStateDictionary instance
            modelState = new ModelStateDictionary();

            // Setup ActionContext with httpContext and route data
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
            pageModel = new IndexModel(productService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet
        // This test checks if OnGet() fetches products correctly
        [Test]

        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange: No setup is needed here as OnGet() doesn’t take parameters

            // Act: Call the OnGet method to fetch products
            pageModel.OnGet();

            // Assert: Ensure that the model state is valid and products list is populated with 15 items
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.Products.ToList().Count, Is.EqualTo(15));
        }
        #endregion OnGet
    }
}