using SportsStore.LCW.Domain.Entities;
using System.Data.Entity;
namespace SportsStore.LCW.Domain.Concrete {
    public class EFDBContext : DbContext {
        public DbSet<Product> Products { get; set; }
    }
}