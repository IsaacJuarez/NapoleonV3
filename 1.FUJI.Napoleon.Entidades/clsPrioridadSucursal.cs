using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.FUJI.Napoleon.Entidades
{
    public class clsPrioridadSucursal
    {
        public int intREL_SitioModID { get; set; }
        public int id_Sitio { get; set; }
        public string vchSitio { get; set; }
        public int intModalidadID { get; set; }
        public string vchModalidadID { get; set; }
        public int intSecuencia { get; set; }
        public DateTime datFecha { get; set; }
        public bool bitActivo { get; set; }
        public clsPrioridadSucursal()
        {
            intREL_SitioModID = int.MinValue;
            id_Sitio = int.MinValue;
            vchSitio = string.Empty;
            intModalidadID = int.MinValue;
            vchModalidadID = string.Empty;
            intSecuencia = int.MinValue;
            datFecha = DateTime.MinValue;
            bitActivo = false;
        }
    }
}
