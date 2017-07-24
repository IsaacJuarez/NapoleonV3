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
                    lblUserTop.Text = user.vchNombre.ToUpper() + " " + user.vchApellido.ToUpper();
                    lblUser.Text = Session["UserID"].ToString();
                    enableMenu(user.intTipoUsuarioID);
                }
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en PageLoad de SiteMaster: " + ePL.Message);
            }
        }

        private void enableMenu(int intTipoUsuarioID)
        {
            try
            {
                if (intTipoUsuarioID == 1 || intTipoUsuarioID == 2) //Administradores
                {
                    mnGeneral.Visible = true;
                    mnManagement.Visible = true;
                    mnDashBoarService.Visible = true;
                    mnConfigSites.Visible = true;
                    mnPrioridadSitio.Visible = true;
                    mnAdminUsers.Visible = true;
                    mnVersiones.Visible = true;
                    mnAddSite.Visible = false;
                    lblUserTop.Text = user.vchNombre.ToUpper();
                }
                else
                {
                    if (intTipoUsuarioID == 4)//Vendedor
                    {
                        mnGeneral.Visible = false;
                        mnManagement.Visible = false;
                        mnDashBoarService.Visible = true;
                        mnConfigSites.Visible = true;
                        mnPrioridadSitio.Visible = false;
                        mnAdminUsers.Visible = false;
                        mnVersiones.Visible = true;
                        mnAddSite.Visible = true;
                    }
                    else
                    {
                        if (intTipoUsuarioID == 3)//Medico
                        {
                            mnGeneral.Visible = true;
                            mnManagement.Visible = true;
                            mnDashBoarService.Visible = true;
                            mnConfigSites.Visible = true;
                            mnPrioridadSitio.Visible = true;
                            mnAdminUsers.Visible = false;
                            mnVersiones.Visible = false;
                            mnAddSite.Visible = false;
                        }
                        else
                        {
                            mnGeneral.Visible = false;
                            mnManagement.Visible = false;
                            mnDashBoarService.Visible = false;
                            mnConfigSites.Visible = false;
                            mnPrioridadSitio.Visible = false;
                            mnAdminUsers.Visible = false;
                            mnVersiones.Visible = false;
                            mnAddSite.Visible = false;
                        }
                    }
                }
            }
            catch (Exception eeM)
            {
                Log.EscribeLog("Existe un error en enableMenu de SiteMaster: " + eeM.Message);
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