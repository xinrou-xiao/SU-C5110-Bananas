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

        /// <summary>
        /// Service used to retrieve, update, and save product details.
        /// </summary>
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
            if (Product ==null)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var updatedProduct = _productService.UpdateData(Product);
            if (updatedProduct ==null)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("/Product/Index");
        }
    }
}
