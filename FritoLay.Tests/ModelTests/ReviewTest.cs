using FritoLay.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FritoLay.Tests.ModelTests
{
    [TestClass]
    public class ReviewTest
    {
        [TestMethod]
        public void GetDescriptionTest()
        {
            //Arrange
            var review = new Review();
            review.Description = "Very Good";
            //Act
            var result = review.Description;
            //Assert
            Assert.AreEqual("Very Good", result);
        }
    }
}
