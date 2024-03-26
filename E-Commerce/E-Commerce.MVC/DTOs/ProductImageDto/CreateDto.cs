using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.MVC.DTOs.ProductImageDto
{
    public class CreateDto
    {
        public Guid productId { get; set; }
        public string imageUrl { get; set; }
    }
}
