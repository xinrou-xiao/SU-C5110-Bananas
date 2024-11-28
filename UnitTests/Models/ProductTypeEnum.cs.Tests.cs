using ContosoCrafts.WebSite.Models;
using NUnit.Framework;

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
        public void DisplayName_Amature_Valid_Should_Return_HandMadeItems()
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
        public void DisplayName_Antique_Valid_Should_Return_Antiques()
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
        public void DisplayName_Collectable_Valid_Should_Return_Collectables()
        {
            // Arrange
            var productType = ProductTypeEnum.Collectable;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Collectables"));
        }

        /// <summary>
        /// Test to verify that the DisplayName method returns the correct display name for Commercial.
        /// </summary>
        [Test]
        public void DisplayName_Commercial_Valid_Should_Return_CommercialGoods()
        {
            // Arrange
            var productType = ProductTypeEnum.Commercial;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo("Commercial goods"));
        }

        /// <summary>
        /// Test to verify that the DisplayName method returns an empty string for Undefined.
        /// </summary>
        [Test]
        public void DisplayName_Undefined_Valid_Should_Return_EmptyString()
        {
            // Arrange
            var productType = ProductTypeEnum.Undefined;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo(""));
        }

        /// <summary>
        /// Test to verify that the DisplayName method returns an empty string for an unknown value.
        /// </summary>
        [Test]
        public void DisplayName_UnknownValue_Invalid_Should_Return_EmptyString()
        {
            // Arrange
            var productType = (ProductTypeEnum)999;

            // Act
            var displayName = productType.DisplayName();

            // Assert
            Assert.That(displayName, Is.EqualTo(""));
        }
    }
}