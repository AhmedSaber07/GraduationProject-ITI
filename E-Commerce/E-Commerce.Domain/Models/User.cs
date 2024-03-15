using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Domain.Models
{
    public class User: BaseEntity
    {
       public string addressLine1 {  get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
