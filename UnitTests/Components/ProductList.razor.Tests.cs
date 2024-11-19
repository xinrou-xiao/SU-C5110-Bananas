using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Bunit;
using NUnit.Framework;

using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Moq;
using System.Collections.Generic;
using Bunit.Extensions;

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
            // Arrange
            // Registering JsonFileProductService as a singleton service, so it can be used in the test
            Services.AddSingleton(TestHelper.ProductService);
            var id = "MoreInfoButton_jenlooper-light";

            // Rendering the ProductList component and storing the result
            var page = RenderComponent<ProductList>();

            // Finding all buttons on the page
            var buttonList = page.FindAll("Button");

            // Finding the button that matches the specified ID and clicking it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Getting the markup after clicking the button
            var buttonMarkup = page.Markup;

            // Finding all span elements (stars) on the page
            var starButtonList = page.FindAll("span");

            // Storing the outer HTML of the vote count span before clicking the star
            var preVoteCountSpan = starButtonList[1];
            var preVoteCoutString = preVoteCountSpan.OuterHtml;

            // Finding the first star button that is not checked and storing its outer HTML
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));
            var preStarChange = starButton.OuterHtml;

            // Clicking the star button
            starButton.Click();

            // Getting the markup after clicking the star button
            buttonMarkup = page.Markup;

            // Finding all span elements (stars) on the page again
            starButtonList = page.FindAll("span");

            // Storing the outer HTML of the vote count span after clicking the star
            var postVoteCountSpan = starButtonList[1];
            var postVoteCoutString = postVoteCountSpan.OuterHtml;

            // Finding the first star button that is checked and storing its outer HTML
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));
            var postStarChange = starButton.OuterHtml;

            JsonFileProductService productService = Services.GetService<JsonFileProductService>();
            var ratings = productService.GetAllData().First(x => x.Id == "jenlooper-light").Ratings.Last();
            Assert.That(ratings.Equals(1), Is.EqualTo(true));
        }

        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
            // Registering JsonFileProductService as a singleton service, so it can be used in the test
            Services.AddSingleton(TestHelper.ProductService);
            var id = "MoreInfoButton_jenlooper-cactus";

            // Rendering the ProductList component and storing the result
            var page = RenderComponent<ProductList>();

            // Finding all buttons on the page
            var buttonList = page.FindAll("Button");

            // Finding the button that matches the specified ID and clicking it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            // Getting the markup after clicking the button
            var buttonMarkup = page.Markup;

            // Finding all span elements (stars) on the page
            var starButtonList = page.FindAll("span");

            // Storing the outer HTML of the vote count span before clicking the star
            var preVoteCountSpan = starButtonList[1];
            var preVoteCoutString = preVoteCountSpan.OuterHtml;

            // Finding the first star button that is not checked and storing its outer HTML
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));
            var preStarChange = starButton.OuterHtml;

            // Clicking the star button
            starButton.Click();

            // Getting the markup after clicking the star button
            buttonMarkup = page.Markup;

            // Finding all span elements (stars) on the page again
            starButtonList = page.FindAll("span");

            // Storing the outer HTML of the vote count span after clicking the star
            var postVoteCountSpan = starButtonList[1];
            var postVoteCoutString = postVoteCountSpan.OuterHtml;

            // Finding the first star button that is checked and storing its outer HTML
            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));
            var postStarChange = starButton.OuterHtml;

            JsonFileProductService productService = Services.GetService<JsonFileProductService>();
            var ratings = productService.GetAllData().First(x => x.Id == "jenlooper-cactus").Ratings.Last();
            Assert.That(ratings.Equals(1), Is.EqualTo(true));
        }


        #region OnInitialized

        /// <summary>
        /// Tests the OnInitialized lifecycle method of the ProductList component.
        /// Verifies that when the component is initialized, the Products and GenreList properties are correctly set,
        /// with Products containing valid data and GenreList being populated with available genres.
        /// </summary>
        [Test]
        public void OnInitialized_Valid_Should_Initialize_Products_And_GenreList_Correctly()
        {
            // Arrange
            // Initialize a Bunit test context and configure the service dependency.
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            // Render the ProductList component to trigger the OnInitialized lifecycle method.
            var component = context.RenderComponent<ProductList>();

            // Reset
            // No specific reset actions are needed here.

            // Assert
            // Verify that the Products property is not null and contains one or more products.
            Assert.That(component.Instance.Products, Is.Not.Null);
            Assert.That(component.Instance.Products.Count() > 0, Is.True);

            // Verify that the GenreList property is not null and contains one or more genres.
            Assert.That(component.Instance.GenreList, Is.Not.Null);
            Assert.That(component.Instance.GenreList.Count() > 0, Is.True);
        }

        #endregion OnInitialized

        #region CleanSearchInput

        /// <summary>
        /// Tests the CleanSearchInput functionality of the ProductList component.
        /// Verifies that when the clean search input button is clicked, 
        /// the searchKeywords property is reset to an empty string.
        /// </summary>
        [Test]
        public void CleanSearchInput_Valid_Should_Reset_SearchKeywords_Empty()
        {
            // Arrange
            // Initialize a Bunit test context and configure the service dependency.
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Render the ProductList component and set an initial value for searchKeywords.
            var component = context.RenderComponent<ProductList>();
            component.Instance.searchKeywords = "test"; // Set a non-empty initial value.

            // Act
            // Locate the clean search input button in the rendered component and simulate a click event.
            var cleanBtn = component.FindAll("button.btn.search-input").First();
            cleanBtn.Click();

            // Reset
            // No specific reset actions are needed here.

            // Assert
            // Verify that the searchKeywords property is reset to an empty string after the button click.
            Assert.That(component.Instance.searchKeywords.Length == 0, Is.True);
        }


        #endregion CleanSearchInput

        #region Search

        /// <summary>
        /// Tests the Search functionality with a valid keyword.
        /// Verifies that the correct product is returned when a valid keyword is provided 
        /// and the search button is clicked.
        [Test]
        public void Search_Valid_Keyword_Should_Find_Correct_Item()
        {
            // Arrange
            // Initialize a Bunit test context and configure the service dependency.
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Render the ProductList component.
            var component = context.RenderComponent<ProductList>();

            // Set up the search keyword and sorting option on the component instance.
            component.Instance.searchKeywords = "Naruto"; // Search for products with the title "Naruto".
            component.Instance.selectedSort = "newest";   // Apply the "newest" sorting option.

            // Act
            // Locate the search button in the rendered component and simulate a click event.
            var searchBtn = component.FindAll("button.btn.search-btn").First();
            searchBtn.Click();

            // Reset

            // Assert
            // Verify that only one product matches the search criteria and appears in the filtered list.
            Assert.That(component.Instance.Products.Count() == 1, Is.True);
        }

        [Test]
        /// <summary>
        /// Tests the Search functionality when the search keyword is null.
        /// Verifies that all products are returned in the absence of a specific search keyword
        /// when the search button is clicked.
        /// </summary>
        public void Search_InValid_Null_Keyword_Should_Fetch_All_Data()
        {
            // Arrange
            // Initialize a Bunit test context and configure the service dependency.
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Render the ProductList component.
            var component = context.RenderComponent<ProductList>();

            // Act
            // Set the search keyword to null and trigger the search action by clicking the button.
            component.Instance.searchKeywords = null; // No search keyword specified.
            var searchBtn = component.FindAll("button.btn.search-btn").First();
            searchBtn.Click();

            // Reset

            // Assert
            // Verify that all valid products (with non-null titles) are returned.
            Assert.That(
                component.Instance.Products.Count() ==
                TestHelper.ProductService.GetAllData().Where(p => !p.Title.IsNullOrEmpty()).Count(),
                Is.True
            );
        }

        #endregion Search
    }
}