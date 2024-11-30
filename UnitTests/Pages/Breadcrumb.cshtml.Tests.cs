using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace UnitTests.Pages.Breadcrumb
{
    internal class BreadcrumbTests
    {
        #region TestSetup

        public static DefaultHttpContext httpContextDefault;
        public static IHttpContextAccessor httpContextAccessor;

        [SetUp]
        public void TestInitialize()
        {
            // Initialize the default HTTP context
            httpContextDefault = new DefaultHttpContext();

            // Mock the IHttpContextAccessor
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextDefault);
            httpContextAccessor = httpContextAccessorMock.Object;
        }

        #endregion TestSetup

        #region GenerateBreadcrumbs

        [Test]
        public void GenerateBreadcrumbs_ShouldReturnCorrectBreadcrumbs()
        {
            // Arrange
            httpContextDefault.Request.Path = new PathString("/product/details");

            // Act
            var request = httpContextAccessor.HttpContext.Request;
            var path = request.Path.Value.ToLower();
            var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var breadcrumbs = new List<string> { "<a href='/'>Home</a>" };

            for (int i = 0; i < segments.Length; i++)
            {
                var segment = segments[i];
                var url = string.Join("/", segments.Take(i + 1));

                if (segment.Contains("product"))
                {
                    breadcrumbs.Add($"<a href='/{url}'>Product</a>");
                }
                if (!segment.Contains("product"))
                {
                    if (!segment.Contains("read"))
                    {
                        if (!segment.Contains("update"))
                        {
                            if (!segment.Contains("delete"))
                            {
                                breadcrumbs.Add($"<a href='/{url}'>{segment}</a>");
                            }

                        }

                    }
                }
            }

            // Assert
            var expectedBreadcrumbs = new List<string>
            {
                "<a href='/'>Home</a>",
                "<a href='/product'>Product</a>",
                "<a href='/product/details'>details</a>"
            };

            Assert.That(breadcrumbs, Is.EqualTo(expectedBreadcrumbs));
        }

        #endregion GenerateBreadcrumbs
    }
}