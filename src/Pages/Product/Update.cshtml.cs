using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// UpdateModel class handles update operations for product details, 
    /// allowing users to modify and save changes to existing products,
    /// </summary>
    public class UpdateModel : PageModel
    {
        // Service used to retrieve, update, and save product details.
        private readonly JsonFileProductService _productService;

        /// <summary>
        /// Constructor for UpdateModel, initializes with productService to manage 
        /// update operations for product details.
        /// </summary>
        /// <param name="productService">Service to handle product data and updates</param>
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
        /// <param name="id">The ID of the product to be updated</param>
        /// <returns>Redirects to error page if product not found, otherwise loads page with product data</returns>
        public IActionResult OnGet(string id)
        {
            // Retrieve product details by ID to prepare for editing
            Product = _productService.GetOneDataById(id);

            // Redirect to error page if the product is not found
            if (Product == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        /// <summary>
        /// Handles POST requests to submit updates to product details.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <param name="genre_dynamic">Genre string array.</param>
        /// <param name="OTT_dynamic_platform">Platform name string array.</param>
        /// <param name="OTT_dynamic_url">Platform URL string array.</param>
        /// <param name="OTT_dynamic_icon">Platform icon string array.</param>
        /// <returns>Redirects to error page if update fails, otherwise navigates to product index page on success</returns>
        public IActionResult OnPost(ProductModel product, string[] genre_dynamic,
            string[] OTT_dynamic_platform, string[] OTT_dynamic_url, string[] OTT_dynamic_icon)
        {
            // Return to update page if model state is invalid
            if (!ModelState.IsValid)
            {
                return Page();
            }

            product.Genre = new string[] { };
            product.OTT = new System.Collections.Generic.List<OTTModel>();

            if (genre_dynamic.Length > 0) // if genre_dynamic has data inside
            {
                int count_Valid = 0; // to count how many valid gnere are valid
                for (int i = 0; i < genre_dynamic.Length; i++)
                {
                    // if genre is not null, count++
                    if (genre_dynamic[i] != null)
                    {
                        count_Valid++;
                    }
                }

                // set up Product.Genre to a correct length's array
                product.Genre = new string[count_Valid];
                var index = 0; // position in Gnere

                for (int i = 0; i < genre_dynamic.Length; i++)
                {
                    if (genre_dynamic[i] != null)
                    {
                        product.Genre[index] = genre_dynamic[i];
                        index++;
                    }
                }
            }

            // if platform name has data inside
            if (OTT_dynamic_platform.Length > 0)
            {
                for (int i = 0; i < OTT_dynamic_platform.Length; i++)
                {
                    if (OTT_dynamic_platform[i] != null) // must at least provide platform name
                    {
                        OTTModel OTT = new OTTModel();
                        OTT.Platform = OTT_dynamic_platform[i];
                        OTT.Url = OTT_dynamic_url[i];
                        OTT.Icon = OTT_dynamic_icon[i];
                        product.OTT.Add(OTT);
                    }
                }
            }

            // Attempt to update product details with provided data
            var updatedProduct = _productService.UpdateData(product);

            if (updatedProduct == null)
            {
                return RedirectToPage("/Error");
            }

            // Redirect to the main product index page on successful update
            return RedirectToPage("/Product/Index");
        }
    }
}