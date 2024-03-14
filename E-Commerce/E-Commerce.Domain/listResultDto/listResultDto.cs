using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.listResultDto
{
    public class listResultDto<TEntity>
    {
        public IEnumerable<TEntity> Entities;
        public int count;
        public listResultDto()
        {
            Entities = new List<TEntity>();
        }
    }
}
