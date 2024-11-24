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
        [StringLength(100, ErrorMessage = "Maker cannot exceed 100 characters.")]
        public string Maker { get; set; }

        // Map img field in product.json to ProductModel's Image
        [JsonPropertyName("img")]
        [Url(ErrorMessage = "Invalid URL format for Image.")]
        public string Image { get; set; }

        // Map Url field in product.json to ProductModel's Url
        [Url(ErrorMessage = "Invalid URL format for Url.")]
        public string Url { get; set; }

        // Map Title field in product.json to ProductModel's Title with validation
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "The Title should have a length between 1 and 200 characters.")]
        public string Title { get; set; }

        // Map Description field in product.json to ProductModel's Description
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        // Map Ratings field in product.json to ProductModel's Ratings array
        public List<int> Ratings { get; set; }

        // Map Genre field in product.json to ProductModel's Genre
        public string[] Genre { get; set; }

        // Map Release field in product.json to ProductModel's Release
        [StringLength(4, ErrorMessage = "Release date should be in a valid format.")]
        public string Release { get; set; }

        // Map Trailer field in product.json to ProductModel's Trailer
        [Url(ErrorMessage = "Invalid URL format for Trailer.")]
        public string Trailer { get; set; }

        // Map OTT field in product.json to ProductModel's OTT list
        
        public List<OTTModel> OTT { get; set; } = new List<OTTModel>();

        // Map Season field in product.json to ProductModel's Season
        [Range(1, int.MaxValue, ErrorMessage = "Season must be a positive number.")]
        public int Season { get; set; }

        // Store the Comments entered by the users on this product
        public List<CommentModel> CommentList { get; set; } = new List<CommentModel>();

        /// <summary>
        /// Serialize ProductModel instance to JSON format for display
        /// </summary>
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);
    }
}