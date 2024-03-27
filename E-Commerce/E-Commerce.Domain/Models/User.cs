using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace E_Commerce.Domain.Models
{
    public class MyUser : IdentityUser<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
       public string? addressLine1 {  get; set; }
        public string? addressLine2 { get; set; }
        public string? city { get; set; }
        public string? country { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
    }
}
