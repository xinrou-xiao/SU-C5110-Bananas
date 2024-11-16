using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages
{
    public class MerchModel : PageModel
    {
        public List<MerchandiseItem> Merchandise { get; set; }

        public void OnGet()
        {

        }
    }
    public class MerchandiseItem
    {

    }
}