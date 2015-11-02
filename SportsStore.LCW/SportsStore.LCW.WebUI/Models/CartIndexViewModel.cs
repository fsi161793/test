using SportsStore.LCW.Domain.Entities;

namespace SportsStore.LCW.WebUI.Models {
    public class CartIndexViewModel {
        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; }
    }
}