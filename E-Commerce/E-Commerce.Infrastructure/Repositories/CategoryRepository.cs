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
    public class CategoryRepository : baseRepository<Category, Guid>,icategoryRepository 
    {
        private readonly _2B_EgyptDBContext _context;
        public CategoryRepository(_2B_EgyptDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryable<Product>> GetAllProductbyCategoryId(Guid id)
        {
            var productsQuery = _context.products
                                    .Where(p => p.categoryId == id);

            return await Task.FromResult(productsQuery);
        }
        public async Task<bool> IsCategoryExist(Guid id)
        {
            return await _context.categories.AnyAsync(c => c.Id == id);
        }

        public async Task<IQueryable<Category>> GetAllChildrenById(Guid id)
        {
            var Query = _context.categories
                                     .Where(c => c.ParentCategoryId == id);
            return await Task.FromResult(Query);
        }

       async public Task<bool> CheckHasChildren(Guid id)
        {
            return await _context.categories.AnyAsync(c => c.ParentCategoryId == id);
        }

        public async Task<bool> SeaechByName(string name)
        {
            return await _context.categories.AnyAsync(c => c.nameEn == name);
        }
    }
}
