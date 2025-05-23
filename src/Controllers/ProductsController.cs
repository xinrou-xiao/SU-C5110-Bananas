using System.Collections.Generic; // Importing the namespace for collections
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC components
using ContosoCrafts.WebSite.Models; // Importing the models used in the application
using ContosoCrafts.WebSite.Services; // Importing the services used for product management

namespace ContosoCrafts.WebSite.Controllers // Defining the namespace for the controller
{
    /// <summary>
    /// API controller for managing products.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase // Inheriting from ControllerBase for API functionality
    {
        /// <summary>
        /// Constructor that takes a JsonFileProductService as a dependency.
        /// </summary>
        /// <param name="productService">The product service instance.</param>
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService; // Assigning the injected service to a property
        }

        // Property to hold the product service instance
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// HTTP GET method to retrieve all products.
        /// </summary>
        /// <returns>An IEnumerable of ProductModel.</returns>
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            // Calling the service to get all product data and returning it
            return ProductService.GetAllData();
        }

        /// <summary>
        /// HTTP PATCH method to update the rating of a product.
        /// </summary>
        /// <param name="request">The rating request containing product ID and rating.</param>
        /// <returns>An ActionResult indicating the result of the operation.</returns>
        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            // Calling the service to add a rating for the specified product
            ProductService.AddRating(request.ProductId, request.Rating);

            // Returning a 200 OK response to indicate success
            return Ok();
        }

        /// <summary>
        /// Inner class to represent the request body for rating updates.
        /// </summary>
        public class RatingRequest
        {
            // Property to hold the product ID
            public string ProductId { get; set; }

            // Property to hold the rating value
            public int Rating { get; set; }
        }
    }
}