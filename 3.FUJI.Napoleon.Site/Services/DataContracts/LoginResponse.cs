using _1.FUJI.Napoleon.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            CurrentUser = new clsUsuario();
            Token = string.Empty;
            Success = false;
            vchMensaje = string.Empty;
        }
        public clsUsuario CurrentUser { get; set; }
        public string Token { get; set; }
        public bool Success { get; set; }
        public string vchMensaje { get; set; }
    }
}