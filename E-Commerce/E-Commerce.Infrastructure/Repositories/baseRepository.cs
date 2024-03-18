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
    public class baseRepository<TEntity, TID> : ibaseRepository<TEntity, TID> where TEntity : BaseEntity
    {
        private readonly _2B_EgyptDBContext _context;
        public baseRepository(_2B_EgyptDBContext context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await _context.Set<TEntity>().AddAsync(entity)).Entity;
        }

        public Task<TEntity> HardDeleteAsync(TEntity entity)
        {

            return Task.FromResult(_context.Set<TEntity>().Remove(entity).Entity);
        }
        public Task<TEntity> SoftDeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
                return Task.FromResult(_context.Set<TEntity>().Where(E => E.IsDeleted == false));
        }

        public async Task<TEntity> GetByIdAsync(TID id, string[] includes = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query=query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(E=>E.Id.Equals(id));
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(_context.Set<TEntity>().Update(entity).Entity);
        }

        public async Task<bool> EntityExist(TID id)
        {
            return (await _context.Set<TEntity>().AnyAsync(E=>E.Id.Equals(id)));

        }
    }
}
