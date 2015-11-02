using SportsStore.LCW.Domain.Entities;

namespace SportsStore.LCW.Domain.Abstract {
    public interface IOrderProcessor {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}