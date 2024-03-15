using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.listResultDto
{
    public class listResultDto<TEntity>
    {
        public IEnumerable<TEntity> entities;
        public int count;
        public listResultDto()
        {
            entities = new List<TEntity>();
        }
    }
}
