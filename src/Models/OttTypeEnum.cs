namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Enum representing different OTT (Over The Top) platforms
    /// </summary>
    public enum OttTypeEnum
    {
        // Represents an undefined or unknown platform
        Undefined = 0,

        // Represents Netflix platform
        Netflix = 3,

        // Represents Amazon Prime Video platform
        Prime = 7,

        // Represents Crunchyroll platform
        Crunchyroll = 15,

        // Represents Tubi TV platform
        tubitv = 21,

        // Represents Hulu platform
        Hulu = 29
    }

    /// <summary>
    /// Static class that provides extension methods for OttTypeEnum
    /// </summary>
    public static class OttTypeEnumExtensions
    {
        /// <summary>
        /// Extension method to get the URL associated with a specific OTT platform
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetUrl(this OttTypeEnum data)
        {
            // Use switch expression to return the appropriate URL for each platform
            return data switch
            {
                OttTypeEnum.Netflix => "https://www.netflix.com/", // URL for Netflix
                OttTypeEnum.Prime => "https://www.amazon.com/gp/video/storefront", // URL for Amazon Prime Video
                OttTypeEnum.Crunchyroll => "https://www.crunchyroll.com/", // URL for Crunchyroll
                OttTypeEnum.tubitv => "https://tubitv.com/", // URL for Tubi TV
                OttTypeEnum.Hulu => "https://www.hulu.com/welcome?orig_referrer=https%3A%2F%2Fwww.google.com%2F", // URL for Hulu

                // Default case for unknown or undefined product types
                _ => "",
            };
        }

        /// <summary>
        /// Extension method to get the icon URL associated with a specific OTT platform
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetIcon(this OttTypeEnum data)
        {
            // Use switch expression to return the appropriate icon URL for each platform
            return data switch
            {
                OttTypeEnum.Netflix => "https://images.ctfassets.net/y2ske730sjqp/1aONibCke6niZhgPxuiilC/2c401b05a07288746ddf3bd3943fbc76/BrandAssets_Logos_01-Wordmark.jpg?w=940", // Icon for Netflix
                OttTypeEnum.Prime => "https://assets.aboutamazon.com/dims4/default/59e4166/2147483647/strip/true/crop/4454x2634+0+0/resize/365x216!/format/webp/quality/90/?url=https%3A%2F%2Famazon-blogs-brightspot.s3.amazonaws.com%2F4b%2F7f%2F4a4a80724a5e9a6b4a1931e8bf99%2Fprime-logo-rgb-prime-blue-master.png", // Icon for Amazon Prime Video
                OttTypeEnum.Crunchyroll => "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d6/Crunchyroll.svg/768px-Crunchyroll.svg.png", // Icon for Crunchyroll
                OttTypeEnum.tubitv => "https://upload.wikimedia.org/wikipedia/commons/c/c5/Tubi_logo_2024_purple.svg", // Icon for Tubi TV
                OttTypeEnum.Hulu => "https://greenhouse.hulu.com/app/uploads/sites/12/2023/10/logo-gradient-3up.svg", // Icon for Hulu

                // Default case for unknown or undefined product types
                _ => "",
            };
        }
    }
}