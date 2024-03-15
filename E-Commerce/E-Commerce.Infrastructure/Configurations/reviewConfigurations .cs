using E_Commerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Infrastructure.Configurations
{
    internal class ReviewConfigurations : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {

            builder.HasOne(r => r.Product)
           .WithMany(p => p.Reviews)
           .HasForeignKey(r => r.ProductId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}


