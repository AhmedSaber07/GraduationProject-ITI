using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.CategoryDto
{
    public class CreateOrUpdateCategoryDto
    {
       
        public string NameAr { get; set; }

      
        public string NameEn { get; set; }

        public Guid? ParentCategoryId { get; set; }
    }
}
