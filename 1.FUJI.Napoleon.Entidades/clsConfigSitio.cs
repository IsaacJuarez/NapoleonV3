using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.FUJI.Napoleon.Entidades
{
    public class clsConfigSitio
    {
        public int id_Sitio { get; set; }
        public string vchClaveSitio { get; set; }
        public string vchnombreSitio { get; set; }
        public string vchIPCliente { get; set; }
        public string vchMaskCliente { get; set; }
        public int intPuertoCliente { get; set; }
        public string vchAETitle { get; set; }
        public string vchPathLocal { get; set; }
        public string vchIPServidor { get; set; }
        public int in_tPuertoServer { get; set; }
        public string vchAETitleServer { get; set; }
        public DateTime datFechaSistema { get; set; }
        public string vchUserAdmin { get; set; }
        public bool bitActivo { get; set; }
        public bool bitSeleccionado { get; set; }

        public clsConfigSitio()
        {
            id_Sitio = int.MinValue;
            vchClaveSitio = string.Empty;
            vchnombreSitio = string.Empty;
            vchIPCliente = string.Empty;
            vchMaskCliente = string.Empty;
            intPuertoCliente = int.MinValue;
            vchAETitle = string.Empty;
            vchPathLocal = string.Empty;
            vchIPServidor = string.Empty;
            in_tPuertoServer = int.MinValue;
            vchAETitleServer = string.Empty;
            datFechaSistema = DateTime.MinValue;
            vchUserAdmin = string.Empty;
            bitActivo = false;
            bitSeleccionado = false;
        }
    }
}
