using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _3.FUJI.Napoleon.Site.Services.DataContracts
{
    public class ProyectoResponse
    {
        public bool success { get; set; }
        public string mensaje { get; set; }

        public ProyectoResponse()
        {
            success = false;
            mensaje = string.Empty;
        }
    }
}