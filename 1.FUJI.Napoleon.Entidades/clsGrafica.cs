using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.FUJI.Napoleon.Entidades
{
    public class clsGrafica
    {
        public string _Nombre { get; set; }
        public int _Valor { get; set; }
        public string _Color { get; set; }
        public string _hoverColor { get; set; }
        public clsGrafica()
        {
            _Nombre = string.Empty;
            _Valor = int.MinValue;
            _Color = string.Empty;
            _hoverColor = string.Empty;
        }
    }
}
