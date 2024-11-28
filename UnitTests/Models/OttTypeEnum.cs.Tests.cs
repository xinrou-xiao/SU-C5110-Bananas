using ContosoCrafts.WebSite.Models;
using NUnit.Framework;

namespace UnitTests.Models
{
    /// <summary>
    /// Unit tests for the OttTypeEnum clas.
    /// </summary>
    [TestFixture]
    public class OttTypeEnumTests
    {
        #region GetUrl

        /// <summary>
        /// Verifies that the GetUrl function for all Enum type.
        /// </summary>
        [Test]
        public void GetUrl_Valid_Data_Should_Return_Correct_Url()
        {
            // Arrange

            // Act
            string resultNetflix = OttTypeEnum.Netflix.GetUrl();
            string resultPrime = OttTypeEnum.Prime.GetUrl();
            string resultCrunchyroll = OttTypeEnum.Crunchyroll.GetUrl();
            string resultTubitv = OttTypeEnum.tubitv.GetUrl();
            string resultNetflHuluix = OttTypeEnum.Hulu.GetUrl();

            // Assert
            Assert.That(resultNetflix, Is.EqualTo("https://www.netflix.com/"));
            Assert.That(resultPrime, Is.EqualTo("https://www.amazon.com/gp/video/storefront"));
            Assert.That(resultCrunchyroll, Is.EqualTo("https://www.crunchyroll.com/"));
            Assert.That(resultTubitv, Is.EqualTo("https://tubitv.com/"));
            Assert.That(resultNetflHuluix, Is.EqualTo("https://www.hulu.com/welcome?orig_referrer=https%3A%2F%2Fwww.google.com%2F"));
        }

        /// <summary>
        /// Verifies that the GetUrl function for all Undefined type.
        /// </summary>
        [Test]
        public void GetUrl_Undefined_Data_Should_Return_Empty_String()
        {
            // Arrange

            // Act
            string result = OttTypeEnum.Undefined.GetUrl();

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }

        #endregion GetUrl

        #region GetIcon

        /// <summary>
        /// Verifies that the GetIcon function for all Enum type.
        /// </summary>
        [Test]
        public void GetIcon_Valid_Data_Should_Return_Correct_Url()
        {
            // Arrange

            // Act
            string resultNetflix = OttTypeEnum.Netflix.GetIcon();
            string resultPrime = OttTypeEnum.Prime.GetIcon();
            string resultCrunchyroll = OttTypeEnum.Crunchyroll.GetIcon();
            string resultTubitv = OttTypeEnum.tubitv.GetIcon();
            string resultHulu = OttTypeEnum.Hulu.GetIcon();

            // Assert
            Assert.That(resultNetflix, Is.EqualTo("https://images.ctfassets.net/y2ske730sjqp/1aONibCke6niZhgPxuiilC/2c401b05a07288746ddf3bd3943fbc76/BrandAssets_Logos_01-Wordmark.jpg?w=940"));
            Assert.That(resultPrime, Is.EqualTo("https://assets.aboutamazon.com/dims4/default/59e4166/2147483647/strip/true/crop/4454x2634+0+0/resize/365x216!/format/webp/quality/90/?url=https%3A%2F%2Famazon-blogs-brightspot.s3.amazonaws.com%2F4b%2F7f%2F4a4a80724a5e9a6b4a1931e8bf99%2Fprime-logo-rgb-prime-blue-master.png"));
            Assert.That(resultCrunchyroll, Is.EqualTo("https://upload.wikimedia.org/wikipedia/commons/thumb/d/d6/Crunchyroll.svg/768px-Crunchyroll.svg.png"));
            Assert.That(resultTubitv, Is.EqualTo("https://upload.wikimedia.org/wikipedia/commons/c/c5/Tubi_logo_2024_purple.svg"));
            Assert.That(resultHulu, Is.EqualTo("https://greenhouse.hulu.com/app/uploads/sites/12/2023/10/logo-gradient-3up.svg"));
        }

        /// <summary>
        /// Verifies that the GetIcon function for all Undefined type.
        /// </summary>
        [Test]
        public void GetIcon_Undefined_Data_Should_Return_Empty_String()
        {
            // Arrange

            // Act
            string result = OttTypeEnum.Undefined.GetIcon();

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }
        #endregion GetIcon
    }
}