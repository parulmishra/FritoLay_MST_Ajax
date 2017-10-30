using FritoLay.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FritoLay.Tests.ModelTests
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void GetProductNameTest()
        {
            //Arrange
            var product = new Product();
            product.ProductName = "Lays Chips";
            //Act
            var result = product.ProductName;
            //Assert
            Assert.AreEqual("Lays Chips", result);
        }
    }
}
