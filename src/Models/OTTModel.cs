namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Model representing OTT platform details for the anime product.
    /// Maps data from product.json file.
    /// </summary>
    public class OTTModel
    {
        /// <summary>
        /// Gets or sets the platform name.
        /// Maps the Platform field of the OTT object in product.json to OTTModel's Platform.
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// Maps the Url field of the OTT object in product.json to OTTModel's Url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// Maps the Icon field of the OTT object in product.json to OTTModel's Icon.
        /// </summary>
        public string Icon { get; set; }
    }
}