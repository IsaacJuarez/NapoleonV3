using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.Extensions;
using System;
using System.Configuration;
using System.Web;

namespace _3.FUJI.Napoleon.Site
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }
        public static clsUsuario user = new clsUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["UserID"] != null && Session["UserID"].ToString() != "" && Session["tbl_CAT_Usuarios"] != null &&
                    Security.ValidateToken(Session["Token"].ToString(), Session["intUsuarioID"].ToString(), Session["UserID"].ToString(), Session["Password"].ToString()))
                    {
                        if(Session["tbl_CAT_Usuarios"]!= null)
                        {
                            user = (clsUsuario)Session["tbl_CAT_Usuarios"];
                            if(user!= null)
                            {
                                if(user.intTipoUsuarioID ==1 || user.intTipoUsuarioID == 2)
                                {
                                    btnConfiguraciones.Visible = true;
                                }
                                else
                                {
                                    btnConfiguraciones.Visible = false;
                                }
                            }
                        }
                        hfURL.Value = URL;
                    }
                    lblUser.Text = Session["UserID"].ToString();
                }
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en PageLoad de SiteMaster: " + ePL.Message);
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                Response.Redirect(URL + "/frmLogin.aspx", false);
            }
            catch (Exception ebc)
            {
                throw ebc;
            }
        }
    }
}