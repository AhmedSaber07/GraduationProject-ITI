using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.UserAccount
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
