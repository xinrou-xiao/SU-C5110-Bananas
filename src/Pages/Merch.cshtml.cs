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
                    Description = "More than just headwear... join the straw hat crew.",
                    ImageUrl = "https://store.crunchyroll.com/on/demandware.static/-/Sites-crunchyroll-master-catalog/default/dw043745ff/images/3665361049524_one-piece-luffy-straw-hat_01.jpg",
                    Hashtags = new List<string> { "OnePiece", "Pirate", "hat" },
                    BuyLink = "https://store.crunchyroll.com/products/one-piece-luffy-straw-hat-3665361049524.html",
                    Price = 19.99m
                },
                new MerchandiseItem
                {
                     Title = "Demon Slayer Sword",
                    Description = "Unleash your inner demon slayer with this katana",
                    ImageUrl = "https://makotoswords.com/cdn/shop/files/Kamado_Tanjirou3_4156966b-6411-453d-b7f9-a5a5279a0b77_1080x.jpg?v=1690178718",
                    Hashtags = new List<string> { "DemonSlayer", "Katana"},
                    BuyLink = "https://makotoswords.com/products/handmade-katana-cosplay-replica-anime-swords?currency=USD&variant=44377331433686&utm_source=google&utm_medium=cpc&utm_campaign=Google%20Shopping&stkn=dcd0a93543e3&gad_source=1&gclid=Cj0KCQiA_9u5BhCUARIsABbMSPvGIUPfQp0mWSr2Hw9mqSwMWbFXq0uQWRQwot6i7JxkEtwCF1GNggUaAh-gEALw_wcB",
                    Price = 19.99m
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