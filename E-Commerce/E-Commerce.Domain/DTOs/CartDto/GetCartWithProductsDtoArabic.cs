using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.DTOs.CartDto
{
    public class GetCartWithProductsDtoArabic
    {
        public Guid Id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int Quantity { get; set; }
        public Guid SessionId { get; set; }

        public virtual ICollection<GetCartWithProductsDtoArabic> Products { get; set; }

        public GetCartWithProductsDtoArabic()
        {
            Products = new List<GetCartWithProductsDtoArabic>();
        }
    }
}
