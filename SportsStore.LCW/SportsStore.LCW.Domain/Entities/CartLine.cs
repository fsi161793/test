using System.Collections;

namespace SportsStore.LCW.Domain.Entities {
    
    /// <summary>
    /// 购物车实体
    /// </summary>
    public class CartLine {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}