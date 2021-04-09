using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using InventoryAppTest.Controllers;
using InventoryAppTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InventoryAppTest.Tests.Controllers
{
    [TestClass]
    public class ProductControllerUnitTest
    {
        [TestMethod]
        public void CreateProductSuccess()
        {
            // Arrange
            ProductController productController = new ProductController(GetProductTestItems());
            Product product = new Product
            {
                PName = "Headphone",
                PDescription = "Sony HeadPhone",
                PQuantity = 10,
                PPrice = 10000.50M
            };

            // Act
            IHttpActionResult httpActionResult = productController.CreateProduct(product);
            var createdResult = httpActionResult as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("1", createdResult.Content);
        }

        [TestMethod]
        public void CreateProductFailed()
        {
            // Arrange
            ProductController productController = new ProductController(GetProductTestItems());
            Product product = new Product
            {
                PName = "",
                PDescription = "Sony HeadPhone",
                PQuantity = 10,
                PPrice = 10000.50M
            };

            // Act
            IHttpActionResult httpActionResult = productController.CreateProduct(product);
            var createdResult = httpActionResult as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("Please enter Product Name.", createdResult.Message);
        }

        [TestMethod]
        public void EditProductSuccess()
        {
            // Arrange
            ProductController productController = new ProductController(GetProductTestItems());
            Product product = new Product
            {
                PID = 1,
                PName = "Test123",
                PDescription = "Test Desc1",
                PQuantity = 10,
                PPrice = 10000.50M
            };

            // Act
            IHttpActionResult httpActionResult = productController.EditProduct(product);
            var createdResult = httpActionResult as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("1", createdResult.Content);
        }

        [TestMethod]
        public void EditProductFailed()
        {
            // Arrange
            ProductController productController = new ProductController(GetProductTestItems());
            Product product = new Product
            {
                PID = 15,
                PName = "test15",
                PDescription = "Sony HeadPhone g2",
                PPrice = 12034.34M,
                PQuantity = 10
            };

            // Act
            IHttpActionResult httpActionResult = productController.EditProduct(product);
            var createdResult = httpActionResult as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("-1", createdResult.Message);
        }

        [TestMethod]
        public void DeleteProductSuccess()
        {
            // Arrange
            ProductController productController = new ProductController(GetProductTestItems());

            // Act
            IHttpActionResult httpActionResult = productController.DeleteProduct(3);
            var createdResult = httpActionResult as OkNegotiatedContentResult<string>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("1", createdResult.Content);
        }

        [TestMethod]
        public void DeleteProductFailed()
        {
            // Arrange
            ProductController productController = new ProductController(GetProductTestItems());

            // Act
            IHttpActionResult httpActionResult = productController.DeleteProduct(15);
            var createdResult = httpActionResult as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("-1", createdResult.Message);
        }

        [TestMethod]
        public void GetProductByIdSuccess()
        {
            // Set up Prerequisites 
            ProductController productController = new ProductController(GetProductTestItems());
            // Act on Test
            IHttpActionResult httpActionResult = productController.GetProduct(2);
            var contentResult = httpActionResult as OkNegotiatedContentResult<Product>;
            // Assert the result
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.PID);
        }

        [TestMethod]
        public void GetProductByIdNotFound()
        {
            // Set up Prerequisites 
            ProductController productController = new ProductController(GetProductTestItems());
            // Act on Test
            IHttpActionResult httpActionResult = productController.GetProduct(10);
            // Assert  
            Assert.IsInstanceOfType(httpActionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllProductsSuccess()
        {
            // Set up Prerequisites 
            ProductController productController = new ProductController(GetProductTestItems());
            // Act on Test
            IHttpActionResult httpActionResult = productController.GetProducts();
            var contentResult = httpActionResult as OkNegotiatedContentResult<List<Product>>;
            // Assert the result
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(GetProductTestItems().Count, contentResult.Content.Count);
        }


        private List<Product> GetProductTestItems()
        {
            var productTestItems = new List<Product>();
            productTestItems.Add(new Product { PID = 1, PName = "Test1", PDescription = "Test Desc1", PQuantity = 5, PPrice = 1 });
            productTestItems.Add(new Product { PID = 2, PName = "Test2", PDescription = "Test Desc2", PQuantity = 14, PPrice = 3.75M });
            productTestItems.Add(new Product { PID = 3, PName = "Test3", PDescription = "Test Desc3", PQuantity = 35, PPrice = 16.99M });
            productTestItems.Add(new Product { PID = 4, PName = "Test4", PDescription = "Test Desc4", PQuantity = 55, PPrice = 11.00M });
            return productTestItems;
        }
    }
}
