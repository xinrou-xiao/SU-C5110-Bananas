namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Model representing OTT platform details for the anime product
    /// Maps data from product.json file.
    /// </summary>
    public class OTTModel
    {
        /// <summary>
        /// Map Platform field of OTT object in product.json to OTTModel's Platform
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// Map Url field of OTT object in product.json to OTTModel's Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Map Icon field of OTT object in product.json to OTTModel's Icon
        /// </summary>
        public string Icon { get; set; }
    }
}
