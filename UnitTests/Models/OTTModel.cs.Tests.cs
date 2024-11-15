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
    /// Unit tests for the OTTModel class
    /// </summary>
    [TestFixture]
    public class OTTModelTests
    {
        /// <summary>
        /// Test to verify that the Platform property can be set and retrieved correctly.
        /// </summary>
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
        /// <summary>
        /// Test to verify that the Url property can be set and retrieved correctly.
        /// </summary>
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
        /// <summary>
        /// Test to verify that the Icon property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void Icon_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var ottModel = new OTTModel();
            var expectedIcon = "netflix-icon.png";

            // Act
            ottModel.Icon = expectedIcon;

            // Assert
            Assert.That(ottModel.Icon, Is.EqualTo(expectedIcon));
        }

    }
}