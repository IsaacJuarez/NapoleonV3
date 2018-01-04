using _2.FUJI.Napoleon.AccesoDatos.Extensions;
using System;

namespace _3.FUJI.Napoleon.Site
{
    public partial class frmManual : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        // Limpiamos la salida
                        Response.Clear();
                        // Con esto le decimos al browser que la salida sera descargable
                        Response.ContentType = "application/octet-stream";
                        String ID = Security.Decrypt(Request.QueryString["ID"].ToString());
                        switch (ID)
                        {
                            case "1"://Feed2
                                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                                Response.AddHeader("Content-Disposition", "attachment; filename=Data/ManualFeed2Cloud_Install.doc");
                                // Escribimos el fichero a enviar 
                                Response.WriteFile("Data/ManualFeed2Cloud_Install.doc");                               
                                break;
                            case "2"://SQL
                                // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                                Response.AddHeader("Content-Disposition", "attachment; filename=Data/SQLManualFeed2Cloud.doc");
                                // Escribimos el fichero a enviar
                                Response.WriteFile("Data/SQLManualFeed2Cloud.doc");
                                break;
                        }
                        // volcamos el stream 
                        Response.Flush();
                        // Enviamos todo el encabezado ahora
                    }
                    else
                    {
                        lblTextoError.Text = "Error : No es posible obtener los datos.";
                    }
                }
                else
                {
                    lblTextoError.Text = "Error : No es posible obtener los datos.";
                }
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error al descargar el archivo: " + ePL.Message);
            }
        }
    }
}