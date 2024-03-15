using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Context
{
    public class _2B_EgyptDBContext :DbContext
    {

       public DbSet<Brand> brands { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<orderItem> orderItems { get; set; }
        public DbSet<Payment> paymentItems { get; set; }
        public DbSet<Product> products { get; set; }    
        public DbSet<ProductImage> productsImage { get; set; }
        public DbSet<Review> reviews { get; set; }
         public DbSet<User> AppUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
     
       //     modelBuilder.Entity<IdentityRole>().ToTable("Roles");
       //     modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
       //     modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
       //     modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
       //     modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("UserTokens");
       //     modelBuilder.Entity<IdentityUserToken<string>>().ToTable("RoleClaims");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
        }
        public _2B_EgyptDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    }
}
