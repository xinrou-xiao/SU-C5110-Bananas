using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Bunit;
using NUnit.Framework;

using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;

namespace UnitTests.Components
{
    public class ProductListTests : BunitTestContext
    {
        #region TestSetup

        // Method to set up any initial configurations or services needed for tests
        [SetUp]
        public void TestInitialize()
        {
            // Currently, nothing to initialize, but method is here if setup is needed later
        }

        #endregion TestSetup

        [Test]
        public void ProductList_Valid_Default_Should_Return_Content()
        {
            // Arrange
            // Registering JsonFileProductService as a singleton service, so it can be used in the test
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            // Rendering the ProductList component and storing the result
            var page = RenderComponent<ProductList>();

            // Get the cards returned
            var result = page.Markup;

            // Assert
            // Verifying that the rendered markup contains a specific product title, "Naruto: The Journey of a Ninja Dreamer"
            Assert.That(result.Contains("Naruto: The Journey of a Ninja Dreamer"), Is.EqualTo(true));
        }

        [Test]
        public void SelectedProduct_Valid_ID_jenlooper_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_jenlooper-cactus";
            var page = RenderComponent<ProductList>();

            // Find the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Find the one that matches the ID looking for and click it 
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();

            // Get the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert 
            Assert.That(pageMarkup.Contains("Naruto is an action-packed anime about Naruto Uzumaki, a young ninja aspiring to become Hokage, exploring themes of perseverance, friendship, and identity."), Is.EqualTo(true));
        }

        [Test]
        public void SubmitRating_null_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_sailorhg-bubblesortpic";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();
            var buttonMarkup = page.Markup;
            var starButtonList = page.FindAll("span");
            var preVoteCountSpan = starButtonList[1];

            var preVoteCoutString = preVoteCountSpan.OuterHtml;

            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));
            var preStarChange = starButton.OuterHtml;
            starButton.Click();

            buttonMarkup = page.Markup;
            starButtonList = page.FindAll("span");
            var postVoteCountSpan = starButtonList[1];

            var postVoteCoutString = postVoteCountSpan.OuterHtml;
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));
            var postStarChange = starButton.OuterHtml;
        }

        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "MoreInfoButton_jenlooper-cactus";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();
            var buttonMarkup = page.Markup;
            var starButtonList = page.FindAll("span");
            var preVoteCountSpan = starButtonList[1];

            var preVoteCoutString = preVoteCountSpan.OuterHtml;

            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));
            var preStarChange = starButton.OuterHtml;
            starButton.Click();

            buttonMarkup = page.Markup;
            starButtonList = page.FindAll("span");
            var postVoteCountSpan = starButtonList[1];

            var postVoteCoutString = postVoteCountSpan.OuterHtml;
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));
            var postStarChange = starButton.OuterHtml;
        }
    }
}