using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Settings
{
    public class MailSettings
    {
        public string From { get; set; }
        public string UserName { get; set; }
      
        public string Password { get; set; }
        public string Smtpserver { get; set; }

        public int Port { get; set; }
    }
}
