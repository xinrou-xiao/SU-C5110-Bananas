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
        /// <param name="genre_dynamic">Array of genre strings.</param>
        /// <param name="OTT_dynamic_platform">Array of platform name strings.</param>
        /// <param name="OTT_dynamic_url">Array of platform URL strings.</param>
        /// <param name="OTT_dynamic_icon">Array of platform icon strings.</param>
        /// <returns>Redirects to error page if update fails, otherwise navigates to product index page on success.</returns>
        public IActionResult OnPost(ProductModel product, string[] genre_dynamic,
            string[] OTT_dynamic_platform, string[] OTT_dynamic_url, string[] OTT_dynamic_icon)
        {
            // Return to update page if model state is invalid.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Initialize product genre and OTT list.
            product.Genre = null;
            product.OTT = new System.Collections.Generic.List<OTTModel>();

            // Process genre_dynamic array to populate product.Genre.
            if (genre_dynamic.Length > 0)
            {
                // Count valid genres.
                int count_Valid = 0;

                for (int i = 0; i < genre_dynamic.Length; i++)
                {
                    // Increment count if genre is not null.
                    if (genre_dynamic[i] != null)
                    {
                        count_Valid++;
                    }
                }

                // Initialize product.Genre with the correct length.
                product.Genre = new string[count_Valid];

                // Position in Genre array.
                var index = 0;

                for (int i = 0; i < genre_dynamic.Length; i++)
                {
                    if (genre_dynamic[i] != null)
                    {
                        product.Genre[index] = genre_dynamic[i];
                        index++;
                    }
                }
            }

            // Process OTT_dynamic arrays to populate product.OTT.
            if (OTT_dynamic_platform.Length > 0)
            {
                for (int i = 0; i < OTT_dynamic_platform.Length; i++)
                {
                    // Ensure platform name is provided.
                    if (OTT_dynamic_platform[i] != null)
                    {
                        OTTModel OTT = new OTTModel();
                        OTT.Platform = OTT_dynamic_platform[i];
                        OTT.Url = OTT_dynamic_url[i];
                        OTT.Icon = OTT_dynamic_icon[i];
                        product.OTT.Add(OTT);
                    }
                }
            }

            // Attempt to update product details with provided data.
            var updatedProduct = _productService.UpdateData(product);

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