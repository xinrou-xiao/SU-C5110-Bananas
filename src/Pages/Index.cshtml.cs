using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{

    /// <summary>
    /// Model for the Index page that handles the retrieval and display of product data.
    /// </summary>
    public class IndexModel : PageModel
    {
        // Logger to log messages for this page
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Constructor to initialize the IndexModel with a logger and product service.
        /// </summary>
        /// <param name="logger">The logger used for logging information.</param>
        /// <param name="productService">Service to retrieve product data.</param>
        public IndexModel(ILogger<IndexModel> logger,

            JsonFileProductService productService)
        {
            // Assign the logger
            _logger = logger;
            // Assign the product service
            ProductService = productService;
        }

        /// <summary>
        /// Gets the product service used to access product data.
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Collection of products to be displayed on the Index page.
        /// </summary>
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// Method called on GET requests to retrieve product data.
        /// This method populates the Products property with data from the product service.
        /// </summary>
        public void OnGet()
        {
            // Retrieve all products
            Products = ProductService.GetAllData();
        }
    }
}