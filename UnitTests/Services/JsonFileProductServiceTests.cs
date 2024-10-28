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

        /// <summary>
        /// Test with rating value greater than 5, expect to return false
        /// </summary>
        [Test]
        public void AddRating_Invalid_Rating_GreaterThanFive_Should_Return_False()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ProductService.GetAllData().First();

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 6);

            //Reset

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        /// <summary>
        /// Test to add rating to a product has a null for Ratings attribute, expect it to
        /// create an array of rating, add the rating to array, function should return true,
        /// the length should equal to 1 and last rating should be 3.
        /// </summary>

        [Test]
        public void AddRating_Rating_In_Json_Is_Null_Should_Create_Array_And_Add_Rate_And_Retrun_True()
        {
            // Arrange

            // Get the Last data item
            var data = TestHelper.ProductService.GetAllData().Last();
            var arrayOriginal = data.Ratings;

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 3);
            var dataNewList = TestHelper.ProductService.GetAllData().Last();

            //Reset

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(dataNewList.Ratings, !Is.EqualTo(arrayOriginal));
            Assert.That(dataNewList.Ratings.Length, Is.EqualTo(1));
            Assert.That(dataNewList.Ratings.Last(), Is.EqualTo(3));
        }
        #endregion AddRating
    }
}