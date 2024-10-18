using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Read
{
    /// <summary>
    /// Index Page will return all the data to show the user
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService"></param>
        public IndexModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Data Service
        public JsonFileProductService ProductService { get; }

        // Product data
        public ProductModel Product { get; private set; }

        /// <summary>
        /// Rest OnGet, require title as parameter, 
        /// will retrieve specific product data
        /// </summary>
        /// <param name="title"></param>
        public void OnGet(string title)
        {
            Product = ProductService.GetOneDataByTitle(title);
        }
    }
}