using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.UserAccount
{
    public class ResetPasswordDto
    {
        public string Password { get; set; }    
        public string Email { get; set; }
        public string token { get; set; } 
    }
}
