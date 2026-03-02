using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using backend.Models.Promotions;

namespace Infrastructure.Persistence.Configurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(x => x.Code)
                .IsUnique();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.DiscountValue)
                .HasColumnType("decimal(18,2)");

            builder.HasMany(x => x.UserCoupons)
                .WithOne(x => x.Coupon)
                .HasForeignKey(x => x.CouponId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}