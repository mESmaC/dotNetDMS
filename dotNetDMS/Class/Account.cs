using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dotNetDMS.Class
{
   public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string sessKey { get; set; }
    }
}
