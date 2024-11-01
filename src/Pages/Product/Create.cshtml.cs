using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoCrafts.WebSite.Pages.Product
{
    public class CreateModel : PageModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService"></param>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Data Service
        public JsonFileProductService ProductService { get; }

        // Product data
        public ProductModel Product { get; private set; }

        /// <summary>
        /// Initialize Product object.
        /// </summary>
        public void OnGet()
        {
            ProductModel Product = new ProductModel();
        }

        /// <summary>
        /// Create page post receiver, will skip empty data in genre_dynamic and OTT_dynamic_platform,
        /// and set up data into Product object, then call CreateData to add this new data to json file.
        /// </summary>
        /// <param name="Product"></param>
        /// <param name="genre_dynamic">genere string array</param>
        /// <param name="OTT_dynamic_platform">platfrom name string array</param>
        /// <param name="OTT_dynamic_url">platfrom url string array</param>
        /// <param name="OTT_dynamic_icon">platfrom icon string array</param>
        /// <returns></returns>
        public IActionResult OnPost(ProductModel Product, string[] genre_dynamic, string[] OTT_dynamic_platform, string[] OTT_dynamic_url, string[] OTT_dynamic_icon)
        {

            Product.Id = System.Guid.NewGuid().ToString(); // set up id
            Product.Maker = "banana"; // maker is us

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
                Product.Genre = new string[count_Valid];
                var index = 0; // position in Gnere

                for (int i = 0; i < genre_dynamic.Length; i++)
                {
                    if (genre_dynamic[i] != null)
                    {
                        Product.Genre[index] = genre_dynamic[i];
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
                        Product.OTT.Add(OTT);
                    }
                }
            }
            // Create new data and insert to json file
            ProductService.CreateData(Product);
            return RedirectToPage("/Product/Index");
        }
    }
}
