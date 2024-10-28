using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Read page will display one product details data
    /// </summary>
    public class ReadModel : PageModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService"></param>
        public ReadModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Data Service
        public JsonFileProductService ProductService { get; }

        // Product data
        public ProductModel Product { get; private set; }

        /// <summary>
        /// Rest OnGet, require id as parameter, 
        /// will retrieve specific product data with that id
        /// will retrieve specific product data with that id, if 
        /// id is null, set Product to null and early return
        /// </summary>
        /// <param name="id"></param>
        public void OnGet(string id)
        {
            // Set Product to null and return
            if (id == null)
            {
                Product = null;
                return;
            }
            Product = ProductService.GetOneDataById(id);
        }
    }
}