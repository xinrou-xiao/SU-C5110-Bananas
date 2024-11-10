using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using ContosoCrafts.WebSite.Models;

using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
   public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        public IEnumerable<ProductModel> GetAllData()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Call by onGet function in read page, return product data who has the 
        /// same id as given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductModel GetOneDataById(string id)
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                var products = JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                // Get the data that has the given id
                var product = products.FirstOrDefault(prod => prod.Id == id);
                return product;
            }
        }

        /// <summary>
        /// Add Rating
        /// 
        /// Take in the product ID and the rating
        /// If the rating does not exist, add it
        /// Save the update
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="rating"></param>
        public bool AddRating(string productId, int rating)
        {
            // If the ProductID is invalid, return
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }

            var products = GetAllData();

            // Look up the product, if it does not exist, return
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));
            if (data == null)
            {
                return false;
            }

            // Check Rating for boundaries, do not allow ratings below 0
            if (rating < 0)
            {
                return false;
            }

            // Check Rating for boundaries, do not allow ratings above 5
            if (rating > 5)
            {
                return false;
            }

            // Check to see if the rating exist, if there are none, then create the array
            if(data.Ratings == null)
            {
                data.Ratings = new List<int>();
            }

            // Add the Rating to the Array
            data.Ratings.Add(rating);

            // Save the data back to the data store
            SaveData(products);

            return true;
        }

        /// <summary>
        /// Find the data record
        /// Update the fields
        /// Save to the data store
        /// </summary>
        /// <param name="data"></param>
        public ProductModel UpdateData(ProductModel updatedProduct)
        {
            var products = GetAllData().ToList();
            var existingProduct = products.FirstOrDefault(x => x.Id.Equals(updatedProduct.Id));

            if (existingProduct is not null)
            {
                existingProduct.Title = updatedProduct.Title;
                if (existingProduct.Description != null)
                {
                    updatedProduct.Description = updatedProduct.Description.Trim();
                }
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Url = updatedProduct.Url;
                existingProduct.Image = updatedProduct.Image;
                existingProduct.CommentList = updatedProduct.CommentList;
                existingProduct.Release = updatedProduct.Release;
                existingProduct.Trailer = updatedProduct.Trailer;
                existingProduct.Season = updatedProduct.Season;
                existingProduct.Genre = updatedProduct.Genre;
                existingProduct.OTT = updatedProduct.OTT;

                SaveData(products);
            }

            return existingProduct;
        }

        /// <summary>
        /// Save All products data to storage
        /// </summary>
        private void SaveData(IEnumerable<ProductModel> products)
        {

            using (var outputStream = File.Create(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }

        /// <summary>
        /// Added given product into json file
        /// </summary>
        /// <returns></returns>
        public ProductModel CreateData(ProductModel product)
        {
            // just in case any incorrect called
            if (product == null)
            {
                return null;
            }
            // Get the current set, and append the new record to it because IEnumerable does not have Add
            var dataSet = GetAllData();
            dataSet = dataSet.Append(product);

            SaveData(dataSet);

            return product;
        }

        /// <summary>
        /// Remove the item from the system
        /// </summary>
        /// <returns></returns>
        public ProductModel DeleteData(string id)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetAllData();
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));
            if (data != null)
            {
                var newDataSet = dataSet.Where(m => m.Id != id);
                SaveData(newDataSet);
            }
            return data;
        }


        //internal void UpdateProduct(ProductModel product)
        //{
        //    throw new NotImplementedException();
        //}
    }
}