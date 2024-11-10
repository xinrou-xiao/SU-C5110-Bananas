using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC components

namespace ContosoCrafts.WebSite.Controllers // Defining the namespace for the controller
{
    /// <summary>
    /// Controller for handling read page requests.
    /// </summary>
    public class ReadController : Controller
    {
        /// <summary>
        /// Action method to return the Read page view.
        /// </summary>
        /// <returns>An IActionResult representing the Read page view.</returns>
        public IActionResult Read()
        {
            // Returning the view associated with the Read action
            return View();
        }
    }
}