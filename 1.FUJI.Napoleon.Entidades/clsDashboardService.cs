using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.FUJI.Napoleon.Entidades
{
    public class clsDashboardService
    {
        public string vchClaveSitio { get; set; }
        public int id_sitio { get; set; }
        public string vchNombreSitio { get; set; }
        public int intProyectoID { get; set; }
        public DateTime datFechaSCP { get; set; }
        public DateTime datFechaSCU { get; set; }
        public DateTime datFechaSync { get; set; }
        public clsDashboardService()
        {
            id_sitio = int.MinValue;
            intProyectoID = int.MinValue;
            vchClaveSitio = string.Empty;
            vchNombreSitio = string.Empty;
            datFechaSCP = DateTime.MinValue;
            datFechaSCU = DateTime.MinValue;
            datFechaSync = DateTime.MinValue;
        }

    }
}
