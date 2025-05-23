﻿using ContosoCrafts.WebSite.Pages;
using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
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
        public void OnGet_AnyCondition_State_Should_Initialize_Merchandise_List_Correctly()
        {
            // Arrange: Prepare the PageModel for testing
            // (No additional setup required as it's done in TestInitialize)

            // Act: Call the OnGet method to initialize the Merchandise list
            PageModel.OnGet();

            // Assert: Verify that the Merchandise list is initialized correctly
            Assert.That(PageModel.Merchandise, Is.Not.Null, "Merchandise list should not be null.");
            Assert.That(PageModel.Merchandise.Count, Is.EqualTo(8), "Merchandise list should contain 8 items.");

            // Assert: Verify each merchandise item has the expected title
            Assert.That(PageModel.Merchandise[0].Title, Is.EqualTo("Luffy Straw Hat"), "First item title should be 'Luffy Straw Hat'.");
            Assert.That(PageModel.Merchandise[1].Title, Is.EqualTo("Demon Slayer Sword"), "Second item title should be 'Demon Slayer Sword'.");
            Assert.That(PageModel.Merchandise[2].Title, Is.EqualTo("Gojo Motion Sticker"), "Third item title should be 'Gojo Motion Sticker'.");
            Assert.That(PageModel.Merchandise[3].Title, Is.EqualTo("Hunter x Hunter License"), "Fourth item title should be 'Hunter x Hunter License'.");
            Assert.That(PageModel.Merchandise[4].Title, Is.EqualTo("Itachi Sharingan Sneakers"), "Fifth item title should be 'Itachi Sharingan Sneakers'.");
            Assert.That(PageModel.Merchandise[5].Title, Is.EqualTo("Ichigo Kurosaki Figure"), "Sixth item title should be 'Ichigo Kurosaki Figure'.");
            Assert.That(PageModel.Merchandise[6].Title, Is.EqualTo("Haikyuu 'Karasuno' Jersey"), "Seventh item title should be 'Haikyuu 'Karasuno' Jersey'.");
            Assert.That(PageModel.Merchandise[7].Title, Is.EqualTo("Death Note Diary"), "Eighth item title should be 'Death Note Diary'.");
        }

        /// <summary>
        /// Test to verify that each MerchandiseItem is initialized with the correct properties.
        /// </summary>
        [Test]
        public void MerchandiseItem_Any_Condition_State_Should_Have_Correct_Properties()
        {
            // Arrange: Prepare the PageModel for testing
            // (No additional setup required as it's done in TestInitialize)

            // Act: Call the OnGet method to initialize the Merchandise list
            PageModel.OnGet();

            // Assert: Verify that each merchandise item has the correct properties
            foreach (var item in PageModel.Merchandise)
            {
                Assert.That(item.Title, Is.Not.Null, "Item title should not be null.");
                Assert.That(item.Description, Is.Not.Null, "Item description should not be null.");
                Assert.That(item.ImageUrl, Is.Not.Null, "Item image URL should not be null.");
                Assert.That(item.Hashtags, Is.Not.Null, "Item hashtags should not be null.");
                Assert.That(item.BuyLink, Is.Not.Null, "Item buy link should not be null.");
                Assert.That(item.Price, Is.GreaterThan(0), "Item price should be greater than 0.");
            }
        }

        #endregion OnGet Tests
    }
}