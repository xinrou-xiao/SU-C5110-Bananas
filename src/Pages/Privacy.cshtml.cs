using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// This class handles the Privacy page logic.
    /// It manages the logging for the Privacy page.
    /// </summary>
    public class PrivacyModel : PageModel
    {
        // Logger to log information for the Privacy page
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>
        /// Constructor to initialize the PrivacyModel with a logger.
        /// </summary>
        /// <param name="logger">The logger used for logging information.</param>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            //Assign the logger
            _logger = logger;
        }

        /// <summary>
        /// Method called on GET requests to display the Privacy page.
        /// Currently, it does not perform any actions.
        /// </summary>
        public void OnGet()
        {
            // This method can be used to handle any logic needed when the page is accessed.
        }
    }
}
