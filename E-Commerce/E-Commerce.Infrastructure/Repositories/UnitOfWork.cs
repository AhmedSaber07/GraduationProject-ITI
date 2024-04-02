using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        public ibrandRepository brand { get; }
        public  icategoryRepository category { get; }
        public iproductRepository product { get; }
        public ishoppingCartRepository shoppingCart { get; }
        public iorderRepository order { get; }
        public iorderItemRepository orderItem { get; }
        public ipaymentRepository payment { get; }
        public ireviewRepository review { get; }
        private readonly _2B_EgyptDBContext _2B_EgyptDBContext;
        public UnitOfWork(_2B_EgyptDBContext DBContext)
        {
            _2B_EgyptDBContext = DBContext;
            brand = new BrandRepository(_2B_EgyptDBContext);
            category = new CategoryRepository(_2B_EgyptDBContext);
            product = new productRepository(_2B_EgyptDBContext);
            shoppingCart = new ShoppingCartRepository(_2B_EgyptDBContext);
            order = new OrderRepository(_2B_EgyptDBContext);
            orderItem = new OrderItemRepository(_2B_EgyptDBContext);
            payment=new PaymentRepository(_2B_EgyptDBContext);
            review = new ReviewRepository(_2B_EgyptDBContext);
        }
        public async Task<bool> Complete()
        {
            var Result = await _2B_EgyptDBContext.SaveChangesAsync();
            return Result > 0;
        }

        public void Dispose()
        {
            _2B_EgyptDBContext.Dispose();
        }
    }
}
