using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    // DeleteModel class for handling delete operations on product data
    public class DeleteModel : PageModel
    {
        /// Constructor that initializes the ProductService dependency
        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Service for accessing and manipulating product data
        public JsonFileProductService ProductService { get; }

        // Holds the product to be deleted, fetched by ID in the OnGet method
        public ProductModel Product 
        { 
            get; private set; 
        }

        // Handles GET requests to display product information for deletion
        public void OnGet(string id)
        {
            // If no ID is provided, set Product to null and return early
            if (id == null)
            {
                Product = null;
                return;
            }

            // Retrieve the product with the specified ID
            Product = ProductService.GetOneDataById(id);
        }

        // Handles POST requests to delete the specified product by ID
        public IActionResult OnPost(string id)
        {
            // Return to update page if model state is invalid
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // If the ID is null or empty, return to the same page without performing any action
            if (string.IsNullOrEmpty(id))
            {
                return Page();
            }

            // Use ProductService to delete the product with the specified ID
            ProductService.DeleteData(id);

            // After deletion, redirect to the Index page
            return RedirectToPage("./Index");
        }
    }
}
