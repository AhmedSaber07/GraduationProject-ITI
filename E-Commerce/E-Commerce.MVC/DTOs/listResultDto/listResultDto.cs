using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.MVC.DTOs.listResultDto
{
    public class listResultDto<TEntity>
    {
        public IEnumerable<TEntity> entities { get; set; }
        public decimal count { get; set; }
        public listResultDto()
        {
            entities = new List<TEntity>();
        }
    }
}
