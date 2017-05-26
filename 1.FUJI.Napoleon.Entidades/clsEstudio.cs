using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.FUJI.Napoleon.Entidades
{
    public class clsEstudio
    {
        public int intEstudioID { get; set; }
        public int intProyectoID { get; set; }
        public int id_Sitio { get; set; }
        public string vchClaveSitio { get; set; }
        public int intModalidadID { get; set; }
        public string vchModalidadID { get; set; }
        public string vchAccessionNumber { get; set; }
        public string vchPatientBirthDate { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public int intNumeroArchivo { get; set; }
        public int intTamanoTotal { get; set; }
        public DateTime datFecha { get; set; }
        public bool bitUrgente { get; set; }
        public int intEstatusID { get; set; }
        public string vchEstatusID { get; set; }

        public clsEstudio()
        {
            intEstudioID = int.MinValue;
            intProyectoID = int.MinValue;
            id_Sitio = int.MinValue;
            vchClaveSitio = string.Empty;
            intModalidadID = int.MinValue;
            vchModalidadID = string.Empty;
            vchAccessionNumber = string.Empty;
            vchPatientBirthDate = string.Empty;
            PatientID = string.Empty;
            PatientName = string.Empty;
            intTamanoTotal = int.MinValue;
            intNumeroArchivo = int.MinValue;
            datFecha = DateTime.MinValue;
            bitUrgente = false;
            intEstatusID = int.MinValue;
            vchEstatusID = string.Empty;
        }
    }
}
