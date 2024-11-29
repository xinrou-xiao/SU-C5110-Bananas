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
            // No setup needed for now, but this method is here for future setup if required
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

        /// <summary>
        /// Test AddRating with a null product ID to ensure it returns false.
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Null_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        /// <summary>
        /// Test AddRating with an empty product ID to check that it returns false.
        /// </summary>
        [Test]
        public void AddRating_InValid_Product_Empty_Should_Return_False()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("", 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        /// <summary>
        /// Test AddRating with a valid product ID and a rating of 5.
        /// Ensures that the rating is added successfully.
        /// </summary>
        [Test]
        public void AddRating_Valid_Product_Rating_5_Should_Return_True()
        {
            // Arrange

            // Get the First data item
            var data = TestHelper.ProductService.GetAllData().First();
            var countOriginal = data.Ratings.Count;

            // Act
            var result = TestHelper.ProductService.AddRating(data.Id, 5);
            var dataNewList = TestHelper.ProductService.GetAllData().First();

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(dataNewList.Ratings.Count, Is.EqualTo(countOriginal + 1));
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
            Assert.That(dataNewList.Ratings.Count, Is.EqualTo(1));
            
        }

        #endregion AddRating

        #region CreateData

        /// <summary>
        /// Test CreateData by passing a normal product,
        /// expected it append the data in the product.json and the length should add by one.
        /// </summary>
        [Test]
        public void CreateData_Should_Increase_Size_by_One_To_Json_And_Return_Data_And_Last_Item_In_New_List_Should_Equal_To_New_Data()
        {
            // Arrange

            // Get the data list
            var data = TestHelper.ProductService.GetAllData();

            // set up newProduct
            var newProduct = new ProductModel();
            newProduct.Title = "test";
            newProduct.Description = "test";
            newProduct.Id = System.Guid.NewGuid().ToString();

            // Act

            var result = TestHelper.ProductService.CreateData(newProduct);
            var dataNewList = TestHelper.ProductService.GetAllData();

            //Reset

            // Assert
            Assert.That(result.ToString(), Is.EqualTo(dataNewList.Last().ToString()));
            Assert.That(dataNewList.Count(), Is.EqualTo(data.Count() + 1));
        }

        /// <summary>
        /// Test CreateData by passing a null,
        /// expected it return null, and no change to json file.
        /// </summary>
        [Test]
        public void CreateData_Null_Product_Should_Return_Null_And_No_Update_On_Json_File()
        {
            // Arrange

            // Get the data list
            var data = TestHelper.ProductService.GetAllData();

            // Act
            // pass null to CreateData
            var result = TestHelper.ProductService.CreateData(null);
            var dataNewList = TestHelper.ProductService.GetAllData();

            //Reset

            // Assert
            Assert.That(result, Is.EqualTo(null));
            Assert.That(dataNewList.Count(), Is.EqualTo(data.Count()));
        }

        #endregion CreateData

        #region DeleteData

        /// <summary>
        /// Test DeleteData, expected it to delete the data from the product.json 
        /// and the length should decrease by one.
        /// </summary>
        [Test]
        public void DeleteData_Valid_Id_Should_Decrease_Size_By_One_And_Return_Deleted_Data()
        {
            // Arrange

            // Get the last data 
            var data = TestHelper.ProductService.GetAllData();

            // Act
            var result = TestHelper.ProductService.DeleteData(data.Last().Id);
            var dataNewList = TestHelper.ProductService.GetAllData();

            //Reset

            // Assert
            Assert.That(result.ToString(), Is.EqualTo(data.Last().ToString()));
            Assert.That(dataNewList.Count(), Is.EqualTo(data.Count() - 1));
        }

        #endregion DeleteData
    }
}