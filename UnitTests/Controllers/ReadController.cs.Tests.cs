using ContosoCrafts.WebSite.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class ReadControllerTests
    {
        private ReadController _controller;

        [SetUp]
        public void Setup()
        {
            // Initialize the ReadController before each test
            _controller = new ReadController();
        }
    }
}
