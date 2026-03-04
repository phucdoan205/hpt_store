using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using backend.Models.Promotions;

namespace Infrastructure.Persistence.Configurations
{
    public class UserCouponConfiguration : IEntityTypeConfiguration<UserCoupon>
    {
        public void Configure(EntityTypeBuilder<UserCoupon> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.UserId, x.CouponId })
                .IsUnique(); // 1 user không được nhận 1 coupon 2 lần

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserCoupons)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Coupon)
                .WithMany(x => x.UserCoupons)
                .HasForeignKey(x => x.CouponId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}