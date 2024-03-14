using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Context
{
    public interface ibaseRepository<TEntity, TID>
    {
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TID id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, int Id);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
