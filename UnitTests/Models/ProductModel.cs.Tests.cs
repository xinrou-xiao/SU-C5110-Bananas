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

    }
}
