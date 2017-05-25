using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class ProyectoRequest
    {
        public tbl_CAT_Proyecto mdlProyecto;
        public List<tbl_REL_ProyectoSitio> lstSitos;
        public List<clsConfigSitio> lstSites;
        public string mensaje { get; set; }
        public ProyectoRequest()
        {
            mdlProyecto = new tbl_CAT_Proyecto();
            lstSitos = new List<tbl_REL_ProyectoSitio>();
            lstSites = new List<clsConfigSitio>();
            mensaje = string.Empty;
        }
    }
}