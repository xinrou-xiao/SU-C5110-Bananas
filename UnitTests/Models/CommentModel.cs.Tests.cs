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

        
    }
}
