using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.FUJI.Napoleon.AccesoDatos
{
    public class Helper
    {

        public static string ConnectionString()
        {
            string  cadena = "";
            try
            {
                cadena = ConfigurationManager.ConnectionStrings["NapConString"].ConnectionString;
                
            }
            catch (Exception ehp)
            {
                throw new Exception(ehp.Message);
            }
            return cadena;
        }
    }
}
