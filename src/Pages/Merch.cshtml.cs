using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// The MerchModel class handles the display of merchandise items on the website.
    /// It stores and provides the list of available merchandise items.
    /// </summary>
    public class MerchModel : PageModel
    {
        /// <summary>
        /// Gets or sets the list of merchandise items.
        /// </summary>
        public List<MerchandiseItem> Merchandise { get; set; }

        /// <summary>
        /// OnGet method is called when the page is first accessed.
        /// It initializes the Merchandise list with predefined items.
        /// </summary>
        public void OnGet()
        {
            // Initialize the Merchandise list with merchandise items.
            Merchandise = new List<MerchandiseItem>
            {
                new MerchandiseItem
                {
                    Title = "Luffy Straw Hat",
                    Description = "More than just headwear... become part of the straw hat crew.",
                    ImageUrl = "https://store.crunchyroll.com/on/demandware.static/-/Sites-crunchyroll-master-catalog/default/dw043745ff/images/3665361049524_one-piece-luffy-straw-hat_01.jpg",
                    Hashtags = new List<string> { "OnePiece", "Pirate", "hat" },
                    BuyLink = "https://store.crunchyroll.com/products/one-piece-luffy-straw-hat-3665361049524.html",
                    Price = 19.99m
                },
                new MerchandiseItem
                {
                     Title = "Demon Slayer Sword",
                    Description = "Unleash your inner demon slayer with this battle-ready katana",
                    ImageUrl = "https://makotoswords.com/cdn/shop/files/Kamado_Tanjirou3_4156966b-6411-453d-b7f9-a5a5279a0b77_1080x.jpg?v=1690178718",
                    Hashtags = new List<string> { "DemonSlayer", "Katana"},
                    BuyLink = "https://makotoswords.com/products/handmade-katana-cosplay-replica-anime-swords?currency=USD&variant=44377331433686&utm_source=google&utm_medium=cpc&utm_campaign=Google%20Shopping&stkn=dcd0a93543e3&gad_source=1&gclid=Cj0KCQiA_9u5BhCUARIsABbMSPvGIUPfQp0mWSr2Hw9mqSwMWbFXq0uQWRQwot6i7JxkEtwCF1GNggUaAh-gEALw_wcB",
                    Price = 149.99m
                },
                new MerchandiseItem
                {
                     Title = "Gojo Motion Sticker",
                    Description = "Bring Gojo’s domain expansion to life with this motion sticker",
                    ImageUrl = "https://seakoff.com/cdn/shop/files/gojo-motion-sticker-194613.gif?v=1711420262&width=1100",
                    Hashtags = new List<string> { "JujutsuKaisen", "Gojo"},
                    BuyLink = "https://seakoff.com/products/gojo-motion-sticker",
                    Price = 3.00m
                },
                new MerchandiseItem
                {
                     Title = "Hunter x Hunter License",
                     Description = "Prove your Hunter status with this sleek license card.",
                     ImageUrl = "https://i.etsystatic.com/30827093/r/il/5cdc55/6311016041/il_1588xN.6311016041_qot7.jpg",
                     Hashtags = new List<string> { "HunterxHunter", "License" },
                     BuyLink = "https://www.etsy.com/listing/1773719044/hunter-x-hunter-license-card-plastic?gpla=1&gao=1&&utm_source=google&utm_medium=cpc&utm_campaign=shopping_us_c-paper_and_party_supplies&utm_custom1=_k_Cj0KCQiA_9u5BhCUARIsABbMSPusWv2u_QZa7teAuEUUnjZQdntJbUMdD7_eMaexKUqNweLv_3KLQ-8aAg5NEALw_wcB_k_&utm_content=go_21500568603_167985816239_716809480513_aud-2079782229334:pla-314261241107_c__1773719044_12768591&utm_custom2=21500568603&gad_source=1&gclid=Cj0KCQiA_9u5BhCUARIsABbMSPusWv2u_QZa7teAuEUUnjZQdntJbUMdD7_eMaexKUqNweLv_3KLQ-8aAg5NEALw_wcB",
                     Price = 6.61m
                },
                new MerchandiseItem
                {
                    Title = "Itachi Sharingan Sneakers",
                    Description = "Step into Itachi's world with these custom J1 sneakers",
                    ImageUrl = "https://img.thesitebase.net/10266/10266415/products/ver_1427bf7a363dbbf192d74649a891f0990/1686206061ac8b030366.jpeg?width=3840&height=0&min_height=0",
                    Hashtags = new List<string> { "Naruto", "Itachi" },
                    BuyLink = "https://www.gearanime.com/products/akt-itachi-sharingan-eyes-j1-sneakers?variant=1000010641246865&hl=en&gad_source=1&gclid=Cj0KCQiA_9u5BhCUARIsABbMSPuF2d6zhylAeD_wJ4BFsKEcvI1of7I6ZsxrxPXb9V7Zd48eSFFppaAaApF3EALw_wcB",
                    Price = 99.95m
                },
                new MerchandiseItem
                {
                    Title = "Ichigo Kurosaki Figure",
                    Description = "Add Ichigo's limited-editio, 1000 year Blood War figure to your collection",
                    ImageUrl = "https://www.gkfigure.com/cdn/shop/products/1_d7bfe899-7041-42ce-846d-665161123e46.jpg?v=1678421661",
                    Hashtags = new List<string> { "Bleach", "BloodWar" },
                    BuyLink = "https://www.gkfigure.com/products/trieagles-studio-bleach-kurosaki-ichigo-licensed",
                    Price = 1080.00m
                },
                new MerchandiseItem
                {
                Title = "Haikyuu 'Karasuno' Jersey",
                Description = "Be part of the Karasuno team with this high-quality jersey",
                ImageUrl = "https://feeltheanime.com/cdn/shop/products/haykiupagina.jpg?v=1612484586&width=1206\n",
                Hashtags = new List<string> { "Haikyuu", "Karasuno" },
                BuyLink = "https://feeltheanime.com/products/haykiuu-jersey?variant=38106346225847&currency=USD&utm_medium=product_sync&utm_source=google&utm_content=sag_organic&utm_campaign=sag_organic&srsltid=AfmBOorcV3iP4kFevLl5JpCrpi45_50brbdQgm4REnF1Z26N-40_5430iKM",
                Price = 39.99m
                },
                new MerchandiseItem
                {
                Title = "Death Note Diary",
                Description = "Channel your inner Shinigami with this death note journal, perfect for all your thoughts.",
                ImageUrl = "https://i.etsystatic.com/54545721/r/il/e9c327/6360501293/il_1588xN.6360501293_jzk0.jpg",
                Hashtags = new List<string> { "DeathNote", "Journaling" },
                BuyLink = "https://etsy.com/listing/1802539733/hardcover-notebookdiary-death-note",
                Price = 18.98m
                }

            };
        }
    }

    /// <summary>
    /// Represents an individual merchandise item.
    /// </summary>
    public class MerchandiseItem
    {
        /// <summary>
        /// Gets or sets the title of the merchandise item.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the merchandise item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the URL of the merchandise image.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a list of hashtags related to the merchandise item.
        /// </summary>
        public List<string> Hashtags { get; set; }

        /// <summary>
        /// Gets or sets the URL to purchase the merchandise item.
        /// </summary>
        public string BuyLink { get; set; }

        /// <summary>
        /// Gets or sets the price of the merchandise item.
        /// </summary>
        public decimal Price { get; set; } 

    }
}