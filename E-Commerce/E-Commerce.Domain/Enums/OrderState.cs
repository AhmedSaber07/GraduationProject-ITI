using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Enums
{
    public enum OrderState
    {
        Pending = 1,
        Confirmed,
        Canceled,
        Received,
        Attempted_delivery,
    }
}
