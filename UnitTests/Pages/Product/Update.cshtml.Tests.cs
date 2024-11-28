using System.Linq;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Pages.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Routing;

namespace UnitTests.Pages.Product.Update
{
    public class UpdateTests
    {
        #region TestSetup

        // Setting up the default HTTP context for the tests
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;
        public static UpdateModel PageModel;

        // This method runs before each test to initialize the test environment
        [SetUp]
        public void TestInitialize()
        {
            // Creating a new default HTTP context for isolation
            httpContextDefault = new DefaultHttpContext();

            // Initialize a new model state
            modelState = new ModelStateDictionary();

            // Creating the action context with the current HTTP context and empty model state
            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            // Setting up the metadata provider for models
            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);

            // Setting up TempData to store data across requests
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            // Creating the page context with action context and view data
            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            // Mocking the web host environment for testing purposes
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            // Setting up a mock logger for the UpdateModel
            var MockLoggerDirect = Mock.Of<ILogger<UpdateModel>>();
            JsonFileProductService productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            // Creating an instance of the UpdateModel with the mocked services
            PageModel = new UpdateModel(productService)
            {
                PageContext = pageContext,
                TempData = tempData,
                Url = new UrlHelper(actionContext)
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Test case to verify that an invalid product ID redirects to the error page
        /// </summary>
        [Test]
        public void OnGet_Invalid_Id_Should_Redirect_To_Error()
        {
            // Act
            var result = PageModel.OnGet("invalid-id") as RedirectToPageResult;

            // Assert
            // Check if redirected to error page
            Assert.That(result.PageName, Is.EqualTo("/Error"));
            // Ensure no product is loaded
            Assert.That(PageModel.Product, Is.Null);
        }

        /// <summary>
        /// Test case to check that a valid product ID returns the correct product
        /// </summary>
        [Test]
        public void OnGet_Valid_Id_Should_Return_Product_new()
        {
            // Arrange
            var productId = "vogueandcode-ruby-sis-2"; // Use an actual product ID from your JSON data

            // Act
            var result = PageModel.OnGet(productId);

            // Assert
            // Verify the result type is PageResult
            Assert.That(result, Is.InstanceOf<PageResult>());

            // Check if model state is valid
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(true));

            // Ensure the product is loaded
            Assert.That(PageModel.Product, Is.Not.Null);

            // Confirm the product ID matches
            Assert.That(PageModel.Product.Id, Is.EqualTo(productId));
        }

        #endregion OnGet

        #region OnPost

        /// <summary>
        /// Test case to ensure that if the model state is invalid, the page is returned
        /// </summary>
        [Test]
        public void OnPost_Invalid_Model_Should_Return_Page()
        {
            // Force an invalid error state
            // Adding a dummy error to simulate invalid state
            PageModel.ModelState.AddModelError("bogus", "Bogus Error");

            // Act
            var result = PageModel.OnPost(new ProductModel()) as PageResult;

            // Assert
            // Check that the model state is invalid
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(false));

            // Ensure the result is not null
            Assert.That(result, Is.Not.Null);
        }

        /// <summary>
        /// Test case to check if a valid product can be updated and redirects correctly
        /// </summary>
        [Test]
        public void OnPost_Valid_Product_Should_Update_And_Redirect_new()
        {
            // Arrange
            // Setting up a product with updated information
            PageModel.Product = new ProductModel
            {

                // Use an actual product ID from your JSON data
                Id = "vogueandcode-ruby-sis-2",
                Title = "Updated Title",
                Description = "Updated Description",
                Url = "http://example.com",
                Image = "http://example.com/image.jpg",
                Release = "2023",
                Trailer = "http://example.com/trailer",
                Season = 1,
                Genre = new string[] { "Action" },
                Ott = new OttTypeEnum[] { OttTypeEnum.Netflix }
            };

            // Act
            var result = PageModel.OnPost(PageModel.Product) as RedirectToPageResult;

            // Assert
            // Check that the model state is valid after posting
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(true));

            // Verify that it redirects to the product index page
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }

        /// <summary>
        /// Test pass an not existing product to OnPost, should redirect to error page.
        /// </summary>
        [Test]
        public void OnPost_Product_Is_Null_Should_Return_Redirect_Page()
        {
            // Arrange
            // Initialize the Product property of the PageModel with a new ProductModel instance.
            PageModel.Product = new ProductModel();
            PageModel.Product.Id = "test";

            // Act
            // Call the OnPost method of the PageModel. This method is expected to handle the post request logic.
            var data = PageModel.OnPost(PageModel.Product);

            // Assert
            // Verify that the result of the OnPost method is a RedirectToPageResult
            Assert.That(data.GetType().ToString(), Is.EqualTo("Microsoft.AspNetCore.Mvc.RedirectToPageResult"));
        }


        /// <summary>
        /// Test OnPost by giving a valid product with a Null in Genre,
        /// expected the last item's Id in json should equal to product's Id,
        /// and the last item's Genre should has length 2.
        /// </summary>
        [Test]
        public void OnPost_Valid_Product_One_Null_Genre_Should_Create_Valid_Page_And_Last_Data_Id_Is_Equal_To_Updated_Product_And_Genre_Length_Is_Two()
        {
            // Arrange
            var data = PageModel._productService.GetAllData().Last();
            data.Genre = new string[] { "Action", null, "Romance" };

            // Act
            PageModel.OnPost(data);
            var result = PageModel._productService.GetAllData().Last();

            // Assert
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(data.Id, Is.EqualTo(result.Id));
            Assert.That(result.Genre.Length, Is.EqualTo(2));
        }


        /// <summary>
        /// Test OnPost by giving a valid product and all valid Genre,
        /// and the last item's Genre in json should have length = 2.
        /// </summary>
        [Test]
        public void OnPost_Valid_Product_Vaid_Genre_Dynamic_Should_Create_Valid_Page_And_Last_Data_Id_Is_Equal_Updated_Product_Id_And_Genre_Is_Length_Two()
        {
            // Arrange
            var data = PageModel._productService.GetAllData().Last();
            data.Genre = new string[] { "Action", "Romance" };

            // Act
            PageModel.OnPost(data);
            var result = PageModel._productService.GetAllData().Last();

            // Assert
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(result.Id, Is.EqualTo(data.Id));
            Assert.That(result.Genre.Length, Is.EqualTo(2));
        }

        #endregion OnPost
    }
}