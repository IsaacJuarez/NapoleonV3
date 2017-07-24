using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
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
    public partial class AgregarSitio : System.Web.UI.Page
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
                if (Session["UserID"] != null && Session["UserID"].ToString() != "" &&
                Security.ValidateToken(Session["Token"].ToString(), Session["intUsuarioID"].ToString(), Session["UserID"].ToString(), Session["Password"].ToString()))
                {
                    if (!IsPostBack)
                    {
                        enableClean(true);
                        clsUsuario user = new clsUsuario();
                        user = (clsUsuario)Session["tbl_CAT_Usuarios"];
                        txtVendedor.Text = user.vchNombre;
                        txtVendedor.Enabled = false;
                    }
                }
                else
                {
                    Response.Redirect(URL + "/frmLogin.aspx");
                }
            }
            catch (Exception ePL)
            {
                ShowMessage("Error al Cargar la página: " + ePL.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Error al Cargar la página: " + ePL.Message);
            }
        }

        private void enableClean(bool v)
        {
            try
            {
                if (v)
                {
                    txtClaveSitio.Text = "";
                    txtEmail.Text = "";
                    txtNombreContacto.Text = "";
                    txtNombreSitio.Text = "";
                    txtNumContacto.Text = "";
                    txtVendedor.Text = "";
                }
                txtClaveSitio.Enabled = v;
                txtEmail.Enabled = v;
                txtNombreContacto.Enabled = v;
                txtNombreSitio.Enabled = v;
                txtNumContacto.Enabled = v;
                txtVendedor.Enabled = v;
                btnCrearSitio.Enabled = v;

            }
            catch (Exception eB)
            {
                Log.EscribeLog("Error al limpiar los datos:" + eB.Message);
                throw eB;
            }
        }

        protected void btnCrearSitio_Click(object sender, EventArgs e)
        {
            try
            {
                //obenter modelo
                tbl_ConfigSitio site = new tbl_ConfigSitio();
                tbl_RegistroSitio reg = new tbl_RegistroSitio();
                site.vchClaveSitio = txtClaveSitio.Text;
                site.vchnombreSitio = txtNombreSitio.Text;
                site.vchAETitle = "AE" + txtClaveSitio.Text;
                site.bitActivo = true;

                reg.vchNombreCliente = txtNombreContacto.Text;
                reg.vchEmail = txtEmail.Text;
                //reg.vchpassword = Security.Decrypt(txtPassSitio.Text);
                reg.vchNumeroContacto = txtNumContacto.Text;
                reg.vchVendedor = txtVendedor.Text;
                reg.bitActivo = true;
                //validar SItio
                bool valido = false;
                valido = NapoleonDA.validarSitio(site.vchClaveSitio);
                if (valido)
                {
                    if (site != null && reg != null)
                    {
                        clsMensaje response = new clsMensaje();
                        response = NapoleonDA.setSitio(site, reg);
                        if (response.valido)
                        {
                            enableClean(false);
                            ShowMessage("El Sitio " + txtClaveSitio.Text + " se reservó correctamente", MessageType.Correcto, "alert_container");
                        }
                        else
                        {
                            ShowMessage("Existe un error:" + response.vchMensaje, MessageType.Informacion, "alert_container");
                        }
                    }
                    else
                    {
                        ShowMessage("Favor de verificar la información.", MessageType.Informacion, "alert_container");
                    }
                }
                else
                {
                    ShowMessage("La clave para el sitio ya existe, favor de verificar.", MessageType.Informacion, "alert_container");
                }
                //almacenar Sitio
            }
            catch (Exception eCS)
            {
                ShowMessage("Existe un error al crear el sitio: " + eCS.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error al crear el sitio: " + eCS.Message);
            }
        }

        private void obtenerSitio(ref tbl_ConfigSitio mdl, ref tbl_RegistroSitio reg)
        {
            try
            {
                mdl.vchClaveSitio = txtClaveSitio.Text.ToUpper();
                mdl.vchnombreSitio = txtNombreSitio.Text.ToUpper();

                reg.vchNombreCliente = txtNombreContacto.Text.ToUpper();
                reg.vchEmail = txtEmail.Text;
                reg.vchNumeroContacto = txtNumContacto.Text;
                reg.vchVendedor = txtVendedor.Text;
            }
            catch (Exception eOS)
            {
                Log.EscribeLog("Existe un error al obtenerSitio: " + eOS.Message);
            }
        }

        public enum MessageType { Correcto, Error, Informacion, Advertencia };

        protected void ShowMessage(string Message, MessageType type, String container)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message.Replace("'", "") + "','" + type + "','" + container + "');", true);
            }
            catch (Exception eSM)
            {
                throw eSM;
            }
        }
    }
}