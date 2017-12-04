using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class PrioridadRequest
    {
        public int intEstudioID { get; set; }
        public int intDireccion { get; set; }
        public int intSecuenciaActual { get; set; }
        public string Token { get; set; }
        public String intUsuarioID { get; set; }
        public String vchUsuario { get; set; }
        public String vchPassword { get; set; }

        public PrioridadRequest()
        {
            intEstudioID = int.MinValue;
            intDireccion = int.MinValue;
            intSecuenciaActual = int.MinValue;
            Token = string.Empty;
            intUsuarioID = string.Empty;
            vchUsuario = string.Empty;
            vchPassword = string.Empty;
        }
    }
}