using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// CreateModel class for handling creation of new product data.
    /// </summary>
    public class CreateModel : PageModel
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="productService">Service for managing product data.</param>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Data service for accessing product data
        public JsonFileProductService ProductService { get; }

        // Product data
        public ProductModel Product { get; private set; }

        /// <summary>
        /// Initializes Product object.
        /// </summary>
        public void OnGet()
        {
            Product = new ProductModel();
        }

        /// <summary>
        /// Processes POST requests to create a new product entry.
        /// </summary>
        /// <param name="Product">The product to create.</param>
        /// <param name="genre_dynamic">Genre string array.</param>
        /// <param name="OTT_dynamic_platform">Platform name string array.</param>
        /// <param name="OTT_dynamic_url">Platform URL string array.</param>
        /// <param name="OTT_dynamic_icon">Platform icon string array.</param>
        /// <returns>Redirects to the Index page on success.</returns>
        public IActionResult OnPost(
            ProductModel Product,
            string[] genre_dynamic,
            string[] OTT_dynamic_platform,
            string[] OTT_dynamic_url,
            string[] OTT_dynamic_icon)
        {
            // Redirect to error page if model state is invalid
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Error");
            }

            // Generate unique ID and set maker for the new product
            Product.Id = System.Guid.NewGuid().ToString();
            Product.Maker = "banana";

            // Process genre_dynamic array if it contains data
            if (genre_dynamic.Length > 0)
            {
                int count_Valid = 0;

                // Count non-null entries in genre_dynamic
                for (int i = 0; i < genre_dynamic.Length; i++)
                {
                    if (genre_dynamic[i] != null)
                    {
                        count_Valid++;
                    }
                }

                // Initialize Product.Genre with valid entries
                Product.Genre = new string[count_Valid];
                var index = 0;

                for (int i = 0; i < genre_dynamic.Length; i++)
                {
                    if (genre_dynamic[i] != null)
                    {
                        Product.Genre[index] = genre_dynamic[i];
                        index++;
                    }
                }
            }

            // Process OTT_dynamic arrays if platform names are provided
            if (OTT_dynamic_platform.Length > 0)
            {
                for (int i = 0; i < OTT_dynamic_platform.Length; i++)
                {
                    if (OTT_dynamic_platform[i] != null) // Ensure platform name is provided
                    {
                        OTTModel OTT = new OTTModel
                        {
                            Platform = OTT_dynamic_platform[i],
                            Url = OTT_dynamic_url[i],
                            Icon = OTT_dynamic_icon[i]
                        };

                        Product.OTT.Add(OTT);
                    }
                }
            }

            // Create new product data and save to JSON file
            ProductService.CreateData(Product);

            // Redirect to the Product Index page after successful creation
            return RedirectToPage("/Product/Index");
        }
    }
}