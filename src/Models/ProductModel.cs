using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Represents a product including its details, ratings, and user comments.
    /// </summary>
    public class ProductModel
    {
        // Map Id field in product.json to ProductModel's Id
        public string Id { get; set; }

        // Map Maker field in product.json to ProductModel's Maker
        public string Maker { get; set; }

        // Map img field in product.json to ProductModel's Image
        [JsonPropertyName("img")]
        public string Image { get; set; }

        // Map Url field in product.json to ProductModel's Url
        public string Url { get; set; }

        // Map Title field in product.json to ProductModel's Title with validation
        [StringLength(maximumLength: 200, MinimumLength = 1, ErrorMessage = "The Title should have a length of more than {2} and less than {1}")]
        public string Title { get; set; }

        // Map Description field in product.json to ProductModel's Description
        public string Description { get; set; }

        // Map Ratings field in product.json to ProductModel's Ratings array
        public int Ratings { get; set; }

        // Map Genre field in product.json to ProductModel's Genre
        public string Genre { get; set; }


        /// <summary>
        /// Map Release field in product.json to ProductModel's Release
        /// </summary>
        public string Release { get; set; }

        /// <summary>
        /// Map Trailor field in product.json to ProductModel's Trailor
        /// </summary>
        public string Trailer { get; set; }

        /// <summary>
        /// Map OTT field in product.json to ProductModel's OTT list
        /// </summary>
        public List<OTTModel> OTT { get; set; } = new List<OTTModel> ();

        /// <summary>
        /// Map Season field in product.json to ProductModel's Season
        /// </summary>
        public int Season { get; set; }

        // Store the Comments entered by the users on this product
        public List<CommentModel> CommentList { get; set; } = new List<CommentModel>();

        /// <summary>
        /// Serialize ProductModel instance to JSON format for display
        /// </summary>
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);


    }
}