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
    public class _2B_EgyptDBContext :IdentityDbContext<MyUser,IdentityRole<Guid>,Guid>   //                    bug was here
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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder); //                                bug was here this must be included on migration

            modelBuilder.Entity<MyUser>().ToTable("Users").HasKey(u => u.Id);
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles").HasKey(r => r.Id);
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");

            // Define primary key for IdentityUserLogin<Guid> entity
            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
        }
        public _2B_EgyptDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    }
}
