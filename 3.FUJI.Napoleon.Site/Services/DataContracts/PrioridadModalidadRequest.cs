using _1.FUJI.Napoleon.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class PrioridadModalidadRequest
    {
        public clsPrioridadSucursal mdlPrioridad;
        public string Token { get; set; }
        public String intUsuarioID { get; set; }
        public String vchUsuario { get; set; }
        public String vchPassword { get; set; }

        public PrioridadModalidadRequest()
        {
            mdlPrioridad = new clsPrioridadSucursal();
            Token = string.Empty;
            intUsuarioID = String.Empty;
            vchUsuario = string.Empty;
            vchPassword = string.Empty;
        }
    }
}