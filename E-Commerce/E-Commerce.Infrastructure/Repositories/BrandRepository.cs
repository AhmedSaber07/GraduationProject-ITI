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
    public class BrandRepository : baseRepository<Brand, Guid>, ibrandRepository
    {
        private readonly _2B_EgyptDBContext _context;
        public BrandRepository(_2B_EgyptDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> SeaechByName(string name)
        {
            return await _context.brands.AnyAsync(c => c.nameEn == name);
        }
       
    }
}
