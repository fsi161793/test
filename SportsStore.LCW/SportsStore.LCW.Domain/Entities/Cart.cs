using System.Collections.Generic;
using System.Linq;

namespace SportsStore.LCW.Domain.Entities {
    /// <summary>
    /// 购物车
    /// </summary>
    public class Cart {


        private List<CartLine> lineCollection = new List<CartLine>();


        /// <summary>
        /// 返回一个购物链
        /// </summary>
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }


        public void AddItem(Product product, int quantity) {
            CartLine line = lineCollection
                .FirstOrDefault(p => p.Product.ProductID == product.ProductID);

            if (line == null) {
                lineCollection.Add(new CartLine {
                    Product = product,
                    Quantity = quantity
                });
            }
            else {
                line.Quantity += quantity;
            }
        }

        public int RemoveItem(Product product) {
            return lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public void ClearItem() {
            lineCollection.Clear();
        }

        public decimal ComputeTotalValue() {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }




    }
}