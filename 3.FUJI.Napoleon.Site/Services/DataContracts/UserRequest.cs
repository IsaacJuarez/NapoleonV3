using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class UserRequest
    {
        public clsUsuario user;
        public tbl_CAT_Usuarios usuario;
        public string Token { get; set; }
        public String intUsuarioID { get; set; }
        public String vchUsuario { get; set; }
        public String vchPassword { get; set; }
        public string mensaje { get; set; }

        public UserRequest()
        {
            user = new clsUsuario();
            usuario = new tbl_CAT_Usuarios();
            Token = string.Empty;
            intUsuarioID = string.Empty;
            vchUsuario = string.Empty;
            vchPassword = string.Empty;
            mensaje = string.Empty;
        }
    }
}