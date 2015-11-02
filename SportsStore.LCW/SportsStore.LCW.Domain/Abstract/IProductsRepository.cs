using System.Linq;
using SportsStore.LCW.Domain.Entities;

namespace SportsStore.LCW.Domain.Abstract {
    public interface IProductsRepository {
        IQueryable<Product> Products { get; }
    }
}