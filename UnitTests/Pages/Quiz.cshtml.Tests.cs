using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;
using Microsoft.AspNetCore.Routing;

namespace UnitTests.Pages.Quiz
{
    internal class QuizTests
    {
        #region TestSetup

        // Static fields used for setting up the test environment
        public static IUrlHelperFactory urlHelperFactory; // Factory for creating URL helpers
        public static DefaultHttpContext httpContextDefault; // Default HTTP context for the tests
        public static IWebHostEnvironment webHostEnvironment; // Web host environment mock
        public static ModelStateDictionary modelState; // Model state dictionary for validation
        public static ActionContext actionContext; // Action context for the page
        public static EmptyModelMetadataProvider modelMetadataProvider; // Metadata provider for the model
        public static ViewDataDictionary viewData; // View data dictionary
        public static TempDataDictionary tempData; // Temp data dictionary
        public static PageContext pageContext; // Page context for the tests

        // The page model being tested
        public static QuizModel pageModel;

        /// <summary>
        /// Initializes the test setup.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Initialize the default HTTP context
            httpContextDefault = new DefaultHttpContext();

            // Initialize the model state dictionary
            modelState = new ModelStateDictionary();

            // Create an action context using the HTTP context and model state
            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            // Initialize the model metadata provider
            modelMetadataProvider = new EmptyModelMetadataProvider();

            // Initialize the view data dictionary with the model metadata provider and model state
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);

            // Initialize the temp data dictionary with the HTTP context
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            // Create a page context using the action context and view data
            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            // Mock the web host environment
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            // Mock the logger for the ReadModel
            var MockLoggerDirect = Mock.Of<ILogger<ReadModel>>();

            // Initialize the product service with the mocked web host environment
            JsonFileProductService productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            // Initialize the page model with the product service
            pageModel = new QuizModel();
        }

        #endregion TestSetup

        #region CurrentQuestion

        /// <summary>
        /// Test CurrentQuestion's setter and getter.
        /// </summary>
        [Test]
        public void CurrentQuestion_Valid_Value_Should_Set_Value_To_Given_Value()
        {
            // Arrange

            // Act
            pageModel.CurrentQuestion = 5;

            // Assert
            Assert.That(pageModel.CurrentQuestion, Is.EqualTo(5));
        }

        #endregion CurrentQuestion
    }
}
