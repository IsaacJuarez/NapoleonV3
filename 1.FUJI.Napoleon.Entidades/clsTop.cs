using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.FUJI.Napoleon.Entidades
{
    public class clsTop
    {
        public string TotalEstEnviados { get; set; }
        public string PendientesEnvSC { get; set; }
        public string PendientesEnvCSy { get; set; }
        public string TamañoTotalArc { get; set; }
        public string PromedioArchivos { get; set; }

        public clsTop()
        {
            TotalEstEnviados = "0";
            TamañoTotalArc = "0";
            PendientesEnvCSy = "0";
            PendientesEnvSC = "0";
            PromedioArchivos = "0";
        }
    }
}
