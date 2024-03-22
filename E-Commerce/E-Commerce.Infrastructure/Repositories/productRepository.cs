using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    public class productRepository : baseRepository<Product,Guid> ,iproductRepository
    {
        public productRepository(_2B_EgyptDBContext context) :base(context)
        {   
            
        }
    }
}
