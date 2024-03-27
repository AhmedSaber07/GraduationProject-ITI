using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.productDto
{
    public class getProductCartDto
    {
        public string nameAr { get; set; }
        public string nameEn { get; set; }
        public string descriptionAr { get; set; }
        public string descriptionEn { get; set; }
        public decimal price { get; set; } 
        public List<string> Images { get; set; }

    }
}
