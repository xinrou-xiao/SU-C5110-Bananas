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
        [Test]
        public void Url_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var productModel = new ProductModel();
            var expectedUrl = "http://example.com";

            // Act
            productModel.Url = expectedUrl;

            // Assert
            Assert.That(productModel.Url, Is.EqualTo(expectedUrl));
        }
        [Test]
        public void Title_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var productModel = new ProductModel();
            var expectedTitle = "Product Title";

            // Act
            productModel.Title = expectedTitle;

            // Assert
            Assert.That(productModel.Title, Is.EqualTo(expectedTitle));
        }
        [Test]
        public void Description_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var productModel = new ProductModel();
            var expectedDescription = "This is a product description.";

            // Act
            productModel.Description = expectedDescription;

            // Assert
            Assert.That(productModel.Description, Is.EqualTo(expectedDescription));
        }
        [Test]
        public void Release_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var productModel = new ProductModel();
            var expectedRelease = "2023";

            // Act
            productModel.Release = expectedRelease;

            // Assert
            Assert.That(productModel.Release, Is.EqualTo(expectedRelease));
        }
        [Test]
        public void Trailer_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var productModel = new ProductModel();
            var expectedTrailer = "trailer.mp4";

            // Act
            productModel.Trailer = expectedTrailer;

            // Assert
            Assert.That(productModel.Trailer, Is.EqualTo(expectedTrailer));
        }
        [Test]
        public void OTT_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var productModel = new ProductModel();
            var expectedOTT = new List<OTTModel> { new OTTModel { Platform = "Netflix" } };

            // Act
            productModel.OTT = expectedOTT;

            // Assert
            Assert.That(productModel.OTT, Is.EqualTo(expectedOTT));
        }
        [Test]
        public void Season_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var productModel = new ProductModel();
            var expectedSeason = 2;

            // Act
            productModel.Season = expectedSeason;

            // Assert
            Assert.That(productModel.Season, Is.EqualTo(expectedSeason));
        }


    }
}
