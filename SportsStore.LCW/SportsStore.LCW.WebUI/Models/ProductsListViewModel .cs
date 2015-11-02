using System.Collections.Generic;
using SportsStore.LCW.Domain.Entities;

namespace SportsStore.LCW.WebUI.Models {
    public class ProductsListViewModel {
        public IEnumerable<Product> Products;
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}