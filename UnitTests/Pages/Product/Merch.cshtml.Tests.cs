using ContosoCrafts.WebSite.Pages;
using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace UnitTests.Pages
{
    /// <summary>
    /// Unit tests for the MerchModel class.
    /// </summary>
    [TestFixture]
    public class MerchModelTests
    {
        #region TestSetup

        public static DefaultHttpContext httpContextDefault;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static MerchModel PageModel;

        [SetUp]
        public void TestInitialize()
        {
            // Arrange: Set up the necessary context and dependencies for the PageModel
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

            // Initialize the PageModel with the prepared context
            PageModel = new MerchModel()
            {
                PageContext = pageContext,
                TempData = tempData
            };
        }

        #endregion TestSetup

        #region OnGet Tests

        /// <summary>
        /// Test to verify that the OnGet method initializes the Merchandise list correctly.
        /// </summary>
        [Test]
        public void OnGet_Should_Initialize_Merchandise_List_Correctly()
        {
            // Act
            PageModel.OnGet();

            // Assert
            Assert.That(PageModel.Merchandise, Is.Not.Null);
            Assert.That(PageModel.Merchandise.Count, Is.EqualTo(8));

            // Verify each merchandise item
            Assert.That(PageModel.Merchandise[0].Title, Is.EqualTo("Luffy Straw Hat"));
            Assert.That(PageModel.Merchandise[1].Title, Is.EqualTo("Demon Slayer Sword"));
            Assert.That(PageModel.Merchandise[2].Title, Is.EqualTo("Gojo Motion Sticker"));
            Assert.That(PageModel.Merchandise[3].Title, Is.EqualTo("Hunter x Hunter License"));
            Assert.That(PageModel.Merchandise[4].Title, Is.EqualTo("Itachi Sharingan Sneakers"));
            Assert.That(PageModel.Merchandise[5].Title, Is.EqualTo("Ichigo Kurosaki Figure"));
            Assert.That(PageModel.Merchandise[6].Title, Is.EqualTo("Haikyuu 'Karasuno' Jersey"));
            Assert.That(PageModel.Merchandise[7].Title, Is.EqualTo("Death Note Diary"));
        }

        /// <summary>
        /// Test to verify that each MerchandiseItem is initialized with the correct properties.
        /// </summary>
        [Test]
        public void MerchandiseItem_Should_Have_Correct_Properties()
        {
            // Act
            PageModel.OnGet();

            // Assert
            foreach (var item in PageModel.Merchandise)
            {
                Assert.That(item.Title, Is.Not.Null);
                Assert.That(item.Description, Is.Not.Null);
                Assert.That(item.ImageUrl, Is.Not.Null);
                Assert.That(item.Hashtags, Is.Not.Null);
                Assert.That(item.BuyLink, Is.Not.Null);
                Assert.That(item.Price, Is.GreaterThan(0));
            }
        }

        #endregion OnGet Tests
    }
}