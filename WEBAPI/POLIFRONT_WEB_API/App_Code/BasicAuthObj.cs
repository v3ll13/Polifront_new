using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POLIFRONT_WEB_API.App_Code
{
    public class BasicAuthObj
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string password_confirm { get; set; }
    }
}