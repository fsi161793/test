using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.LCW.Domain.Abstract;
using SportsStore.LCW.Domain.Entities;
using SportsStore.LCW.WebUI.Controllers;
using SportsStore.LCW.WebUI.HtmlHelpers;
using SportsStore.LCW.WebUI.Models;


namespace SportsStore.LCW.UnitTests {
    [TestClass]
    public class UnitTest {
        
        public void TestMethod1() {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(p => p.Products).Returns(new Product[] {
                new Product{ProductID=1,Name="p1"},
                new Product{ProductID=2,Name="p2"},
                new Product{ProductID=3,Name="p3"},
                new Product{ProductID=4,Name="p4"},
                new Product{ProductID=5,Name="p5"},
                new Product{ProductID=6,Name="p6"},
                new Product{ProductID=7,Name="p7"},
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);

            controller.pageSize = 3;

            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(null,2).Model;
            Product[] prodarray = result.ToArray();

            Assert.IsTrue(prodarray.Length == 3);

            Assert.AreEqual(prodarray[0].Name, "p4");
            Assert.AreEqual(prodarray[2].Name, "p6");
        }

        [TestMethod]
        public void Can_Generate_Page_Links() {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            // Assert
            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a>"
            + @"<a class=""selected"" href=""Page2"">2</a>"
            + @"<a href=""Page3"">3</a>");
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model() {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(p => p.Products).Returns(
                new Product[] {
                new Product{ProductID=1,Name="p1"},
                new Product{ProductID=2,Name="p2"},
                new Product{ProductID=3,Name="p3"},
                new Product{ProductID=4,Name="p4"},
                new Product{ProductID=5,Name="p5"},
                new Product{ProductID=6,Name="p6"},
                new Product{ProductID=7,Name="p7"},
                    }.AsQueryable()
                );

            ProductController controller = new ProductController(mock.Object);
            ProductsListViewModel result = (ProductsListViewModel)controller.List(null,2).Model;

            PagingInfo pageInfo = result.PagingInfo;

            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, controller.pageSize);
            Assert.AreEqual(pageInfo.TotalItems, 7);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products() {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(p => p.Products).Returns(
                new Product[] {
                    new Product{ProductID=1,Name="cat1",Category="EDG"},
                    new Product{ProductID=1,Name="cat1",Category="EDG"},
                    new Product{ProductID=1,Name="cat2",Category="OMG"},
                    new Product{ProductID=1,Name="cat2",Category="OMG"},
                    new Product{ProductID=1,Name="cat3",Category="WE"},
                    new Product{ProductID=1,Name="cat3",Category="WE"},
                    new Product{ProductID=1,Name="cat4",Category="IG"},
                    new Product{ProductID=1,Name="cat4",Category="IG"},
                    new Product{ProductID=1,Name="cat5",Category="LGD"},
                    new Product{ProductID=1,Name="cat5",Category="LGD"},        
                }.AsQueryable()
                );

            ProductController controller = new ProductController(mock.Object);
            var result = (ProductsListViewModel) controller.List(null, 1).Model;
            var array = result.Products.ToArray();


            Assert.IsTrue(array.Length == 4);
            Assert.AreEqual(array[0].Name, "cat1");
            Assert.AreEqual(array[1].Name, "cat1");
        }

        [TestMethod]
        public void Can_Create_Categories() {
            var mock = new UnitTest().CareaData();
            var controller = new NavController(mock.Object);
            var result = (IEnumerable<string>)controller.Menu(null).Model;
            var array = result.ToArray();
            Assert.IsTrue(array.Length == 5);

        }

        [TestMethod]
        public void Indicates_Selected_Category() {
            var mock = new UnitTest().CareaData();
            var controller = new NavController(mock.Object);
            var result = (string)controller.Menu("OMG").ViewBag.SelectedCategory;


            Assert.AreEqual("OMG", result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Product_Count() {
            // Arrange
            // - create the mock repository
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
                }.AsQueryable());
            // Arrange - create a controller and make the page size 3 items
            ProductController target = new ProductController(mock.Object);
            target.pageSize = 3;
            // Action - test the product counts for different categories
            int res1 = ((ProductsListViewModel)target
            .List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)target
            .List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)target
            .List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductsListViewModel)target
            .List(null).Model).PagingInfo.TotalItems;
            // Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

        public  Mock<IProductsRepository> CareaData() {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(p => p.Products).Returns(
                new Product[] {
                    new Product{ProductID=1,Name="cat1",Category="EDG"},
                    new Product{ProductID=1,Name="cat1",Category="EDG"},
                    new Product{ProductID=1,Name="cat2",Category="OMG"},
                    new Product{ProductID=1,Name="cat2",Category="OMG"},
                    new Product{ProductID=1,Name="cat3",Category="WE"},
                    new Product{ProductID=1,Name="cat3",Category="WE"},
                    new Product{ProductID=1,Name="cat4",Category="IG"},
                    new Product{ProductID=1,Name="cat4",Category="IG"},
                    new Product{ProductID=1,Name="cat5",Category="LGD"},
                    new Product{ProductID=1,Name="cat5",Category="LGD"},        
                }.AsQueryable()
                );
            return mock;
        }
    }
}
