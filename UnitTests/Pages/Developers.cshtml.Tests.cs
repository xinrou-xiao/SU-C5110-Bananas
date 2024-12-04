using NUnit.Framework;
using ContosoCrafts.WebSite.Pages;

namespace UnitTests.Pages
{
    [TestFixture]
    internal class DevelopersTests
    {
        // Instance of the DevelopersModel to test
        private DevelopersModel pageModel;

        #region TestSetup

        /// <summary>
        /// This method runs before each test and initializes the pageModel instance.
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            // Create a new instance of DevelopersModel before each test
            pageModel = new DevelopersModel();
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Test to verify that the Developers list is null before OnGet is called.
        /// </summary>
        [Test]
        public void OnGet_Developers_Initial_Value_Should_Be_Null()
        {
            // Assert
            Assert.That(pageModel.Developers, Is.Null); // Verify Developers is null before OnGet
        }

        /// <summary>
        /// Test to verify that the OnGet method populates the Developers list with four developers.
        /// </summary>
        [Test]
        public void OnGet_Valid_Call_Should_Set_Developer_To_List_And_Has_Four_Elements()
        {
            // Act
            pageModel.OnGet(); // Call OnGet to populate the Developers list

            // Assert
            Assert.That(pageModel.Developers, Is.Not.Null); // Ensure the list is not null
            Assert.That(pageModel.Developers.Count, Is.EqualTo(4)); // Verify there are 4 developers
        }

        /// <summary>
        /// Test to verify that the first developer's details are populated correctly.
        /// </summary>
        [Test]
        public void OnGet_Valid_Call_First_Element_Should_Be_Xinrou()
        {
            // Act
            pageModel.OnGet(); // Call OnGet to populate the Developers list

            // Assert
            // Verify the first developer's details are correctly populated
            Assert.That(pageModel.Developers[0].Name, Is.EqualTo("Xin Rou Xiao"));
            Assert.That(pageModel.Developers[0].Email, Is.EqualTo("xxiao@seattleu.edu"));
            Assert.That(pageModel.Developers[0].ImageUrl, Is.EqualTo("https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/50925624-027b-4835-bd54-9d934e1a8719/dfpfvo6-a6fb18d1-f982-4351-9a13-16ae03888ff9.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcLzUwOTI1NjI0LTAyN2ItNDgzNS1iZDU0LTlkOTM0ZTFhODcxOVwvZGZwZnZvNi1hNmZiMThkMS1mOTgyLTQzNTEtOWExMy0xNmFlMDM4ODhmZjkuanBnIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmZpbGUuZG93bmxvYWQiXX0.XTT6YCEkGDGKZ66I2O-G2L9fHhO9-4rq0K5UlaSsBJM"));
        }

        /// <summary>
        /// Test to verify that the second developer's details are populated correctly.
        /// </summary>
        [Test]
        public void OnGet_Valid_Call_First_Element_Should_Be_Shanvi()
        {
            // Act
            pageModel.OnGet(); // Call OnGet to populate the Developers list

            // Assert
            // Verify the second developer's details are correctly populated
            Assert.That(pageModel.Developers[1].Name, Is.EqualTo("Shanvi Sinha"));
            Assert.That(pageModel.Developers[1].Email, Is.EqualTo("ssinha1@seattleu.edu"));
            Assert.That(pageModel.Developers[1].ImageUrl, Is.EqualTo("https://img.freepik.com/premium-photo/female-developer-background_665280-9655.jpg?w=740"));
        }

        /// <summary>
        /// Test to verify that the third developer's details are populated correctly.
        /// </summary>
        [Test]
        public void OnGet_Valid_Call_First_Element_Should_Be_Samarth()
        {
            // Act
            pageModel.OnGet(); // Call OnGet to populate the Developers list

            // Assert
            // Verify the third developer's details are correctly populated
            Assert.That(pageModel.Developers[2].Name, Is.EqualTo("Samarth Tanwar"));
            Assert.That(pageModel.Developers[2].Email, Is.EqualTo("stanwar@seattleu.edu"));
            Assert.That(pageModel.Developers[2].ImageUrl, Is.EqualTo("https://img.freepik.com/premium-photo/dog-with-headphones-it-words-sound-back_887562-1432.jpg"));
        }

        /// <summary>
        /// Test to verify that the fourth developer's details are populated correctly.
        /// </summary>
        [Test]
        public void OnGet_Valid_Call_First_Element_Should_Be_Vineet()
        {
            // Act
            pageModel.OnGet(); // Call OnGet to populate the Developers list

            // Assert
            // Verify the fourth developer's details are correctly populated
            Assert.That(pageModel.Developers[3].Name, Is.EqualTo("Vineet Somwanshi"));
            Assert.That(pageModel.Developers[3].Email, Is.EqualTo("vsomwanshi@seattleu.edu"));
            Assert.That(pageModel.Developers[3].ImageUrl, Is.EqualTo("https://t3.ftcdn.net/jpg/06/01/17/18/360_F_601171862_l7yZ0wujj8o2SowiKTUsfLEEx8KunYNd.jpg"));
        }

        #endregion OnGet
    }
}