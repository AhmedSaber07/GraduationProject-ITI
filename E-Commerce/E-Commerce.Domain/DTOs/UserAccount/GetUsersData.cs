using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace E_Commerce.Domain.DTOs.UserAccount
{
    public class GetUsersData
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string phone { get; set; }
        public string LastName { get; set; }
        public string? addressLine1 { get; set; }
        public string? addressLine2 { get; set; }
        public string? city { get; set; }
        public string? country { get; set; }
    }
}
