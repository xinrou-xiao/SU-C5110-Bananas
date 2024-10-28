using System.Linq;

using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.AddRating
{
    public class JsonFileProductServiceTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region AddRating
        //[Test]
        //public void AddRating_InValid_....()
        //{
        //    // Arrange

        //    // Act
        //    //var result = TestHelper.ProductService.AddRating(null, 1);

        //    // Assert
        //    //Assert.AreEqual(false, result);
        //}

        // ....

        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_InValid_Product_Empty_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("", 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void AddRating_Valid_Product_Rating_5_Should_Return_True()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ProductService.GetAllData().First();
            var countOriginal = data.Ratings.Length;

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 5);
            var dataNewList = TestHelper.ProductService.GetAllData().First();

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(dataNewList.Ratings.Length, Is.EqualTo(countOriginal + 1));
            Assert.That(dataNewList.Ratings.Last(), Is.EqualTo(5));
        }

        /// <summary>
        /// Test with not exists product id, expect to return false
        /// </summary>
        [Test]
        public void AddRating_NotExist_ProductId_Hello_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("Hello", 1);

            //Reset

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }


        /// <summary>
        /// Test with rating value less than 0, expect to return false
        /// </summary>
        [Test]
        public void AddRating_Invalid_Rating_LessThanZero_Should_Return_False()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ProductService.GetAllData().First();

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, -1);

            //Reset

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }
        #endregion AddRating

    }
}