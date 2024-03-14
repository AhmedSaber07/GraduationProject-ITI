using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Dtos.ViewResult
{
    public class resultDto<TEntity>
    {
        public bool IsSuccess {  get; set; }
        public string Message { get; set; }
        public TEntity Entity { get; set; }
    }
}
