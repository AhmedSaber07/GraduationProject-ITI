using E_Commerce.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
