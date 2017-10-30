using FritoLay.Controllers;
using FritoLay.Models;
using FritoLay.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FritoLay.Tests
{
    [TestClass]
     public class ProductsControllerTest
    {
        // Arrange
        Mock<IProductRepository> mock = new Mock<IProductRepository>();
        EFProductRepository repo = new EFProductRepository(new TestDbContext());

        private void DbSetup()
        {
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, ProductName = "Lays", Cost = 15, CountryOfOrigin = "India", Featured = false},
                new Product {ProductId = 2, ProductName = "Dorritos", Cost = 15, CountryOfOrigin = "USA", Featured = false},
                new Product {ProductId = 3, ProductName = "Nachos", Cost = 20, CountryOfOrigin = "Europe", Featured = false}
            }.AsQueryable());
        }
        [TestMethod]
        public void Mock_GetViewResultIndex_Test() //Confirms route returns view
        {
            //Arrange
            DbSetup();

            ProductController controller = new ProductController(mock.Object);
            //Act
            var result = controller.Index();
            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }
        [TestMethod]
        public void Mock_IndexListOfItems_Test() //Confirms model as list of items
        {
            //Arrange
            DbSetup();
            ViewResult indexView = new ProductController(mock.Object).Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }
        [TestMethod]
        public void Mock_ConfirmEntry_Test() //Confirms presence of known entry
        {
            //Arrange
            DbSetup();
            ProductController controller = new ProductController(mock.Object);
            Product testProduct = new Product();
            testProduct.ProductId = 1;
            testProduct.ProductName = "Lays";
            testProduct.Cost = 15;
            testProduct.CountryOfOrigin = "India";
            testProduct.Featured = false;

            // Act
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            //Assert
            CollectionAssertContains(collection, testProduct);
        }
        [TestMethod]
        public void DB_CreateNewEntry_Test()
        {
            // Arrange
            ProductController controller = new ProductController(repo);
            Product testProduct = new Product();
            testProduct.ProductName = "Test Product";
            testProduct.Cost = 15;
            testProduct.CountryOfOrigin = "India";
            testProduct.Featured = false;
            // Act
            controller.Create(testProduct);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Product>;

            // Assert
            CollectionAssert.Contains(collection, testProduct);
        }

        private void CollectionAssertContains(List<Product> collection, Product product)
        {
            foreach(var p in collection)
            {
                if (p.Equals(product))
                    return;
            }

            throw new Exception($"Collection does not contain element");
        }
    }
}
