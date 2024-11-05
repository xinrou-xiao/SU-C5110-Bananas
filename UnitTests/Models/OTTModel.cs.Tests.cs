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
    public class OTTModelTests
    {
        [Test]
        public void Platform_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var ottModel = new OTTModel();
            var expectedPlatform = "Netflix";

            // Act
            ottModel.Platform = expectedPlatform;

            // Assert
            Assert.That(ottModel.Platform, Is.EqualTo(expectedPlatform));
        }
        [Test]
        public void Url_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var ottModel = new OTTModel();
            var expectedUrl = "http://netflix.com";

            // Act
            ottModel.Url = expectedUrl;

            // Assert
            Assert.That(ottModel.Url, Is.EqualTo(expectedUrl));
        }

    }
}
