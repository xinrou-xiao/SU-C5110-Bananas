using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    /// <summary>
    /// Unit tests for the ProductTypeEnum class and its extension methods.
    /// </summary>
    [TestFixture]
    public class ProductTypeEnumTests
    {
        /// <summary>
        /// Test to verify that the DisplayName method returns the correct display name for Amature.
        /// </summary>
        [Test]
        public void DisplayName_Should_Return_Correct_DisplayName_For_Amature()
        {
            // Arrange
            var productType = ProductTypeEnum.Amature;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Hand Made Items"));
        }

        /// <summary>
        /// Test to verify that the DisplayName method returns the correct display name for Antique.
        /// </summary>
        [Test]
        public void DisplayName_Should_Return_Correct_DisplayName_For_Antique()
        {
            // Arrange
            var productType = ProductTypeEnum.Antique;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Antiques"));
        }

        /// <summary>
        /// Test to verify that the DisplayName method returns the correct display name for Collectable.
        /// </summary>
        [Test]
        public void DisplayName_Should_Return_Correct_DisplayName_For_Collectable()
        {
            // Arrange
            var productType = ProductTypeEnum.Collectable;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Collectables"));
        }
        [Test]
        public void DisplayName_Should_Return_Correct_DisplayName_For_Commercial()
        {
            // Arrange
            var productType = ProductTypeEnum.Commercial;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Commercial goods"));
        }
        [Test]
        public void DisplayName_Should_Return_Empty_String_For_Undefined()
        {
            // Arrange
            var productType = ProductTypeEnum.Undefined;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo(""));
        }
        [Test]
        public void DisplayName_Should_Return_Empty_String_For_Unknown_Value()
        {
            // Arrange
            var productType = (ProductTypeEnum)999; // An unknown value not defined in the enum

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo(""));
        }

    }
}
