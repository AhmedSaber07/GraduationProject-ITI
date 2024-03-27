using E_Commerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    internal class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.User)
               .WithMany(o => o.Payments)
               .HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
