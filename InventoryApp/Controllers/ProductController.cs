using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using InventoryApp.DataService;
using InventoryApp.Models;

namespace InventoryApp.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        // Create New Product
        /// <summary>
        /// Create new product and returns the result.
        /// </summary>
        /// <returns>
        /// The operation result.
        /// </returns>
        [HttpPost]
        [ResponseType(typeof(Product))]
        public string CreateProduct(Product product)
        {
            try
            {
                DataOperationService dataOperationService = new DataOperationService();
                int result = 0;
                string errorMessage = Product.VailidateProductFields(product);
                if (string.IsNullOrEmpty(errorMessage))
                    result = dataOperationService.InsertProductAsync(product).GetAwaiter().GetResult();
                else if (!string.IsNullOrEmpty(errorMessage))
                    return errorMessage;
                else
                    result = -1;
                return result.ToString();
            }
            catch
            {
                throw;
            }
        }

        // Edit Product
        /// <summary>
        /// Edit existing product and returns the result.
        /// </summary>
        /// <returns>
        /// The operation result.
        /// </returns>
        [HttpPut]
        [ResponseType(typeof(Product))]
        public string EditProduct(Product product)
        {
            try
            {
                DataOperationService dataOperationService = new DataOperationService();
                int result = 0;
                string errorMessage = Product.VailidateProductFields(product);
                if (string.IsNullOrEmpty(errorMessage))
                    result = dataOperationService.UpdateProductAsync(product).GetAwaiter().GetResult();
                else if (!string.IsNullOrEmpty(errorMessage))
                    return errorMessage;
                else
                    result = -1;
                return result.ToString();
            }
            catch
            {
                throw;
            }
        }

        // Delete Product
        /// <summary>
        /// Delete and returns the result.
        /// </summary>
        /// <returns>
        /// The operation result.
        /// </returns>
        [HttpDelete]
        public string DeleteProduct(int Id)
        {
            try
            {
                DataOperationService dataOperationService = new DataOperationService();
                int result = 0;

                result = dataOperationService.DeleteProductAsync(Id).GetAwaiter().GetResult();
                return result.ToString();
            }
            catch
            {
                throw;
            }
        }

        // Get Product details
        /// <summary>
        /// Finds the product and returns the result.
        /// </summary>
        /// <returns>
        /// The product result.
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetProduct(int Id)
        {
            try
            {
                DataOperationService dataOperationService = new DataOperationService();
                Product product = dataOperationService.GetProductAsync(Id).GetAwaiter().GetResult();
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch
            {
                throw;
            }
        }

        // Get all Product details
        /// <summary>
        /// Returns all the result products as per page size mentioned.
        /// </summary>
        /// <returns>
        /// The all products result.
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetProducts(int pageSize = 10)
        {
            try
            {
                int pageNo = 0;
                int IsPaging = 0;
                DataOperationService dataOperationService = new DataOperationService();
                List<Product> products = dataOperationService.GetProductsAsync(pageNo, pageSize, IsPaging).GetAwaiter().GetResult();
                return Ok(products);
            }
            catch
            {
                throw;
            }
        }
    }
}