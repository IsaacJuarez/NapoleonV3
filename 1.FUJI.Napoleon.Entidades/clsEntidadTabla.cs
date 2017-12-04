using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.FUJI.Napoleon.Entidades
{
    public class clsEntidadTabla
    {
        public string Numero_Estudio { get; set; }
        public string Modalidad { get; set; }
        public int Numero_Archivos { get; set; }
        public int Tamaño_Archivos { get; set; }
        public string Estatus { get; set; }
        public string Sucursal { get; set; }

        public clsEntidadTabla()
        {
            Numero_Archivos = int.MinValue;
            Numero_Estudio = string.Empty;
            Modalidad = string.Empty;
            Tamaño_Archivos = int.MinValue;
            Estatus = string.Empty;
            Sucursal = string.Empty;
        }
    }
}
