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
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;
using System;

namespace UnitTests.Pages.Product.Create
{
    /// <summary>
    /// Unit test class for the Create page model.
    /// </summary>
    internal class Create
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
        public static CreateModel pageModel;

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
            pageModel = new CreateModel(productService);
        }

        #endregion TestSetup
#region OnGet
/// <summary>
/// Initialize the Create page and set Product id to test, it should create a valid Create page and Product id should be test.
/// </summary>
[Test]
        public void OnGet_Page_Should_be_Valid_Id_Should_Be_Test()
        {
            // Arrange

            // Act
            pageModel.OnGet();
            // set up product id to test
            pageModel.Product.Id = "test"; 


            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(pageModel.Product.Id, Is.EqualTo("test"));
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// Test OnPost by giving a valid product, and valid string array for the rest arguments,
        /// expected the page created valid, and json length + 1,
        /// and last item's Id in json should equal to new product's Id.
        /// </summary>
        [Test]
        public void OnPost_Valid_Product_Should_Create_Valid_Page_And_Json_Data_Length_Increase_By_One_And_Last_Data_Id_Is_Equal_To_New_Added_Product()
        {
            // Arrange
            var data = pageModel.ProductService.GetAllData();

            // create valid arguments
            var product = new ProductModel();
            product.Id = "test-data";
            string[] genre_dynamic = new string[] { "Action", "Shonen" };
            string[] OTT_dynamic_platform = new string[] { "Netflex", "Prime" };
            string[] OTT_dynamic_url = new string[] { "Netflex.com", "Prime.com" };
            string[] OTT_dynamic_icon = new string[] { "Netflex.png", "Prime.png" };

            // Act
            pageModel.OnPost(product, genre_dynamic, OTT_dynamic_platform, OTT_dynamic_url, OTT_dynamic_icon);
            var result = pageModel.ProductService.GetAllData();

            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(data.Count() + 1, Is.EqualTo(result.Count()));
            Assert.That(result.Last().Id, Is.EqualTo(product.Id));
        }

        /// <summary>
        /// Test OnPost by giving a valid product, empty genre_dynamic and valid string array for the rest arguments,
        /// expected the page created valid, and json length + 1,
        /// and the last item's Id in json should equal to new product's Id,
        /// and the last item's Genre in json should be null,
        /// </summary>
        [Test]
        public void OnPost_Valid_Product_Empty_Genre_Dynamic_Should_Create_Valid_Page_And_Json_Data_Length_Increase_By_One_And_Last_Data_Id_Is_Equal_To_New_Added_Product_And_Genre_Is_Null()
        {
            // Arrange
            var data = pageModel.ProductService.GetAllData();

            // create arguments
            var product = new ProductModel();
            product.Id = "test-data";
            // empty genre_dynamic array
            string[] genre_dynamic = new string[] { };
            string[] OTT_dynamic_platform = new string[] { "Netflex", "Prime" };
            string[] OTT_dynamic_url = new string[] { "Netflex.com", "Prime.com" };
            string[] OTT_dynamic_icon = new string[] { "Netflex.png", "Prime.png" };

            // Act
            pageModel.OnPost(product, genre_dynamic, OTT_dynamic_platform, OTT_dynamic_url, OTT_dynamic_icon);
            var result = pageModel.ProductService.GetAllData();

            Console.WriteLine(result.Last());
            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(data.Count() + 1, Is.EqualTo(result.Count()));
            Assert.That(result.Last().Id, Is.EqualTo(product.Id));
            Assert.That(result.Last().Genre, Is.EqualTo(null));
        }


        /// <summary>
        /// Test OnPost by giving a valid product, genre_dynamic with an null inside the array
        /// and valid string array for the rest arguments,
        /// expected the page created valid, and json length + 1,
        /// and the last item's Id in json should equal to new product's Id,
        /// and the last item's Genre in json should have length = 2(skip null).
        /// </summary>
        [Test]
        public void OnPost_Valid_Product_One_Null_In_Genre_Dynamic_Should_Create_Valid_Page_And_Json_Data_Length_Increase_By_One_And_Last_Data_Id_Is_Equal_To_New_Added_Product_And_Genre_Should_Skip_Null()
        {
            // Arrange
            var data = pageModel.ProductService.GetAllData();
            // create valid arguments
            var product = new ProductModel();
            product.Id = "test-data";
            // a null is inside genre_dynamic
            string[] genre_dynamic = new string[] { "Action", null, "Shonen" };
            string[] OTT_dynamic_platform = new string[] { "Netflex", "Prime" };
            string[] OTT_dynamic_url = new string[] { "Netflex.com", "Prime.com" };
            string[] OTT_dynamic_icon = new string[] { "Netflex.png", "Prime.png" };

            // Act
            pageModel.OnPost(product, genre_dynamic, OTT_dynamic_platform, OTT_dynamic_url, OTT_dynamic_icon);
            var result = pageModel.ProductService.GetAllData();

            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(data.Count() + 1, Is.EqualTo(result.Count()));
            Assert.That(result.Last().Id, Is.EqualTo(product.Id));
            Assert.That(result.Last().Genre.Length, Is.EqualTo(2));
        }


        /// <summary>
        /// Test OnPost by giving a valid product, OTT_dynamic_platform with empty OTT_dynamic_platform
        /// and valid string array for the rest arguments,
        /// expected the page created valid, and json length + 1,
        /// and the last item's Id in json should equal to new product's Id,
        /// and the last item's OTT should be emtpy.
        /// </summary>
        [Test]
        public void OnPost_Valid_Product_Empty_OTT_Dynamic_Platform_Should_Create_Valid_Page_And_Json_Data_Length_Increase_By_One_And_Last_Data_Id_Is_Equal_To_New_Added_Product_And_OTT_Is_Empty()
        {
            // Arrange
            var data = pageModel.ProductService.GetAllData();
            // create valid arguments
            var product = new ProductModel();
            product.Id = "test-data";
            string[] genre_dynamic = new string[] { "Action", "Shonen" };
            // empty OTT_dynamic_platform array
            string[] OTT_dynamic_platform = new string[] { };
            string[] OTT_dynamic_url = new string[] { "Netflex.com", "Prime.com" };
            string[] OTT_dynamic_icon = new string[] { "Netflex.png", "Prime.png" };

            // Act
            pageModel.OnPost(product, genre_dynamic, OTT_dynamic_platform, OTT_dynamic_url, OTT_dynamic_icon);
            var result = pageModel.ProductService.GetAllData();

            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(data.Count() + 1, Is.EqualTo(result.Count()));
            Assert.That(result.Last().Id, Is.EqualTo(product.Id));
            Assert.That(result.Last().OTT.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Test OnPost by giving a valid product, OTT_dynamic_platform with empty OTT_dynamic_platform
        /// and valid string array for the rest arguments,
        /// expected the page created valid, and json length + 1,
        /// and the last item's Id in json should equal to new product's Id,
        /// and the last item's OTT length should be 1(skip null).
        /// </summary>
        [Test]
        public void OnPost_Valid_Product_One_Null_in_OTT_Dynamic_Platform_Should_Create_Valid_Page_And_Json_Data_Length_Increase_By_One_And_Last_Data_Id_Is_Equal_To_New_Added_Product_And_OTT_Should_Skip_Null()
        {
            // Arrange
            var data = pageModel.ProductService.GetAllData();
            // create valid arguments
            var product = new ProductModel();
            product.Id = "test-data";
            string[] genre_dynamic = new string[] { "Action", "Shonen" };
            // a null inside OTT_dynamic_platform
            string[] OTT_dynamic_platform = new string[] { "Netflex", null };
            string[] OTT_dynamic_url = new string[] { "Netflex.com", "Prime.com" };
            string[] OTT_dynamic_icon = new string[] { "Netflex.png", "Prime.png" };

            // Act
            pageModel.OnPost(product, genre_dynamic, OTT_dynamic_platform, OTT_dynamic_url, OTT_dynamic_icon);
            var result = pageModel.ProductService.GetAllData();

            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(true));
            Assert.That(data.Count() + 1, Is.EqualTo(result.Count()));
            Assert.That(result.Last().Id, Is.EqualTo(product.Id));
            Assert.That(result.Last().OTT.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Test if page state is invalid, should return RedirectToPageResult.
        /// </summary>
        [Test]
        public void OnPost_Invalid_PageModel_State_Should_Be_False_And_Return_A_RedirectToPageResult()
        {
            // Arrange
            pageModel.OnGet();
            pageModel.Product.Title = "";
            pageModel.ModelState.AddModelError("test", "test");

            // Act
            var action = pageModel.OnPost(pageModel.Product, new string[] { }, new string[] { }, new string[] { }, new string[] { });

            Console.WriteLine(action);
            // Assert
            Assert.That(pageModel.ModelState.IsValid, Is.EqualTo(false));
            Assert.That(action.GetType().ToString(), Is.EqualTo("Microsoft.AspNetCore.Mvc.RedirectToPageResult"));
        }
        #endregion OnPost
    }
}