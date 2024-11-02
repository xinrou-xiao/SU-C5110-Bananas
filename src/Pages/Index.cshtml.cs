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

        public JsonFileProductService ProductService { get; }
        public IEnumerable<ProductModel> Products { get; private set; }

        public void OnGet()
        {
            Products = ProductService.GetAllData();
        }
    }
}