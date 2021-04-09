using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using InventoryAppTest.Models;

namespace InventoryAppTest.Controllers
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController // for unit testing purpose
    {
        public List<Product> _products;
        public ProductController(List<Product> products)
        {
            _products = products;
        }
        [HttpPost]
        [ResponseType(typeof(Product))]
        public IHttpActionResult CreateProduct(Product product)
        {
            try
            {
                int result = 0;
                string errorMessage = Product.VailidateProductFields(product);
                if (string.IsNullOrEmpty(errorMessage))
                    result = 1; // insert success
                else if (!string.IsNullOrEmpty(errorMessage))
                    return BadRequest(errorMessage);
                else
                    result = -1;
                return Ok(result.ToString());
            }
            catch
            {
                throw;
            }
        }

        [HttpPut]
        [ResponseType(typeof(Product))]
        public IHttpActionResult EditProduct(Product product)
        {
            try
            {
                int result = 0;
                string errorMessage = Product.VailidateProductFields(product);
                if (string.IsNullOrEmpty(errorMessage))
                    result = (_products.Where(p => p.PID == product.PID).ToList().Count == 0) ? -1 : 1;
                else if (!string.IsNullOrEmpty(errorMessage))
                    return BadRequest(errorMessage);
                else
                    result = -1;
                if (result == -1)
                    return BadRequest("-1");
                else
                    return Ok(result.ToString());
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int Id)
        {
            try
            {
                int result = 0;

                result = (_products.Where(p => p.PID == Id).ToList().Count == 0) ? -1 : 1;
                if (result == -1)
                    return BadRequest("-1");
                else
                    return Ok(result.ToString());
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetProduct(int Id)
        {
            try
            {
                Product product = _products.Where(p => p.PID == Id).FirstOrDefault();
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

        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            try
            {
                List<Product> products = _products;
                return Ok(products);
            }
            catch
            {
                throw;
            }
        }
    }
}