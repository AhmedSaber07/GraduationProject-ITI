using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.listResultDto
{
    public class ReviewListDto<TEntity>
    {
        public IEnumerable<TEntity> entities { get; set; }
        public decimal Rating { get; set; }

        public ReviewListDto()
        {
            entities = new List<TEntity>();
        }
    }
}
