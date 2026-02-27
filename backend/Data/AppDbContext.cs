using Microsoft.EntityFrameworkCore;
using backend.Models.Cart;
using backend.Models.Masters;
using backend.Models.Orders;
using backend.Models.Products;
using backend.Models.Users;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<UserAddress> UserAddresses => Set<UserAddress>();

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<MasterColor> MasterColors => Set<MasterColor>();
    public DbSet<MasterSize> MasterSizes => Set<MasterSize>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>().ToTable("Users");
        b.Entity<Product>().ToTable("Products");
        b.Entity<Order>().ToTable("Orders");
    }
}
