using Microsoft.AspNetCore.Mvc;

namespace ContosoCrafts.WebSite.Controllers
{
    public class ReadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
