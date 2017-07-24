using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.FUJI.Napoleon.Entidades
{
    public class clsCorreo
    {
        public string correo { get; set; }
        public string asunto { get; set; }
        public string usuarioCorreo { get; set; }
        public string passwordCorreo { get; set; }
        public string toEmail { get; set; }
        public string htmlCorreo { get; set; }
        public int intUsuarioID { get; set; }
        public clsCorreo()
        {
            correo = string.Empty;
            asunto = string.Empty;
            usuarioCorreo = string.Empty;
            passwordCorreo = string.Empty;
            toEmail = string.Empty;
            htmlCorreo = string.Empty;
        }
    }
}
