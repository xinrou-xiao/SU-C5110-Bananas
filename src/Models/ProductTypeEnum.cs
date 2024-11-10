using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoCrafts.WebSite.Models
{
    // <summary>
    // Types of products available.
    // </summary>
    public enum ProductTypeEnum
    {
        // <summary>
        // Default undefined product type.
        // </summary>
        Undefined = 0,

        // <summary>
        // Represents amateur, handmade items.
        // </summary>
        Amature = 1,

        // <summary>
        // Represents antique items.
        // </summary>
        Antique = 5,

        // <summary>
        // Represents collectible items.
        // </summary>
        Collectable = 130,

        // <summary>
        // Represents commercial goods.
        // </summary>
        Commercial = 55,
    }

    // <summary>
    // Extension methods for the ProductTypeEnum to provide additional functionality.
    // </summary>
    public static class ProductTypeEnumExtensions
    {
        /// <summary>
        /// Gets the display name for each ProductTypeEnum value.
        /// </summary>
        /// <param name="data">The product type enum value.</param>
        /// <returns>String representing the display name of the product type.</returns>
        public static string DisplayName(this ProductTypeEnum data)
        {
            return data switch
            {
                ProductTypeEnum.Amature => "Hand Made Items",
                ProductTypeEnum.Antique => "Antiques",
                ProductTypeEnum.Collectable => "Collectables",
                ProductTypeEnum.Commercial => "Commercial goods",

                // Default case for unknown or undefined product types
                _ => "",
            };
        }
    }
}