using ContosoCrafts.WebSite.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    public class CommentModelTests
    {
        public void Constructor_Should_Initialize_Id_With_New_Guid()
        {
            // Arrange & Act
            var commentModel = new CommentModel();

            // Assert
            Assert.That(commentModel.Id, Is.Not.Null);
            Assert.That(commentModel.Id, Is.Not.Empty);
            Assert.That(System.Guid.TryParse(commentModel.Id, out _), Is.True);
        }
        [Test]
        public void Comment_Property_Should_Be_Set_And_Retrieved_Correctly()
        {
            // Arrange
            var commentModel = new CommentModel();
            var expectedComment = "This is a test comment.";

            // Act
            commentModel.Comment = expectedComment;

            // Assert
            Assert.That(commentModel.Comment, Is.EqualTo(expectedComment));
        }


    }
}
