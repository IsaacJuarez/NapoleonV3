using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.FUJI.Napoleon.Entidades
{
    public class clsModeloCatalogo
    {
        public string vchDescripcion { get; set; }
        public string vchValue { get; set; }

        public clsModeloCatalogo()
        {
            vchDescripcion = string.Empty;
            vchValue = string.Empty;
        }
    }
}
