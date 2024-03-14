﻿using E_Commerce.Domain.Models;
using E_Commerce.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositories
{
    public class Repository<TEntity, TID> : ibaseRepository<TEntity, TID> where TEntity : BaseEntity
    {
        private readonly _2B_EgyptDBContext _context;
        public Repository(_2B_EgyptDBContext context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await _context.Set<TEntity>().AddAsync(entity)).Entity;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {

            return Task.FromResult(_context.Set<TEntity>().Remove(entity).Entity);
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_context.Set<TEntity>().Where(E => E.IsDeleted == false));
        }

        public async Task<TEntity> GetByIdAsync(TID id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<TEntity> UpdateAsync(TEntity entity, int Id)
        {
            throw new NotImplementedException();
        }
    }
}