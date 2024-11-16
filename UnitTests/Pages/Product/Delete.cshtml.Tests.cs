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
using System.Collections.Generic;
using System;

namespace UnitTests.Pages.Product.Delete
{
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
            Assert.That(PageModel.Product.Ratings, Is.EqualTo(new[] { 5, 5, 3, 2, 1, 5, 4, 5, 3 }));
            Assert.That(PageModel.Product.Genre, Is.EqualTo(new[] { "Action", "Romance" }));
            Assert.That(PageModel.Product.Release, Is.EqualTo("1999"));
            Assert.That(PageModel.Product.Trailer, Is.EqualTo("https://www.youtube.com/embed/yeUpnIKt6k4"));
            Assert.That(PageModel.Product.OTT[0].Platform, Is.EqualTo("Netflix"));
            Assert.That(PageModel.Product.OTT[0].Url, Is.EqualTo("https://www.netflix.com/"));
            Assert.That(PageModel.Product.OTT[0].Icon, Is.EqualTo("https://images.ctfassets.net/y2ske730sjqp/1aONibCke6niZhgPxuiilC/2c401b05a07288746ddf3bd3943fbc76/BrandAssets_Logos_01-Wordmark.jpg?w=940"));
            Assert.That(PageModel.Product.OTT[1].Platform, Is.EqualTo("Prime"));
            Assert.That(PageModel.Product.OTT[1].Url, Is.EqualTo("https://www.amazon.com/Amazon-Video/b/?&node=2858778011&ref=dvm_MLP_ROWNA_US_1"));
            Assert.That(PageModel.Product.OTT[1].Icon, Is.EqualTo("https://assets.aboutamazon.com/dims4/default/59e4166/2147483647/strip/true/crop/4454x2634+0+0/resize/365x216!/format/webp/quality/90/?url=https%3A%2F%2Famazon-blogs-brightspot.s3.amazonaws.com%2F4b%2F7f%2F4a4a80724a5e9a6b4a1931e8bf99%2Fprime-logo-rgb-prime-blue-master.png"));
            Assert.That(PageModel.Product.Season, Is.EqualTo(5));
            Assert.That(PageModel.Product.CommentList, Is.Empty);

        }

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

        [Test]
        public void OnGet_ID_Null_Should_Set_Product_To_Null()
        {
            // Arrange

            // Act
            PageModel.OnGet(null);

            // Assert
            Assert.That(PageModel.Product, Is.EqualTo(null));
        }

        #endregion OnGet Tests

        #region OnPost Tests

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
