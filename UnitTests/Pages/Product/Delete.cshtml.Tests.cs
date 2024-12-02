using System.Linq;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Pages.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace UnitTests.Pages.Product.Delete
{
    /// <summary>
    /// Contains unit tests for the Delete page model.
    /// </summary>
    public class DeleteTests
    {
        #region TestSetup

        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static DeleteModel PageModel;
        private static JsonFileProductService productService;

        /// <summary>
        /// Initializes test setup, creating mock objects and configuring dependencies.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            PageModel = new DeleteModel(productService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet Tests

        /// <summary>
        /// Tests OnGet when the product is not found and ensures that the product is null.
        /// </summary>
        [Test]
        public void OnGet_Product_NotFound_Should_Return_NotFound()
        {
            // Arrange
            var nonExistentId = "non-existent-id";

            // Act
            PageModel.OnGet(nonExistentId);

            // Assert
            Assert.That(PageModel.Product, Is.Null);
        }

        /// <summary>
        /// Tests OnGet with a valid product ID and ensures the product is correctly populated.
        /// </summary>
        [Test]
        public void OnGet_Valid_Should_Return_Valid_Product()
        {
            // Arrange

            // Act
            ProductModel data = PageModel.ProductService.GetAllData().First();

            // Act
            PageModel.OnGet(data.Id);

            //PageModel.OnGet("jenlooper-cactus");

            // Assert
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(PageModel.Product.Id, Is.EqualTo("jenlooper-cactus"));
            Assert.That(PageModel.Product.Maker, Is.EqualTo("@jenlooper"));

            Assert.That(PageModel.Product.Url, Is.EqualTo("https://naruto-official.com/en"));
            Assert.That(PageModel.Product.Title, Is.EqualTo("Naruto: The Journey of a Ninja Dreamer"));
            Assert.That(PageModel.Product.Description, Is.EqualTo("Naruto is an action-packed anime about Naruto Uzumaki, a young ninja aspiring to become Hokage, exploring themes of perseverance, friendship, and identity."));
            Assert.That(PageModel.Product.Ratings, Is.EqualTo(new[] { 5, 5, 3, 2, 1, 5, 4, 5, 3, 5 }));
            Assert.That(PageModel.Product.Genre, Is.EqualTo(new[] { "Action", "Romance" }));
            Assert.That(PageModel.Product.Release, Is.EqualTo("1999"));
            Assert.That(PageModel.Product.Trailer, Is.EqualTo("https://www.youtube.com/embed/yeUpnIKt6k4"));
            Assert.That(PageModel.Product.Ott[0], Is.EqualTo(OttTypeEnum.Netflix));
            Assert.That(PageModel.Product.Ott[1], Is.EqualTo(OttTypeEnum.Prime));
            Assert.That(PageModel.Product.Season, Is.EqualTo(5));
            Assert.That(PageModel.Product.CommentList, Is.Empty);
        }

        /// <summary>
        /// Tests OnPost when the ID is null and ensures that the page is returned.
        /// </summary>
        [Test]
        public void OnPost_ID_Null_Should_Return_Page()
        {
            // Arrange
            PageModel.OnGet("jenlooper-cactus");

            // Act
            var result = PageModel.OnPost(null);

            // Assert
            Assert.That(result, Is.TypeOf<PageResult>());
        }

        /// <summary>
        /// Tests OnGet when the product ID is null, ensuring that the product is set to null.
        /// </summary>
        [Test]
        public void OnGet_ID_Null_Should_Set_Product_To_Null()
        {
            // Act
            PageModel.OnGet(null);

            // Assert
            Assert.That(PageModel.Product, Is.EqualTo(null));
        }

        #endregion OnGet Tests

        #region OnPost Tests

        /// <summary>
        /// Tests OnPost when the model state is invalid and ensures that the page is returned.
        /// </summary>
        [Test]
        public void OnPost_InvalidModelState_Should_Return_Page()
        {
            // Arrange
            PageModel.ModelState.AddModelError("Product.Id", "Product ID is required");

            // Act
            var result = PageModel.OnPost("Product.Id");

            // Assert
            Assert.That(result, Is.TypeOf<PageResult>());
        }

        /// <summary>
        /// Tests OnPost when the product is not found, ensuring that the product is not deleted and the page is returned.
        /// </summary>
        [Test]
        public void OnPost_Product_NotFound_Should_Not_Delete_And_Return_Page()
        {
            // Arrange
            var nonExistentProductId = "non-existent-id";
            PageModel.Product = new ProductModel { Id = nonExistentProductId };

            // Act
            var result = PageModel.OnPost("Product.Id");

            // Assert
            Assert.That(result, Is.TypeOf<RedirectToPageResult>());
            Assert.That(PageModel.Product, Is.Not.Null); // Product should not be deleted
        }

        #endregion OnPost Tests
    }
}