using System.ComponentModel.DataAnnotations;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Model representing OTT platform details for the anime product.
    /// Maps data from product.json file
    /// </summary>
    public class OTTModel
    {
        /// <summary>
        /// Gets or sets the platform name.
        /// Maps the Platform field of the OTT object in product.json to OTTModel's Platform.
        /// </summary>
        [Required(ErrorMessage = "Platform name is required.")]
        [StringLength(100, ErrorMessage = "Platform name cannot exceed 100 characters.")]
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// Maps the Url field of the OTT object in product.json to OTTModel's Url.
        /// </summary>
        [Required(ErrorMessage = "URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// Maps the Icon field of the OTT object in product.json to OTTModel's Icon.
        /// </summary>
        [Required(ErrorMessage = "Icon is required.")]
        [StringLength(200, ErrorMessage = "Icon path cannot exceed 200 characters.")]
        public string Icon { get; set; }
    }
}
