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
    
    public partial class tbl_ConfigSitio_AUD
    {
        public int id_SitioAUD { get; set; }
        public Nullable<int> id_Sitio { get; set; }
        public string vchClaveSitio { get; set; }
        public string vchNombreSitio { get; set; }
        public string vchIPCliente { get; set; }
        public string vchMaskCliente { get; set; }
        public Nullable<int> intPuertoCliente { get; set; }
        public string vchAETitle { get; set; }
        public string vchPathLocal { get; set; }
        public string vchIPServidor { get; set; }
        public Nullable<int> intPuertoServer { get; set; }
        public string vchAETitleServer { get; set; }
        public Nullable<System.DateTime> datFechaSitema { get; set; }
        public string vchUserAdmin { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<int> TIPOMOV_ID { get; set; }
    
        public virtual tbl_ConfigSitio tbl_ConfigSitio { get; set; }
    }
}
