using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.FUJI.Napoleon.Entidades
{
    public class clsMensaje
    {
        public string vchMensaje { get; set; }
        public string vchError { get; set; }
        public bool valido { get; set; }
        public List<clsEstudio> _lstEst;
        public clsMensaje()
        {
            _lstEst = new List<clsEstudio>();
            vchError = string.Empty;
            vchMensaje = string.Empty;
            valido = false;
        }
    }
}
