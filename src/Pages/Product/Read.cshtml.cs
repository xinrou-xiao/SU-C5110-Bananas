using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System.Linq;

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
        public ProductModel Product { get; set; }

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

        /// <summary>
        /// Calculates the average rating of the product.
        /// </summary>
        /// <returns>
        /// The average rating as a double if ratings are available; otherwise, null.
        /// </returns>
        public double? AverageRating
        {
            get
            {
                // Check if the product is null
                if (Product == null)
                {
                    // Return null if the product is null
                    return null; 
                }

                // Check if the product's ratings are null
                if (Product.Ratings == null)
                {
                    // Return null if the ratings are null
                    return null; 
                }

                // Check if there are no ratings
                if (!Product.Ratings.Any())
                {
                    // Return null if there are no ratings
                    return null; 
                }

                // Calculate and return the average rating
                return Product.Ratings.Average(); 
            }
        }
    }
}