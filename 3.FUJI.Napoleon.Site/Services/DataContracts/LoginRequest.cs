using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string vchSitio { get; set; }

        public LoginRequest()
        {
            username = string.Empty;
            password = string.Empty;
            vchSitio = string.Empty;
        }
    }
}