﻿using E_Commerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace E_Commerce.Domain.DTOs.CartDto
{
    public class CreateOrUpdateDto
    {
        public int Quantity { get; set; }
        public decimal ItemTotal { get; set; }
        public Guid SessionId { get; set; }
        public Guid ProductId { get; set; }

    }
}