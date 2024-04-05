using E_Commerce.Domain.DTOs.productDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.CartDto
{
    public class GetCartDtoEnglish
    {
        public getProductCartDtoEnglish product { get; set; }
        public int Quantity { get; set; }
    }
}
