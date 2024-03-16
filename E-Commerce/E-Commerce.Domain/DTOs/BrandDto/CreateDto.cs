using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.BrandDto
{
    public class CreateDto
    {
        public DateTime createdAt { get; set; }
        public string nameAr { get; set; }
        public string nameEn { get; set; }
        public string email { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
