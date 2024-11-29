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
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

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
        public void CurrentQuestion_Value_Five_Should_Set_Value_To_Five()
        {
            // Arrange

            // Act
            pageModel.CurrentQuestion = 5;

            // Assert
            Assert.That(pageModel.CurrentQuestion, Is.EqualTo(5));
        }

        #endregion CurrentQuestion

        #region IsSubmitted

        /// <summary>
        /// Test IsSubmitted's setter and getter.
        /// </summary>
        [Test]
        public void IsSubmitted_True_Value_Should_Set_Value_To_Given_Value()
        {
            // Arrange

            // Act
            pageModel.IsSubmitted = true;

            // Assert
            Assert.That(pageModel.IsSubmitted, Is.EqualTo(true));
        }

        #endregion IsSubmitted

        #region Questions

        /// <summary>
        /// Test IsSubmitted's setter and getter.
        /// </summary>
        [Test]
        public void Questions_Empty_Array_Should_Set_Value_To_Given_Value()
        {
            // Arrange

            // Act
            pageModel.Questions = new string[] { };

            // Assert
            Assert.That(pageModel.Questions.Count(), Is.EqualTo(0));
        }

        #endregion Questions

        #region Options

        /// <summary>
        /// Test Options's setter and getter.
        /// </summary>
        [Test]
        public void Options_One_Element_2D_Array_Should_Set_Value_To_Given_Value()
        {
            // Arrange

            // Act
            pageModel.Options = new string[][] { new[] { "option" } };

            // Assert
            Assert.That(pageModel.Options.Count(), Is.EqualTo(1));
            Assert.That(pageModel.Options[0].Count(), Is.EqualTo(1));
        }

        #endregion Options

        #region Answers

        /// <summary>
        /// Test Answers's setter and getter.
        /// </summary>
        [Test]
        public void Answers_One_Element_List_Should_Set_Value_To_Given_Value()
        {
            // Arrange

            // Act
            pageModel.Answers = new List<int> { 1 };

            // Assert
            Assert.That(pageModel.Answers.Count(), Is.EqualTo(1));
        }

        #endregion Answers

        #region ResultName

        /// <summary>
        /// Test ResultName's setter and getter.
        /// </summary>
        [Test]
        public void ResultName_Valid_String_Should_Set_Value_To_Given_Value()
        {
            // Arrange

            // Act
            pageModel.ResultName = "test";

            // Assert
            Assert.That(pageModel.ResultName, Is.EqualTo("test"));
        }

        #endregion ResultName

        #region ResultDescription

        /// <summary>
        /// Test ResultDescription's setter and getter.
        /// </summary>
        [Test]
        public void ResultDescription_Valid_String_Should_Set_Value_To_Given_Value()
        {
            // Arrange

            // Act
            pageModel.ResultDescription = "test";

            // Assert
            Assert.That(pageModel.ResultDescription, Is.EqualTo("test"));
        }

        #endregion ResultDescription

        #region ResultVideoUrl

        /// <summary>
        /// Test ResultVideoUrl's setter and getter.
        /// </summary>
        [Test]
        public void ResultVideoUrl_Valid_String_Should_Set_Value_To_Given_Value()
        {
            // Arrange

            // Act
            pageModel.ResultVideoUrl = "test";

            // Assert
            Assert.That(pageModel.ResultVideoUrl, Is.EqualTo("test"));
        }

        #endregion ResultVideoUrl

        #region OnPost

        /// <summary>
        /// Test OnPost simulate user answer the last question.
        /// </summary>
        [Test]
        public void OnPost_Finished_Quiz_Should_Add_Answer_To_Answers_Then_Setup_ResultName_And_ResultDescription_And_ResultVideoUrl()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                { "Answer", "0" },
                { "CurrentQuestion", "9" }
            });
            httpContext.Request.Form = formCollection; 

            var pageContext = new PageContext
            {
                HttpContext = httpContext
            };

            var pageModel = new QuizModel
            {
                PageContext = pageContext,
                Answers = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                CurrentQuestion = 9,
                Questions = new string[10]
            };

            // Act
            pageModel.OnPost();

            // Assert
            Assert.That(pageModel.CurrentQuestion, Is.EqualTo(10));
            Assert.That(pageModel.IsSubmitted, Is.EqualTo(true));
            Assert.That(pageModel.ResultName, Is.EqualTo("Naruto Uzumaki"));
            Assert.That(pageModel.ResultDescription, Is.EqualTo("You're energetic, optimistic, and never give up on your dreams. You inspire others with your determination and your unwavering belief in the power of friendship." +
                                    "<br/><br/><strong>Pros:</strong><ul>" +
                                    "<li>Boundless energy and optimism.</li>" +
                                    "<li>Inspires loyalty and camaraderie.</li>" +
                                    "<li>Unshakable determination to achieve your goals.</li></ul>" +
                                    "<br/><strong>Cool Factor:</strong> You're the underdog who rises to greatness, proving that anything is possible with hard work and belief!"));
            Assert.That(pageModel.ResultVideoUrl, Is.EqualTo("https://www.desktophut.com/files/dkZH0secomXUvl9_Naruto%20Artistic%20Live%20Wallpaper_2_151011.mp4"));
        }

        #endregion OnPost
    }

}
