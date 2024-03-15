using E_Commerce.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Configurations
{
    internal class productConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

          builder.HasOne(p => p.Brand)
          .WithMany(b => b.Products)
          .HasForeignKey(p => p.brandId).OnDelete(DeleteBehavior.NoAction); ;

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.categoryId).OnDelete(DeleteBehavior.NoAction); ;

        }
    }
}
