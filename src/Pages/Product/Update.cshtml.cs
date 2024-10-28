using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// UpdateModel class handles update operations for product details, 
    /// allowing users to modify and save changes to existing products,
    /// for product data, using JsonFileProductService as its data source.
    /// </summary>
    public class UpdateModel : PageModel
    {
        private readonly JsonFileProductService _productService;

        public UpdateModel(JsonFileProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductModel Product { get; set; }

        public IActionResult OnGet(string id)
        {
            Product = _productService.GetOneDataById(id);
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
