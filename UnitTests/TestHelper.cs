using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using ContosoCrafts.WebSite.Services;

namespace UnitTests
{
    /// <summary>
    /// Helper class to hold and initialize various web start settings needed for unit tests.
    /// This includes mock objects for services like IHttpContext, IUrlHelperFactory, etc.
    /// </summary>
    public static class TestHelper
    {
        // Mock of the IWebHostEnvironment to simulate the web hosting environment
        public static Mock<IWebHostEnvironment> MockWebHostEnvironment;

        // to create URL helpers for routing purposes
        public static IUrlHelperFactory UrlHelperFactory;

        // Default HttpContext used in the tests
        public static DefaultHttpContext HttpContextDefault;

        // Actual IWebHostEnvironment used in the application
        public static IWebHostEnvironment WebHostEnvironment;

        // Model state to track validation errors for the model binding
        public static ModelStateDictionary ModelState;

        // Action context that holds the request context for a particular action method
        public static ActionContext ActionContext;

        // Provider for model metadata 
        public static EmptyModelMetadataProvider ModelMetadataProvider;

        // ViewData dictionary to hold data that needs to be passed to the view
        public static ViewDataDictionary ViewData;

        // TempData dictionary for storing temporary data between requests
        public static TempDataDictionary TempData;

        // Page context that provides all necessary information for Razor Pages processing
        public static PageContext PageContext;

        // Service for interacting with product data (mocked as needed in tests)
        public static JsonFileProductService ProductService;

        /// <summary>
        /// Default Constructor to initialize mock services and properties required for unit tests.
        /// </summary>
        static TestHelper()
        {
            // Set up the mock for IWebHostEnvironment to simulate web host environment details
            MockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            MockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            MockWebHostEnvironment.Setup(m => m.WebRootPath).Returns(TestFixture.DataWebRootPath);
            MockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns(TestFixture.DataContentRootPath);

            // Initialize the default HttpContext with a trace identifier
            HttpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
            };

            HttpContextDefault.HttpContext.TraceIdentifier = "trace";

            // Create a new instance of ModelStateDictionary for tracking model state
            ModelState = new ModelStateDictionary();

            // Create a new ActionContext that holds the current request context, including route data and model state
            ActionContext = new ActionContext(HttpContextDefault, HttpContextDefault.GetRouteData(), new PageActionDescriptor(), ModelState);

            // Create a provider for model metadata (empty in this case).
            ModelMetadataProvider = new EmptyModelMetadataProvider();
            ViewData = new ViewDataDictionary(ModelMetadataProvider, ModelState);
            TempData = new TempDataDictionary(HttpContextDefault, Mock.Of<ITempDataProvider>());

            // Set up PageContext with ActionContext and ViewData for Razor Pages processing.
            PageContext = new PageContext(ActionContext)
            {
                ViewData = ViewData,
                HttpContext = HttpContextDefault
            };

            // Initialize the ProductService with a mocked IWebHostEnvironment.
            ProductService = new JsonFileProductService(MockWebHostEnvironment.Object);

            // Create another instance of JsonFileProductService using TestHelper's mock web host environment.
            JsonFileProductService productService;

            productService = new JsonFileProductService(TestHelper.MockWebHostEnvironment.Object);
        }
    }
}