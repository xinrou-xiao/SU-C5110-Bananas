using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    [TestFixture]
    public class ProductTypeEnumTests
    {
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

    }
}
