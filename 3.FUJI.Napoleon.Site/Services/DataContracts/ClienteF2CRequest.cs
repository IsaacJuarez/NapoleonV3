using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class ClienteF2CRequest
    {
        public string Token { get; set; }
        public string vchClaveSitio { get; set; }
        public String intUsuarioID { get; set; }
        public String vchUsuario { get; set; }
        public String vchPassword { get; set; }
        public int id_Sitio { get; set; }
        public int tipoServicio { get; set; }
        public int intDetEstudioID { get; set; }
        public tbl_ConfigSitio mdlConfig;
        public clsConfiguracion mdlConfiguracion;
        public string vchPathServer { get; set; }

        public clsEstudio estudio;

        public ClienteF2CRequest()
        {
            Token = string.Empty;
            vchClaveSitio = string.Empty;
            intUsuarioID = String.Empty;
            vchUsuario = string.Empty;
            vchPassword = string.Empty;
            id_Sitio = int.MinValue;
            tipoServicio = int.MinValue;
            estudio = new clsEstudio();
            intDetEstudioID = int.MinValue;
            mdlConfig = new tbl_ConfigSitio();
            mdlConfiguracion = new clsConfiguracion();
            vchPathServer = string.Empty;
        }
    }
}