using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages
{
    public class MerchModel : PageModel
    {
        public List<MerchandiseItem> Merchandise { get; set; }

        public void OnGet()
        {
            Merchandise = new List<MerchandiseItem>
            {
                new MerchandiseItem
                {
                    Title = "Luffy Straw Hat",
                    Description = "More than just headwear.. join the straw hat crew.",

                },
            };
        }
    }
    public class MerchandiseItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Hashtags { get; set; }
        public string BuyLink { get; set; }
        public decimal Price { get; set; } 

    }
}