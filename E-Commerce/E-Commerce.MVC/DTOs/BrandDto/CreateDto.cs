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
        public Guid id { get; set; }
        public DateTime createdAt { get; set; }
        public string nameAr { get; set; }
        public string nameEn { get; set; }
        public string email { get; set; }
       
    }
}
