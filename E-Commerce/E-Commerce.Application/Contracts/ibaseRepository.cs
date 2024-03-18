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
        Task<TEntity> GetByIdAsync(TID id, string[] includes = null);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> HardDeleteAsync(TEntity entity);
        Task<TEntity> SoftDeleteAsync(TEntity entity);
        Task<bool> EntityExist(TID entity);
        Task<int> SaveChangesAsync();
    }
}
