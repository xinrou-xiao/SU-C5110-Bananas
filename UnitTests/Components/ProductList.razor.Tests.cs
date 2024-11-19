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

        /// <summary>
        /// Method to set up any initial configurations or services needed for tests
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Currently, nothing to initialize, but method is here if setup is needed later
        }

        #endregion TestSetup

        /// <summary>
        /// Verifies that the ProductList component renders correctly with the default products 
        /// and returns content containing a specific product title.
        /// </summary>
        [Test]
        public void ProductList_Valid_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<ProductList>();

            // Get the cards returned
            var result = page.Markup;

            // Assert
            Assert.That(result.Contains("Naruto: The Journey of a Ninja Dreamer"), Is.EqualTo(true));
        }

        /// <summary>
        /// Verifies that clicking the "More Info" button for a product with a specific ID (jenlooper) 
        /// returns the correct product description in the content.
        /// </summary>
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

        /// <summary>
        /// Verifies that clicking an unstarred rating for a product increments the vote count 
        /// and updates the star to a checked state. Also ensures that the rating is correctly added to the product.
        /// </summary>
        [Test]
        public void SubmitRating_null_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
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

        /// <summary>
        /// Verifies that clicking an unstarred rating for a product increments the vote count 
        /// and updates the star to a checked state, and ensures that the rating is correctly added to the product.
        /// </summary>
        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
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

        #region UpdateGenre

        /// <summary>
        /// Tests the UpdateGenre functionality of the ProductList component.
        /// Verifies that when the user selects the "Action" genre, the component fetches and displays the correct products.
        /// </summary>
        [Test]
        public void UpdateGenre_Valid_Genre_Should_Fetch_Action_Anime()
        {
            // Arrange
            // Initialize a Bunit test context and configure the service dependency.
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Render the ProductList component and set initial properties.
            var component = context.RenderComponent<ProductList>();
            component.Instance.searchKeywords = null; // Set search to null for testing.
            component.Instance.selectedSort = "newest"; // Set the sorting to "newest".

            // Act
            // Locate the "Action" genre button and simulate a click event to filter products by the selected genre.
            var genreBtn = component.FindAll("button.genre-unselected").First();
            genreBtn.Click();

            // Reset

            // Assert
            // Verify that the genre button clicked is "Action".
            Assert.That(genreBtn.TextContent == "Action", Is.True);

            // Verify that the number of products displayed is correct for the "Action" genre.
            Assert.That(component.Instance.Products.Count() == 8, Is.True);
        }

        /// <summary>
        /// Tests the UpdateGenre functionality of the ProductList component.
        /// Verifies that when the user selects the "All" genre, all products are fetched and displayed.
        /// </summary>
        [Test]
        public void UpdateGenre_Valid_All_Genre_Should_Fetch_All()
        {
            // Arrange
            // Initialize a Bunit test context and configure the service dependency.
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Render the ProductList component and set initial properties.
            var component = context.RenderComponent<ProductList>();
            component.Instance.searchKeywords = null; // Set search to null for testing.
            component.Instance.selectedSort = "newest"; // Set the sorting to "newest".

            // Act
            // Locate the "All" genre button and simulate a click event to display all products.
            var genreBtn = component.FindAll("button.genre-selected").First();
            genreBtn.Click();

            // Reset

            // Assert
            // Verify that the genre button clicked is "All".
            Assert.That(genreBtn.TextContent == "All", Is.True);

            // Verify that the number of products displayed is correct for the "All" genre.
            Assert.That(component.Instance.Products.Count() == 15, Is.True);
        }

        #endregion UpdateGenre

        #region UpdateSort

        /// <summary>
        /// Tests the UpdateSort functionality of the ProductList component.
        /// Verifies that when a valid sorting option (e.g., "asc") is selected from the dropdown, 
        /// the selectedSort property is updated correctly.
        /// </summary>
        [Test]
        public void UpdateSort_Valid_Select_Option_Should_Update_SelectSort()
        {
            // Arrange
            // Initialize a Bunit test context and configure the service dependency.
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Render the ProductList component to trigger the component lifecycle and initial state.
            var component = context.RenderComponent<ProductList>();

            // Act
            // Locate the first sorting option ("asc") and simulate a click event to select it.
            var sortSelect = component.Find("select.sort-select");
            sortSelect.Change("asc");

            // Reset

            // Assert
            // Verify that the selectedSort property is updated to "asc" after the selection.
            Assert.That(component.Instance.selectedSort == "asc", Is.True);
        }

        #endregion UpdateSort

        #region Sort

        /// <summary>
        /// Tests the UpdateSort functionality in the ProductList component.
        /// Verifies that selecting "asc" in the sorting dropdown orders products in ascending order based on title.
        /// </summary>
        [Test]
        public void UpdateSort_Valid_Select_Asc_Should_Order_Products_By_Ascending()
        {
            // Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var component = context.RenderComponent<ProductList>();

            // Act
            component.Instance.searchKeywords = null;
            var searchBtn = component.FindAll("button.btn.search-btn").First();
            searchBtn.Click();

            var sortSelect = component.Find("select.sort-select");
            sortSelect.Change("asc");

            // Reset

            // Assert
            Assert.That(component.Instance.Products.First().Title.CompareTo(component.Instance.Products.Last().Title) == -1, Is.True);
        }

        /// <summary>
        /// Tests the UpdateSort functionality in the ProductList component.
        /// Verifies that selecting "desc" in the sorting dropdown orders products in descending order based on title.
        /// </summary>
        [Test]
        public void UpdateSort_Valid_Select_Desc_Should_Order_Products_By_Descending()
        {
            // Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var component = context.RenderComponent<ProductList>();

            // Act
            component.Instance.searchKeywords = null;
            var searchBtn = component.FindAll("button.btn.search-btn").First();
            searchBtn.Click();

            var sortSelect = component.Find("select.sort-select");
            sortSelect.Change("desc");

            // Reset

            // Assert
            Assert.That(component.Instance.Products.First().Title.CompareTo(component.Instance.Products.Last().Title) == 1, Is.True);
        }

        /// <summary>
        /// Tests the UpdateSort functionality in the ProductList component.
        /// Verifies that selecting "newest" in the sorting dropdown orders products by release year from new to old.
        /// </summary>
        [Test]
        public void UpdateSort_Valid_Select_Newest_Should_Order_Products_By_Release_Year_New_To_Old()
        {
            // Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var component = context.RenderComponent<ProductList>();

            // Act
            component.Instance.searchKeywords = null;
            var searchBtn = component.FindAll("button.btn.search-btn").First();
            searchBtn.Click();

            var sortSelect = component.Find("select.sort-select");
            sortSelect.Change("newest");

            // Reset

            // Assert
            Assert.That(component.Instance.Products.First().Release.CompareTo(component.Instance.Products.Last().Release) == 1, Is.True);
        }

        /// <summary>
        /// Tests the UpdateSort functionality in the ProductList component.
        /// Verifies that selecting "oldest" in the sorting dropdown orders products by release year from old to new.
        /// </summary>
        [Test]
        public void UpdateSort_Valid_Select_Oldest_Should_Order_Products_By_Release_Year_Old_To_New()
        {
            // Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var component = context.RenderComponent<ProductList>();

            // Act
            component.Instance.searchKeywords = null;
            var searchBtn = component.FindAll("button.btn.search-btn").First();
            searchBtn.Click();

            var sortSelect = component.Find("select.sort-select");
            sortSelect.Change("oldest");

            // Reset

            // Assert
            Assert.That(component.Instance.Products.First().Release.CompareTo(component.Instance.Products.Last().Release) == -1, Is.True);
        }

        /// <summary>
        /// Tests the UpdateSort functionality in the ProductList component.
        /// Verifies that selecting "rating" in the sorting dropdown orders products by rating from high to low.
        /// </summary>
        [Test]
        public void UpdateSort_Valid_Select_Rating_Should_Order_Products_By_Rating_High_To_Low()
        {
            // Arrange
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var component = context.RenderComponent<ProductList>();

            // Act
            component.Instance.searchKeywords = null;
            var searchBtn = component.FindAll("button.btn.search-btn").First();
            searchBtn.Click();

            var sortSelect = component.Find("select.sort-select");
            sortSelect.Change("rating");

            var firstRatingAvg = component.Instance.Products.First().Ratings.Average();
            var secondRatingAvg = component.Instance.Products.ElementAt(1).Ratings.Average();

            // Reset

            // Assert
            Assert.That(firstRatingAvg > secondRatingAvg, Is.True);
        }

        #endregion Sort
    }
}