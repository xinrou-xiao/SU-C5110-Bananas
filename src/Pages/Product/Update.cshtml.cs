using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// UpdateModel class handles update operations for product details, 
    /// allowing users to modify and save changes to existing products.
    /// </summary>
    public class UpdateModel : PageModel
    {
        // Service used to retrieve, update, and save product details.
        public readonly JsonFileProductService _productService;

        /// <summary>
        /// Constructor for UpdateModel, initializes with productService to manage 
        /// update operations for product details.
        /// </summary>
        /// <param name="productService">Service to handle product data and updates.</param>
        public UpdateModel(JsonFileProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Product data model bound to the page for updating details.
        /// </summary>
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Handles GET requests to load product details for editing by product ID.
        /// </summary>
        /// <param name="id">The ID of the product to be updated.</param>
        /// <returns>Redirects to error page if product not found, otherwise loads page with product data.</returns>
        public IActionResult OnGet(string id)
        {
            // Retrieve product details by ID to prepare for editing.
            Product = _productService.GetOneDataById(id);

            // Redirect to error page if the product is not found.
            if (Product == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        /// <summary>
        /// Handles POST requests to submit updates to product details.
        /// </summary>
        /// <param name="product">The product to update.</param>
        /// <returns>Redirects to error page if update fails, otherwise navigates to product index page on success.</returns>
        public IActionResult OnPost(ProductModel Product)
        {
            // Return to update page if model state is invalid.
            if (!ModelState.IsValid)
            {
                return Page();
            }

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

            // Attempt to update product details with provided data.
            var updatedProduct = _productService.UpdateData(Product);

            // Redirect to error page if update fails.
            if (updatedProduct == null)
            {
                return RedirectToPage("/Error");
            }

            // Redirect to the main product index page on successful update.
            return RedirectToPage("/Product/Index");
        }
    }
}