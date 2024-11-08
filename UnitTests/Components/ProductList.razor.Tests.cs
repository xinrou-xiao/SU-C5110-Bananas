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
    }
}