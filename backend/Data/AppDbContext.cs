using Microsoft.EntityFrameworkCore;
using backend.Models.Users;
using backend.Models.Promotions;
using backend.Models.Cart;
using backend.Models.Orders;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Identity
    public DbSet<User> Users { get; set; }

    // Promotions
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<UserCoupon> UserCoupons { get; set; }

    // Shopping
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply all configuration classes automatically
        builder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly
        );
    }
}