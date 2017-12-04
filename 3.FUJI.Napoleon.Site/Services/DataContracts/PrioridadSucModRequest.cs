using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class PrioridadSucModRequest
    {
        public int mosID { get; set; }
        public bool activar { get; set; }
        public string Token { get; set; }
        public string intUsuarioID { get; set; }
        public string vchUsuario { get; set; }
        public string vchPassword { get; set; }

        public PrioridadSucModRequest()
        {
            mosID = int.MinValue;
            activar = false;
            Token = string.Empty;
            intUsuarioID = string.Empty;
            vchUsuario = string.Empty;
            vchPassword = string.Empty;
        }
    }
}