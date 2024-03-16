using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface icategoryRepository : ibaseRepository<Category, Guid>
    {
        Task<IQueryable<Product>> GetAllProductbyCategoryId(Guid id);
        Task<bool> isCategoryExist(Guid id);
    }
}
