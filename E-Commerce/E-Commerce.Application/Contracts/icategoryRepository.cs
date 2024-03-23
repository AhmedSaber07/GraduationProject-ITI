using E_Commerce.Domain.DTOs.CategoryDto;
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
        public Task<IQueryable<Product>> GetAllProductbyCategoryId(Guid id);
        public Task<bool> IsCategoryExist(Guid id);
        public Task<IQueryable<Category>> GetAllChildrenById(Guid id);
        public Task<bool> CheckHasChildren(Guid id);
        public Task<bool> SeaechByName(string name);
       
    }
}
