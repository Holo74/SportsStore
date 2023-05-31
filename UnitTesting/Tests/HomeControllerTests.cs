using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTesting.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"}
            }).AsQueryable<Product>());
            HomeController controller = new HomeController(mock.Object);
            // Act
            IEnumerable<Product>? result =
                (controller.Index() as ViewResult)?.ViewData.Model
                     as IEnumerable<Product>;
            // Assert
            Product[] prodArray = result?.ToArray()
                ?? Array.Empty<Product>();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.Equals("P1", prodArray[0].Name);
            Assert.Equals("P2", prodArray[1].Name);
        }
    }
}
