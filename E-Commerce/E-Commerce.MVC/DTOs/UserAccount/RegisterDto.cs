using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.MVC.DTOs.UserAccount
{
    public class RegisterDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string Email {  get; set; }
        public string phoneNumber { get; set; }
        public string Password { get; set; }
    }
}
