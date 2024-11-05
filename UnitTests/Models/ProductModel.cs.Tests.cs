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
    public class ProductModelTests
    {
        [Test]
        public void Id_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var productModel = new ProductModel();
            var expectedId = "123";

            // Act
            productModel.Id = expectedId;

            // Assert
            Assert.That(productModel.Id, Is.EqualTo(expectedId));
        }
        [Test]
        public void Maker_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var productModel = new ProductModel();
            var expectedMaker = "Contoso";

            // Act
            productModel.Maker = expectedMaker;

            // Assert
            Assert.That(productModel.Maker, Is.EqualTo(expectedMaker));
        }
        [Test]
        public void Image_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var productModel = new ProductModel();
            var expectedImage = "image.png";

            // Act
            productModel.Image = expectedImage;

            // Assert
            Assert.That(productModel.Image, Is.EqualTo(expectedImage));
        }

    }
}
