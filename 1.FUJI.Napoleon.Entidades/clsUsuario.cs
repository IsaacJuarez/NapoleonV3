using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.FUJI.Napoleon.Entidades
{
    public class clsUsuario
    {

        public int intUsuarioID { get; set; }

        public int intTipoUsuarioID { get; set; }
        public int intProyectoID { get; set; }
        public string vchProyectoID { get; set; }
        public string vchNombre { get; set; }

        public string vchApellido { get; set; }

        public string vchUsuario { get; set; }

        public string vchPassword { get; set; }

        public bool bitActivo { get; set; }

        public System.DateTime datFecha { get; set; }

        public string vchUserAdmin { get; set; }

        public string Token { get; set; }
        public clsUsuario()
        {
            intUsuarioID = int.MinValue;
            intTipoUsuarioID = int.MinValue;
            intProyectoID = int.MinValue;
            vchProyectoID = string.Empty;
            vchNombre = string.Empty;
            vchApellido = string.Empty;
            vchUsuario = string.Empty;
            vchPassword = string.Empty;
            bitActivo = false;
            datFecha = DateTime.MinValue;
            vchUserAdmin = string.Empty;
            Token = string.Empty;
        }
    }
}
