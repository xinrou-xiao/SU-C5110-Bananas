using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Index Page will return all the data to show the user.
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="productService">Service to retrieve product data.</param>
        public IndexModel(JsonFileProductService productService)
        {
            // Initialize product service
            ProductService = productService;
        }

        // Data Service
        public JsonFileProductService ProductService
        {
            get;
        }

        // Collection of the Data
        public IEnumerable<ProductModel> Products
        {
            get; private set;
        }

        /// <summary>
        /// REST OnGet.
        /// Return all the data.
        /// </summary>
        public void OnGet()
        {
            // Retrieve all products
            Products = ProductService.GetAllData();
        }
    }
}