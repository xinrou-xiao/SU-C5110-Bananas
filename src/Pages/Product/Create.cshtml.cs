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
        /// <returns>Redirects to the Index page on success.</returns>
        public IActionResult OnPost(ProductModel Product)
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
            if (Product.Genre.Length > 0)
            {
                int count_Valid = 0;

                // Count non-null entries in genre_dynamic
                for (int i = 0; i < Product.Genre.Length; i++)
                {
                    if (Product.Genre[i] != null)
                    {
                        count_Valid++;
                    }
                }

                // Initialize Product.Genre with valid entries
                string[] valid_genre = new string[count_Valid];
                var index = 0;

                for (int i = 0; i < Product.Genre.Length; i++)
                {
                    if (Product.Genre[i] != null)
                    {
                        valid_genre[index] = Product.Genre[i];
                        index++;
                    }
                }
                Product.Genre = valid_genre;
            }

            // Create new product data and save to JSON file
            ProductService.CreateData(Product);

            // Redirect to the Product Index page after successful creation
            return RedirectToPage("/Product/Index");
        }
    }
}