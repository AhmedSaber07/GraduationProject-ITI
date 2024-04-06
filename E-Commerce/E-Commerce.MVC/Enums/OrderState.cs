using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.MVC.Enums
{
    public enum OrderStateEn
    {
        Pending = 1,
        Confirmed,
        Canceled,
        Received,
        Attempted_delivery,
    }
    public enum OrderStateAr
    {
        في_انتظار= 1,
        مؤكد,
        ألغيت,
        تلقى,
        حاول_التوصيل,
    }
}
