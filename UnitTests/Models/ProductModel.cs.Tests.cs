using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using System;

namespace UnitTests.Models
{
    /// <summary>
    /// Unit tests for the ProductModel class.
    /// </summary>
    [TestFixture]
    public class ProductModelTests
    {
        /// <summary>
        /// Tests that the Id property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void Id_SetAndRetrieve_Should_Work_Correctly()
        {
            // Arrange: Create a new ProductModel instance and define the expected Id value.
            var productModel = new ProductModel();
            var expectedId = "123";

            // Act: Set the Id property.
            productModel.Id = expectedId;

            // Assert: Verify that the Id property was set correctly.
            Assert.That(productModel.Id, Is.EqualTo(expectedId));
        }

        /// <summary>
        /// Tests that the Maker property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void Maker_Set_And_Retrieve_Should_Work_Correctly()
        {
            // Arrange: Create a new ProductModel instance and define the expected Maker value.
            var productModel = new ProductModel();
            var expectedMaker = "Contoso";

            // Act: Set the Maker property.
            productModel.Maker = expectedMaker;

            // Assert: Verify that the Maker property was set correctly.
            Assert.That(productModel.Maker, Is.EqualTo(expectedMaker));
        }

        /// <summary>
        /// Tests that the Image property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void Image_Set_And_Retrieve_Should_Work_Correctly()
        {
            // Arrange: Create a new ProductModel instance and define the expected Image value.
            var productModel = new ProductModel();
            var expectedImage = "image.png";

            // Act: Set the Image property.
            productModel.Image = expectedImage;

            // Assert: Verify that the Image property was set correctly.
            Assert.That(productModel.Image, Is.EqualTo(expectedImage));
        }

        /// <summary>
        /// Tests that the Url property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void Url_Set_And_Retrieve_Should_Work_Correctly()
        {
            // Arrange: Create a new ProductModel instance and define the expected Url value.
            var productModel = new ProductModel();
            var expectedUrl = "http://example.com";

            // Act: Set the Url property.
            productModel.Url = expectedUrl;

            // Assert: Verify that the Url property was set correctly.
            Assert.That(productModel.Url, Is.EqualTo(expectedUrl));
        }

        /// <summary>
        /// Tests that the Title property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void Title_Set_And_Retrieve_Should_Work_Correctly()
        {
            // Arrange: Create a new ProductModel instance and define the expected Title value.
            var productModel = new ProductModel();
            var expectedTitle = "Product Title";

            // Act: Set the Title property.
            productModel.Title = expectedTitle;

            // Assert: Verify that the Title property was set correctly.
            Assert.That(productModel.Title, Is.EqualTo(expectedTitle));
        }

        /// <summary>
        /// Tests that the Description property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void Description_Set_And_Retrieve_Should_Work_Correctly()
        {
            // Arrange: Create a new ProductModel instance and define the expected Description value.
            var productModel = new ProductModel();
            var expectedDescription = "This is a product description.";

            // Act: Set the Description property.
            productModel.Description = expectedDescription;

            // Assert: Verify that the Description property was set correctly.
            Assert.That(productModel.Description, Is.EqualTo(expectedDescription));
        }

        /// <summary>
        /// Tests that the Release property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void Release_Set_And_Retrieve_Should_Work_Correctly()
        {
            // Arrange: Create a new ProductModel instance and define the expected Release value.
            var productModel = new ProductModel();
            var expectedRelease = "2023";

            // Act: Set the Release property.
            productModel.Release = expectedRelease;

            // Assert: Verify that the Release property was set correctly.
            Assert.That(productModel.Release, Is.EqualTo(expectedRelease));
        }

        /// <summary>
        /// Tests that the Trailer property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void Trailer_Set_And_Retrieve_Should_Work_Correctly()
        {
            // Arrange: Create a new ProductModel instance and define the expected Trailer value.
            var productModel = new ProductModel();
            var expectedTrailer = "trailer.mp4";

            // Act: Set the Trailer property.
            productModel.Trailer = expectedTrailer;

            // Assert: Verify that the Trailer property was set correctly.
            Assert.That(productModel.Trailer, Is.EqualTo(expectedTrailer));
        }

        /// <summary>
        /// Tests that the OTT property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void OTT_Set_And_Retrieve_Should_Work_Correctly()
        {
            // Arrange: Create a new ProductModel instance and define the expected OTT value.
            var productModel = new ProductModel();
            var expectedOTT = new OttTypeEnum[] { OttTypeEnum.Netflix };

            // Act: Set the OTT property.
            productModel.Ott = expectedOTT;

            // Assert: Verify that the OTT property was set correctly.
            Assert.That(productModel.Ott, Is.EqualTo(expectedOTT));
        }

        /// <summary>
        /// Tests that the Season property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void Season_Set_And_Retrieve_Should_Work_Correctly()
        {
            // Arrange: Create a new ProductModel instance and define the expected Season value.
            var productModel = new ProductModel();
            var expectedSeason = 2;

            // Act: Set the Season property.
            productModel.Season = expectedSeason;

            // Assert: Verify that the Season property was set correctly.
            Assert.That(productModel.Season, Is.EqualTo(expectedSeason));
        }

        /// <summary>
        /// Tests that a newly created CommentModel instance has an empty Text property.
        /// </summary>
        [Test]
        public void CommentModel_WhenCreated_ShouldHaveEmptyText()
        {
            // Arrange & Act: Create a new CommentModel instance.
            var comment = new CommentModel();

            // Assert: Verify that the Text property is or empty.
            Assert.That(comment.Text, Is.Null.Or.Empty);
        }

        /// <summary>
        /// Tests that a newly created CommentModel instance has the default Timestamp value.
        /// </summary>
        [Test]
        public void CommentModel_WhenCreated_ShouldHaveDefaultTimestamp()
        {
            // Arrange & Act: Create a new CommentModel instance.
            var comment = new CommentModel();

            // Assert: Verify that the Timestamp property is set to the default DateTime value.
            Assert.That(comment.Timestamp, Is.EqualTo(default(DateTime)));
        }

        /// <summary>
        /// Tests that a newly created CommentModel instance has a EditingComment property.
        /// </summary>
        [Test]
        public void CommentModel_WhenCreated_EditingCommentShouldBeNull()
        {
            // Arrange & Act: Create a new CommentModel instance.
            var comment = new CommentModel();

            // Assert: Verify that the editingComment property is.
            Assert.That(comment.editingComment, Is.Null);
        }

        /// <summary>
        /// Tests that the Text property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void CommentModel_SetText_ShouldUpdateText()
        {
            // Arrange: Create a new CommentModel instance and define the expected Text value.
            var comment = new CommentModel();
            var expectedText = "Test comment";

            // Act: Set the Text property.
            comment.Text = expectedText;

            // Assert: Verify that the Text property was set correctly.
            Assert.That(comment.Text, Is.EqualTo(expectedText));
        }

        /// <summary>
        /// Tests that the Timestamp property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void CommentModel_SetTimestamp_ShouldUpdateTimestamp()
        {
            // Arrange: Create a new CommentModel instance and define the expected Timestamp value.
            var comment = new CommentModel();
            var expectedTime = DateTime.Now;

            // Act: Set the Timestamp property.
            comment.Timestamp = expectedTime;

            // Assert: Verify that the Timestamp property was set correctly.
            Assert.That(comment.Timestamp, Is.EqualTo(expectedTime));
        }

        /// <summary>
        /// Tests that the EditingComment property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public void CommentModel_SetEditingComment_ShouldUpdateEditingComment()
        {
            // Arrange: Create a new CommentModel instance and define the expected EditingComment value.
            var comment = new CommentModel();
            var editingComment = new CommentModel
            {
                Text = "Editing this comment",
                Timestamp = DateTime.Now
            };

            // Act: Set the editingComment property.
            comment.editingComment = editingComment;

            // Assert: Verify that the editingComment property was set correctly.
            Assert.Multiple(() =>
            {
                Assert.That(comment.editingComment, Is.Not.Null);
                Assert.That(comment.editingComment.Text, Is.EqualTo(editingComment.Text));
                Assert.That(comment.editingComment.Timestamp, Is.EqualTo(editingComment.Timestamp));
            });
        }

        /// <summary>
        /// Tests that a CommentModel instance created with specific values has the correct properties.
        /// </summary>
        [Test]
        public void CommentModel_CreateWithValues_ShouldHaveCorrectProperties()
        {
            // Arrange: Define the expected Text and Timestamp values.
            var expectedText = "Test comment";
            var expectedTime = DateTime.Now;

            // Act: Create a new CommentModel instance with the specified values.
            var comment = new CommentModel
            {
                Text = expectedText,
                Timestamp = expectedTime
            };

            // Assert: Verify that the properties were set correctly.
            Assert.Multiple(() =>
            {
                Assert.That(comment.Text, Is.EqualTo(expectedText));
                Assert.That(comment.Timestamp, Is.EqualTo(expectedTime));
                Assert.That(comment.editingComment, Is.Null);
            });
        }

        /// <summary>
        /// Tests that the Text property can be set to.
        /// </summary>
        [Test]
        public void CommentModel_SetNullText_ShouldAllowNull()
        {
            // Arrange: Create a new CommentModel instance.
            var comment = new CommentModel();

            // Act: Set the Text property to.
            comment.Text =null;

            // Assert: Verify that the Text property is.
            Assert.That(comment.Text, Is.Null);
        }

        /// <summary>
        /// Tests that the EditingComment property can be cleared by setting it to.
        /// </summary>
        [Test]
        public void CommentModel_ClearEditingComment_ShouldSetToNull()
        {
            // Arrange: Create a new CommentModel instance and set the EditingComment property.
            var comment = new CommentModel();
            comment.editingComment = new CommentModel();

            // Act: Clear the EditingComment property by setting it to.
            comment.editingComment =null;

            // Assert: Verify that the EditingComment property is.
            Assert.That(comment.editingComment, Is.Null);
        }
    }
}