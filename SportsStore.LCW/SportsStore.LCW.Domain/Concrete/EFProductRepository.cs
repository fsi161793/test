using System.Linq;
using SportsStore.LCW.Domain.Abstract;
using SportsStore.LCW.Domain.Entities;

namespace SportsStore.LCW.Domain.Concrete {
    public class EFProductRepository : IProductsRepository {
        private EFDBContext context = new EFDBContext();

        public IQueryable<Product> Products {
            get { return context.Products; }
        }
    }
}