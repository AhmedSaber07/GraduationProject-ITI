using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.listResultDto
{
    public class CartListDto<TEntity>
    {
        public IEnumerable<TEntity> entities { get; set; }
        public int count { get; set; }
        public decimal TotalPrice { get; set; }

        public CartListDto()
        {
            entities = new List<TEntity>();
        }
    }
}
