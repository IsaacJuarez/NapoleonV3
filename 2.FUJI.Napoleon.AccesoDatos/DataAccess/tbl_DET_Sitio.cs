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
    
    public partial class tbl_DET_Sitio
    {
        public int intDETSitioID { get; set; }
        public Nullable<int> id_Sitio { get; set; }
        public string vchVNAIP { get; set; }
        public string vchAETitleSCU { get; set; }
        public string vchAETitleSCP { get; set; }
        public Nullable<int> intPuertoSCP { get; set; }
        public Nullable<bool> bitActivo { get; set; }
        public Nullable<System.DateTime> datFecha { get; set; }
        public string vchUserAdmin { get; set; }
    
        public virtual tbl_ConfigSitio tbl_ConfigSitio { get; set; }
    }
}