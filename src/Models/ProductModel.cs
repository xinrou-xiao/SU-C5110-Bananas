using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// Represents a product including its details, ratings, and user comments.
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Map Id field in product.json to ProductModel's Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Map Maker field in product.json to ProductModel's Maker
        /// </summary>
        public string Maker { get; set; }

        /// <summary>
        /// Map img field in product.json to ProductModel's Image
        /// </summary>
        [JsonPropertyName("img")]
        public string Image { get; set; }

        /// <summary>
        /// Map Url field in product.json to ProductModel's Url
        /// </summary>
        public string Url { get; set; }

        // <summary>
        /// Map Title field in product.json to ProductModel's Title with validation
        /// </summary>
        [StringLength (maximumLength: 200, MinimumLength = 1, ErrorMessage = "The Title should have a length of more than {2} and less than {1}")]
        public string Title { get; set; }

        /// <summary>
        /// Map Description field in product.json to ProductModel's Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Map Ratings field in product.json to ProductModel's Ratings array
        /// </summary>
        public int[] Ratings { get; set; }

        /// <summary>
        /// Map Genre field in product.json to ProductModel's Genre
        /// </summary>
        public string[] Genre { get; set; }

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