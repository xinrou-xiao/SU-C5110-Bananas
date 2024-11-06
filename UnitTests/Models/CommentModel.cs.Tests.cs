using ContosoCrafts.WebSite.Models;
using NUnit.Framework;

namespace UnitTests.Models
{
    [TestFixture]
    public class CommentModelTests
    {
        [Test]
        public void Constructor_Should_Initialize_Id_With_Unique_Value()
        {
            // Arrange & Act
            var comment1 = new CommentModel();
            var comment2 = new CommentModel();

            // Assert
            Assert.That(comment1.Id, Is.Not.Null.And.Not.Empty, "Expected Id to be initialized with a non-null, non-empty value.");
            Assert.That(comment2.Id, Is.Not.Null.And.Not.Empty, "Expected Id to be initialized with a non-null, non-empty value.");
            Assert.That(comment1.Id, Is.Not.EqualTo(comment2.Id), "Expected each instance to have a unique Id.");
        }

        [Test]
        public void Comment_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var comment = new CommentModel();
            var expectedComment = "This is a test comment.";

            // Act
            comment.Comment = expectedComment;

            // Assert
            Assert.That(comment.Comment, Is.EqualTo(expectedComment), "Expected Comment property to be set and retrieved correctly.");
        }

        [Test]
        public void Id_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var comment = new CommentModel();
            var expectedId = "test-id";

            // Act
            comment.Id = expectedId;

            // Assert
            Assert.That(comment.Id, Is.EqualTo(expectedId), "Expected Id property to be set and retrieved correctly.");
        }
    }
}
