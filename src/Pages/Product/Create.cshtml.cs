using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class CreateModel : PageModel
    {


        // Data Service
        public JsonFileProductService ProductService { get; }

        // Product data
        public ProductModel Product { get; private set; }

        public void OnGet()
        {
        }
    }
}
