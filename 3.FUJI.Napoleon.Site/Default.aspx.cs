using _2.FUJI.Napoleon.AccesoDatos.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3.FUJI.Napoleon.Site
{
    public partial class Default : System.Web.UI.Page
    {
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Log.EscribeLog("Usuario: " + Session["UserID"].ToString());
                Log.EscribeLog("intUsuarioID: " + Session["intUsuarioID"].ToString());
                Log.EscribeLog("Token: " + Session["Token"].ToString());
                if (Session["UserID"] != null && Session["UserID"].ToString() != "" &&
                    Security.ValidateToken(Session["Token"].ToString(), Session["intUsuarioID"].ToString(), Session["UserID"].ToString(), Session["Password"].ToString()))
                {

                }
                else
                {
                    Log.EscribeLog("No hay registro de sesiones.");
                    Response.Redirect(URL + "/frmLogin.aspx");
                }
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error:" + ePL.Message);
            }
        }
    }
}