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
    public class orderItemConfigurations : IEntityTypeConfiguration<orderItem>
    {
        public void Configure(EntityTypeBuilder<orderItem> builder)
        {
           

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(oi => oi.Product)
          .WithMany(p => p.OrderItems)
          .HasForeignKey(oi => oi.ProductId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
