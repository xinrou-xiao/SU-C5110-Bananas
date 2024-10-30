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
using System.Collections.Generic;

namespace UnitTests.Pages.Product.Update
{
    public class UpdateTests
    {
        #region TestSetup

        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static UpdateModel PageModel;

        [SetUp]
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext();

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

            var MockLoggerDirect = Mock.Of<ILogger<UpdateModel>>();
            JsonFileProductService productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            PageModel = new UpdateModel(productService)
            {
                PageContext = pageContext,
                TempData = tempData,
                Url = new UrlHelper(actionContext)
            };
        }

        #endregion TestSetup

        #region OnGet

        [Test]
        public void OnGet_Valid_Id_Should_Return_Product()
        {
            // Arrange
            var productId = "valid-product-id"; // Replace with a valid product ID from your data source

            // Act
            var result = PageModel.OnGet(productId);

            // Assert
            Assert.That(result, Is.InstanceOf<PageResult>());
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(PageModel.Product, Is.Not.Null);
            Assert.That(PageModel.Product.Id, Is.EqualTo(productId));
        }

        [Test]
        public void OnGet_Invalid_Id_Should_Redirect_To_Error()
        {
            // Act
            var result = PageModel.OnGet("invalid-id") as RedirectToPageResult;

            // Assert
            Assert.That(result.PageName, Is.EqualTo("/Error"));
            Assert.That(PageModel.Product, Is.Null);
        }

        #endregion OnGet

        #region OnPost

        [Test]
        public void OnPost_Valid_Product_Should_Update_And_Redirect()
        {
            // Arrange
            PageModel.Product = new ProductModel
            {
                Id = "valid-product-id", // Replace with a valid product ID from your data source
                Title = "Updated Title",
                Description = "Updated Description",
                Url = "http://example.com",
                Image = "http://example.com/image.jpg",
                Release = "2023",
                Trailer = "http://example.com/trailer",
                Season = 1,
                OTT = new List<OTTModel>
                {
                    new OTTModel { Icon = "icon1", Platform = "Platform1", Url = "http://platform1.com" }
                }
            };

            // Act
            var result = PageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(result.PageName, Is.EqualTo("/Product/Index"));
        }

        [Test]
        public void OnPost_Invalid_Model_Should_Return_Page()
        {
            // Force an invalid error state
            PageModel.ModelState.AddModelError("bogus", "Bogus Error");

            // Act
            var result = PageModel.OnPost() as PageResult;

            // Assert
            Assert.That(PageModel.ModelState.IsValid, Is.EqualTo(false));
            Assert.That(result, Is.Not.Null);
        }

        #endregion OnPost
    }
}
