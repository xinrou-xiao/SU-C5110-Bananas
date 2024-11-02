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

        public void OnGet()
        {
        }
    }
}
