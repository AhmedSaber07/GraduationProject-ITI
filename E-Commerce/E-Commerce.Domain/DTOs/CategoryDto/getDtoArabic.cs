﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.CategoryDto
{
    public class getDtoArabic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<getDtoArabic>? children { get; set; }
    }
}
