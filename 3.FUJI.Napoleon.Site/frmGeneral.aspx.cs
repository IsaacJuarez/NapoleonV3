using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.Extensions;
using _3.FUJI.Napoleon.Site.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3.FUJI.Napoleon.Site
{
    public partial class frmGeneral : System.Web.UI.Page
    {
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }

        NapoleonService NapoleonDA = new NapoleonService();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hflURL.Value = URL;
                if (Session["UserID"] != null && Session["UserID"].ToString() != "" && Session["tbl_CAT_Usuarios"] != null &&
                   Security.ValidateToken(Session["Token"].ToString(), Session["intUsuarioID"].ToString(), Session["UserID"].ToString(), Session["Password"].ToString()))
                {
                    DateTime datini, datfin;
                    datini = DateTime.Now.AddDays(-30);
                    datfin = DateTime.Today;
                    clsUsuario users = new clsUsuario();
                    users = (clsUsuario)Session["tbl_CAT_Usuarios"];
                    intProyectoID.Value = users.intProyectoID.ToString();
                    sucOID.Value = users.id_Sitio.ToString();
                    intTipoUsuarioID.Value = users.intTipoUsuarioID.ToString();
                    intUsuarioID.Value = Session["intUsuarioID"].ToString();
                }
                else
                {
                    Response.Redirect(URL + "/frmLogin.aspx");
                }
                //setMensaje("Error al cargar..", 2);
            }
            catch (Exception ePL)
            {
                ShowMessage("Existe un error: " + ePL.Message, MessageType.Error);
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fini = Convert.ToDateTime(fechIni.Value.ToString());
                DateTime ffin = Convert.ToDateTime(fechFin.Value.ToString());
            }
            catch (Exception eAct)
            {
                ShowMessage("Existe un error: " + eAct.Message, MessageType.Error);
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fini = Convert.ToDateTime(fechIni.Value.ToString());
                DateTime ffin = Convert.ToDateTime(fechFin.Value.ToString());
            }
            catch (Exception eExport)
            {
                Log.EscribeLog("Existe un error en btnExport_Click: " + eExport.Message);
            }
        }

        public enum MessageType { Correcto, Error, Informacion, Advertencia };

        protected void ShowMessage(string Message, MessageType type)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message.Replace("'","") + "','" + type + "');", true);
            }
            catch(Exception eSM)
            {
                Log.EscribeLog("Existe un error en  ShowMessage: " + eSM.Message);
            }
        }
    }
}