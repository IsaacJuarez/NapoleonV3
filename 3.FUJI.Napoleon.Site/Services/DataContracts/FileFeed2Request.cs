using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class FileFeed2Request
    {
        public tbl_CAT_Feed2Version file;
        public string Token { get; set; }
        public String intUsuarioID { get; set; }
        public String vchUsuario { get; set; }
        public String vchPassword { get; set; }

        public FileFeed2Request()
        {
            file = new tbl_CAT_Feed2Version();
            intUsuarioID = string.Empty;
            vchUsuario = string.Empty;
            vchPassword = string.Empty;
        }
    }
}