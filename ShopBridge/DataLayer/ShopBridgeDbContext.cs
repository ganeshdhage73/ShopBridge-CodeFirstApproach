using Microsoft.EntityFrameworkCore;
using ShopBridge.ViewModel;

namespace ShopBridge.DataLayer
{
    public class ShopBridgeDbContext : DbContext
    {
        public ShopBridgeDbContext(DbContextOptions<ShopBridgeDbContext> options) : base(options)
        {

        }
        public DbSet<Inventory> Inventory { get; set; } 
    }
}
