﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.UserAccount
{
    public class RegisterDto
    {

        public string Email {  get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
