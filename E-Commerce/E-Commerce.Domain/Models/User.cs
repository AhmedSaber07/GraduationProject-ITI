﻿using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Domain.Models
{
    public class MyUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? addressLine1 {  get; set; }
        public string? addressLine2 { get; set; }
        public string? city { get; set; }
        public string? country { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
    }
}
