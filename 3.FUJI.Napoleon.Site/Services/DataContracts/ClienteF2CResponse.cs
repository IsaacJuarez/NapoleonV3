using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class ClienteF2CResponse
    {
        public clsConfiguracion ConfigSitio;
        public bool valido { get; set; }
        public string message { get; set; }
        public int id_Sitio { get; set; }

        public List<clsEstudio> lstEstudio;

        public string vchFormato { get; set; }

        public ClienteF2CResponse()
        {
            ConfigSitio = new clsConfiguracion();
            valido = false;
            message = string.Empty;
            lstEstudio = new List<clsEstudio>();
            id_Sitio = int.MinValue;
            vchFormato = string.Empty;
        }
    }
}