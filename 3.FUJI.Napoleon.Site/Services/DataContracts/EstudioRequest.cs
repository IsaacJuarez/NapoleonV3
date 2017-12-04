using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class EstudioRequest
    {
        public string Token { get; set; }
        public String intUsuarioID { get; set; }
        public String vchUsuario { get; set; }
        public String vchPassword { get; set; }
        public tbl_MST_PrioridadEstudio _mdlPrioridad;

        public EstudioRequest()
        {
            Token = string.Empty;
            intUsuarioID = string.Empty;
            vchUsuario = string.Empty;
            vchPassword = string.Empty;
            _mdlPrioridad = new tbl_MST_PrioridadEstudio();
        }
    }
}