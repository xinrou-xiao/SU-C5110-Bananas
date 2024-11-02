using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class DeleteModel : PageModel
    {
        /// Default Constructor
        public DeleteModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }
        // Data Service
        public JsonFileProductService ProductService { get; }

        // Product data
        public ProductModel Product { get; private set; }

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
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            ProductService.DeleteData(Product.Id);
            return RedirectToPage("./Index");

        }
    }
}
