
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
    public class CartTest {
        [TestMethod]
        public void Can_Add_New_Lines() {
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100 };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 100 };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 100 };
            Product p4 = new Product { ProductID = 4, Name = "P4", Price = 100 };

            Cart cart = new Cart();

            cart.AddItem(p1, 10);
            cart.AddItem(p2, 2);
            var result = cart.Lines.ToArray();
            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual(result[0].Product, p1);
            Assert.AreEqual(result[1].Product, p2);
        }

        [TestMethod]
        public void Can_Remove_Line() {
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100 };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 100 };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 100 };
            Product p4 = new Product { ProductID = 4, Name = "P4", Price = 100 };

           //创建一个购物车          
            Cart cart = new Cart();

            cart.AddItem(p1, 10);
            cart.AddItem(p2, 2);
            cart.AddItem(p1, 3);

            cart.RemoveItem(p2);

            Assert.AreEqual(cart.Lines.Count(c => c.Product == p2), 0);
            Assert.AreEqual(cart.Lines.First(p => p.Product==p1).Quantity, 13);
        }

        [TestMethod]
        public void Calculate_Cart_Total() {
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100 };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 100 };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 100 };
            Product p4 = new Product { ProductID = 4, Name = "P4", Price = 100 };

            //创建一个购物车          
            Cart cart = new Cart();

            cart.AddItem(p1, 10);
            cart.AddItem(p2, 2);

            Assert.AreEqual(cart.ComputeTotalValue(), 1200);
        }

        [TestMethod]
        public void Can_Clear_Contents() {
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100 };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 100 };
            Product p3 = new Product { ProductID = 3, Name = "P3", Price = 100 };
            Product p4 = new Product { ProductID = 4, Name = "P4", Price = 100 };

            //创建一个购物车          
            Cart cart = new Cart();

            cart.AddItem(p1, 10);
            cart.AddItem(p2, 2);

            cart.ClearItem();

            Assert.AreEqual(cart.Lines.Count(), 0);
        }
    }
}