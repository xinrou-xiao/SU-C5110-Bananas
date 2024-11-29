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
    /// <summary>
    /// Unit test class for the QuizTests page model.
    /// </summary>
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
        /// Test Questions's setter and getter.
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

        #region OnGet

        /// <summary>
        /// Test OnGet function.
        /// </summary>
        [Test]
        public void OnGet_Valid_Called_Should_Set_IsSubmitted_To_False_CurrentQuestion_To_Zero_Answers_To_Empty()
        {
            // Arrange
            pageModel.OnGet();

            // Act

            // Assert
            Assert.That(pageModel.IsSubmitted, Is.EqualTo(false));
            Assert.That(pageModel.CurrentQuestion, Is.EqualTo(0));
            Assert.That(pageModel.Answers.Count(), Is.EqualTo(0));
        }

        #endregion OnGet

        #region OnPost

        /// <summary>
        /// Test CalculateResult function by calling onPost, simulate the quiz result has most answer with option 1.
        /// </summary>
        [Test]
        public void CalculateResult_Most_Answers_Is_1_Should_Set_ResultName_And_ResultDescription_And_ResultVideoUrl_To_Correct_Value()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                { "Answer", "1" },
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
                Answers = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                CurrentQuestion = 9,
                Questions = new string[10]
            };

            // Act
            pageModel.OnPost();

            // Assert
            Assert.That(pageModel.CurrentQuestion, Is.EqualTo(10));
            Assert.That(pageModel.IsSubmitted, Is.EqualTo(true));
            Assert.That(pageModel.ResultName, Is.EqualTo("Luffy D. Monkey"));
            Assert.That(pageModel.ResultDescription, Is.EqualTo("You're adventurous, carefree, and a born leader. You live for freedom and thrive in the vast unknown, with your friends by your side as your greatest treasure." +
                                             "<br/><br/><strong>Pros:</strong><ul>" +
                                             "<li>Infectious enthusiasm and courage.</li>" +
                                             "<li>Unyielding loyalty to your crew and loved ones.</li>" +
                                             "<li>Fearless explorer with a big dream to achieve.</li></ul>" +
                                             "<br/><strong>Cool Factor:</strong> You're the embodiment of freedom and resilience, inspiring others to chase their dreams!"));
            Assert.That(pageModel.ResultVideoUrl, Is.EqualTo("https://motionbgs.com/media/6827/strawhat-luffy.960x540.mp4"));
        }

        /// <summary>
        /// Test CalculateResult function by calling onPost, simulate the quiz result has most answer with option 2.
        /// </summary>
        [Test]
        public void CalculateResult_Most_Answers_Is_2_Should_Set_ResultName_And_ResultDescription_And_ResultVideoUrl_To_Correct_Value()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                { "Answer", "2" },
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
                Answers = new List<int> { 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                CurrentQuestion = 9,
                Questions = new string[10]
            };

            // Act
            pageModel.OnPost();

            // Assert
            Assert.That(pageModel.CurrentQuestion, Is.EqualTo(10));
            Assert.That(pageModel.IsSubmitted, Is.EqualTo(true));
            Assert.That(pageModel.ResultName, Is.EqualTo("Tanjiro Kamado"));
            Assert.That(pageModel.ResultDescription, Is.EqualTo("You're compassionate, kind, and deeply protective of those you care about. Your resilience and determination to do what's right make you a true hero." +
                                    "<br/><br/><strong>Pros:</strong><ul>" +
                                    "<li>Empathy and kindness in the face of adversity.</li>" +
                                    "<li>Unyielding determination and resilience.</li>" +
                                    "<li>Strong sense of justice and loyalty to loved ones.</li></ul>" +
                                    "<br/><strong>Cool Factor:</strong> You're the perfect balance of strength and heart, inspiring everyone with your heroic journey!"));
            Assert.That(pageModel.ResultVideoUrl, Is.EqualTo("https://motionbgs.com/media/6009/tanjiro-water-dragon.960x540.mp4"));
        }

        /// <summary>
        /// Test CalculateResult function by calling onPost, simulate the quiz result has most answer with option 3.
        /// </summary>
        [Test]
        public void CalculateResult_Most_Answers_Is_3_Should_Set_ResultName_And_ResultDescription_And_ResultVideoUrl_To_Correct_Value()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                { "Answer", "3" },
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
                Answers = new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3 },
                CurrentQuestion = 9,
                Questions = new string[10]
            };

            // Act
            pageModel.OnPost();

            // Assert
            Assert.That(pageModel.CurrentQuestion, Is.EqualTo(10));
            Assert.That(pageModel.IsSubmitted, Is.EqualTo(true));
            Assert.That(pageModel.ResultName, Is.EqualTo("Roronoa Zoro"));
            Assert.That(pageModel.ResultDescription, Is.EqualTo("You're fiercely determined and focused, never wavering from your goals. Your loyalty to those you care about is unbreakable, and you're always striving to be the best version of yourself." +
                                  "<br/><br/><strong>Pros:</strong><ul>" +
                                  "<li>Unyielding focus and perseverance.</li>" +
                                  "<li>Incredible strength and discipline.</li>" +
                                  "<li>Quietly protective and fiercely loyal.</li></ul>" +
                                  "<br/><strong>Cool Factor:</strong> You're the strong, silent type who can back up every word with action—cool, confident, and unstoppable!"));
            Assert.That(pageModel.ResultVideoUrl, Is.EqualTo("https://motionbgs.com/media/6005/fearless-zoro.960x540.mp4"));
        }

        /// <summary>
        /// Test CalculateResult function by calling onPost, simulate the quiz result have no answer in Answers.
        /// </summary>
        [Test]
        public void CalculateResult_No_Expected_Answer_In_Answers_Should_Set_ResultName_And_ResultDescription_And_ResultVideoUrl_To_Correct_Value()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                { "Answer", "4" },
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
                Answers = new List<int> { 4, 4, 4, 4, 4, 4, 4, 4, 4 },
                CurrentQuestion = 9,
                Questions = new string[10]
            };

            // Act
            pageModel.OnPost();

            // Assert
            Assert.That(pageModel.CurrentQuestion, Is.EqualTo(10));
            Assert.That(pageModel.IsSubmitted, Is.EqualTo(true));
            Assert.That(pageModel.ResultName, Is.EqualTo("A Unique Character"));
            Assert.That(pageModel.ResultDescription, Is.EqualTo("You have a unique personality that defies conventional categories. Your individuality shines in ways that can't be boxed into one description. " +
                                   "<br/><br/><strong>Pros:</strong><ul>" +
                                   "<li>Adaptable and resourceful in any situation.</li>" +
                                   "<li>Creative thinker with a unique perspective on life.</li>" +
                                   "<li>Unpredictable and full of surprises, keeping others intrigued.</li>" +
                                   "<li>Open-minded and welcoming to diverse ideas and people.</li>" +
                                   "</ul>" +
                                    "<br/><strong>Cool Factor:</strong> You're a trendsetter with a vibe that's entirely your own. People admire your authenticity and your ability to stay true to yourself, no matter what!"));
            Assert.That(pageModel.ResultVideoUrl, Is.EqualTo("https://www.desktophut.com/files/8cqM7yJhpqNnBuw_Black%20Roses%20Goku%204K_2_102359.mp4"));
        }

        #endregion OnPost
    }
}
