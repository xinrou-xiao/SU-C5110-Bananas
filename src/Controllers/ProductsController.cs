using System.Collections.Generic; // Importing the namespace for collections
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC components
using ContosoCrafts.WebSite.Models; // Importing the models used in the application
using ContosoCrafts.WebSite.Services; // Importing the services used for product management

namespace ContosoCrafts.WebSite.Controllers // Defining the namespace for the controller
{
    // Specifying that this class is an API controller
    [ApiController]
    // Defining the route for the controller, which will be prefixed with "products"
    [Route("[controller]")]
    public class ProductsController : ControllerBase // Inheriting from ControllerBase for API functionality
    {
        // Constructor that takes a JsonFileProductService as a dependency
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService; // Assigning the injected service to a property
        }

        // Property to hold the product service instance
        public JsonFileProductService ProductService { get; }

        // HTTP GET method to retrieve all products
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            // Calling the service to get all product data and returning it
            return ProductService.GetAllData();
        }

        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ProductService.AddRating(request.ProductId, request.Rating);
            
            return Ok();
        }

        public class RatingRequest
        {
            public string ProductId { get; set; }
            public int Rating { get; set; }
        }
    }
}