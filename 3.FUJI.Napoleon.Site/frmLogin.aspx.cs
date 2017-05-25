using _2.FUJI.Napoleon.AccesoDatos.Extensions;
using _3.FUJI.Napoleon.Site.Services;
using _3.FUJI.Napoleon.Site.Services.DataContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3.FUJI.Napoleon.Site
{
    public partial class frmLogin : System.Web.UI.Page
    {
        NapoleonService NapoleonDA = new NapoleonService();
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblLogin.Text = "";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //tbl_CAT_Usuarios _user = new tbl_CAT_Usuarios();
                LoginRequest _req = new LoginRequest();
                _req.password = Security.Encrypt(txtContrasenaUser.Text);
                _req.username = txtUsuarioUser.Text;
                LoginResponse access = NapoleonDA.Logear(_req);
                if (access != null)
                {
                    if (access.Success)
                    {
                        Session["UserID"] = access.CurrentUser.vchUsuario;
                        Session["intUsuarioID"] = access.CurrentUser.intUsuarioID;
                        Session["Password"] = access.CurrentUser.vchPassword;
                        Session["intTipoUsuario"] = access.CurrentUser.intTipoUsuarioID;
                        Session["tbl_CAT_Usuarios"] = access.CurrentUser;
                        Session["Token"] = access.CurrentUser.Token;
                        //Session["sucOID"] = access.CurrentUser.sucOID;
                        Response.Redirect(URL + "/Default.aspx", false);
                        lblLogin.Text = "Inicio de Sesion correcta";
                        Log.EscribeLog("Usuario correcto. " + access.CurrentUser.vchUsuario);
                        Log.EscribeLog(URL + "/Default.aspx");
                    }
                    else
                    {
                        lblLogin.Text = "Usuario o contraseña invalida.";
                        lblLogin.ForeColor = System.Drawing.Color.Red;
                        Log.EscribeLog("Usuario o contraseña invalida.");
                        Log.EscribeLog("Credenciales intento: " + txtUsuarioUser.Text + " , " + txtContrasenaUser.Text);
                    }
                }
                else
                {
                    lblLogin.Text = "Usuario o contraseña invalida.";
                    lblLogin.ForeColor = System.Drawing.Color.Red;
                    Log.EscribeLog("Usuario o contraseña invalida.");
                    Log.EscribeLog("Credenciales intento: " + txtUsuarioUser.Text + " , " + txtContrasenaUser.Text);
                }
            }
            catch (Exception eL)
            {
                Log.EscribeLog("Existe un error al iniciar: " + eL.Message);
                lblLogin.Text = "Existe un error." + eL.Message;
                lblLogin.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}