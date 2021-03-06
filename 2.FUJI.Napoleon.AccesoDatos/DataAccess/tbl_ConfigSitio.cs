//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _2.FUJI.Napoleon.AccesoDatos.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_ConfigSitio
    {
        public tbl_ConfigSitio()
        {
            this.tbl_MST_Estudio = new HashSet<tbl_MST_Estudio>();
            this.tbl_REL_SitioModalidad = new HashSet<tbl_REL_SitioModalidad>();
            this.tbl_DET_ServicioSitio = new HashSet<tbl_DET_ServicioSitio>();
            this.tbl_ConfigSitio_AUD = new HashSet<tbl_ConfigSitio_AUD>();
            this.tbl_RegistroSitio = new HashSet<tbl_RegistroSitio>();
            this.tbl_REL_ProyectoSitio = new HashSet<tbl_REL_ProyectoSitio>();
            this.tbl_CAT_Usuarios = new HashSet<tbl_CAT_Usuarios>();
            this.tbl_DET_Sitio = new HashSet<tbl_DET_Sitio>();
        }
    
        public int id_Sitio { get; set; }
        public string vchClaveSitio { get; set; }
        public string vchnombreSitio { get; set; }
        public string vchIPCliente { get; set; }
        public string vchMaskCliente { get; set; }
        public Nullable<int> intPuertoCliente { get; set; }
        public string vchAETitle { get; set; }
        public string vchPathLocal { get; set; }
        public string vchIPServidor { get; set; }
        public Nullable<int> in_tPuertoServer { get; set; }
        public string vchAETitleServer { get; set; }
        public Nullable<System.DateTime> datFechaSistema { get; set; }
        public string vchUserAdmin { get; set; }
        public Nullable<bool> bitActivo { get; set; }
    
        public virtual ICollection<tbl_MST_Estudio> tbl_MST_Estudio { get; set; }
        public virtual ICollection<tbl_REL_SitioModalidad> tbl_REL_SitioModalidad { get; set; }
        public virtual ICollection<tbl_DET_ServicioSitio> tbl_DET_ServicioSitio { get; set; }
        public virtual ICollection<tbl_ConfigSitio_AUD> tbl_ConfigSitio_AUD { get; set; }
        public virtual ICollection<tbl_RegistroSitio> tbl_RegistroSitio { get; set; }
        public virtual ICollection<tbl_REL_ProyectoSitio> tbl_REL_ProyectoSitio { get; set; }
        public virtual ICollection<tbl_CAT_Usuarios> tbl_CAT_Usuarios { get; set; }
        public virtual ICollection<tbl_DET_Sitio> tbl_DET_Sitio { get; set; }
    }
}
