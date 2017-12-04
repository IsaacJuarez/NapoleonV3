using _1.FUJI.Napoleon.Entidades;
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
    public partial class frmUpdateUser : System.Web.UI.Page
    {
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }
        NapoleonService NapoleonDA = new NapoleonService();

        public static string contrasenia = "";

        public static clsUsuario user = new clsUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserID"] != null && Session["UserID"].ToString() != "" && Session["tbl_CAT_Usuarios"] != null &&
               Security.ValidateToken(Session["Token"].ToString(), Session["intUsuarioID"].ToString(), Session["UserID"].ToString(), Session["Password"].ToString()))
                {
                    if (!IsPostBack)
                    {
                        user = (clsUsuario)Session["tbl_CAT_Usuarios"];
                        fill(user);
                    }
                }
                else
                {
                    Response.Redirect(URL + "/frmLogin.aspx", false);
                }
            }
            catch(Exception ePL)
            {
                ShowMessage("Existe un error al cargar la página: " + ePL.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en Page_Load: " + ePL.Message);
            }
        }

        private void fill(clsUsuario user)
        {
            try
            {
                txtApellido.Text = user.vchApellido;
                txtEmail.Text = user.vchCorreo;
                txtNombre.Text = user.vchNombre;
                txtPass.Text = Security.Decrypt(user.vchPassword);
                contrasenia = Security.Decrypt(user.vchPassword);
            }
            catch(Exception efill)
            {
                Log.EscribeLog("Existe un error en fill: " + efill.Message);
            }
        }

        protected void btnChangePass_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if(btnChangePass.Checked)
                {
                    RequiredcFieldValidator2.Enabled = true;
                    RequiredFieldValidator3.Enabled = true;
                    txtPass.Enabled = true;
                    txtRePass.Enabled = true;
                    txtPass.Text = "";
                    txtRePass.Text = "";
                }
                else
                {
                    RequiredcFieldValidator2.Enabled = false;
                    RequiredFieldValidator3.Enabled = false;
                    txtPass.Enabled = false;
                    txtRePass.Enabled = false;
                }
                
            }
            catch (Exception ebCP)
            {
                //ShowMessage("Existe un error al cargar la página: " + ePL.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error en btnChangePass_Click: " + ebCP.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool valido = false;
                if (btnChangePass.Checked)
                {
                    if (txtPass.Text != Security.Decrypt(user.vchPassword))
                    {
                        if(txtPass.Text == txtRePass.Text)
                        {
                            valido = true;
                        }
                        else
                        {
                            ShowMessage("La contraseña no coincide, favor de verificar.", MessageType.Advertencia, "alert_container");
                            valido = false;
                        }
                    }
                    else
                    {
                        valido = false;
                        ShowMessage("Favor de capturar una contraseña diferente a la actual.", MessageType.Advertencia, "alert_container");
                    }
                }
                else
                {
                    valido = true;
                }

                if (valido)
                {
                    clsUsuario mdl = new clsUsuario();
                    mdl = obtenerUsuario();
                    if (mdl != null)
                    {
                        UserRequest request = new UserRequest();
                        clsMensaje response = new clsMensaje();
                        request.user = mdl;
                        request.intUsuarioID = user.intUsuarioID.ToString();
                        request.vchPassword = user.vchPassword;
                        request.vchUsuario = user.vchUsuario;
                        request.Token = user.Token;
                        if (request != null)
                        {
                            response = NapoleonDA.setActualizaUser(request);
                            if (response.valido)
                            {
                                ShowMessage("Cambios correctos, favor de iniciar sesión.", MessageType.Correcto, "alert_container");
                                if (btnChangePass.Checked)
                                {
                                    Session.Clear();
                                    Response.Redirect(URL + "/frmLogin.aspx", false);
                                }
                            }
                            else
                            {
                                ShowMessage("Existe un error al guardar: " + response.vchMensaje, MessageType.Error, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Favor de verificar la información.", MessageType.Informacion, "alert_container");
                        }
                    }
                    else
                    {
                        ShowMessage("Favor de verificar la información.", MessageType.Informacion, "alert_container");
                    }
                }
            }
            catch (Exception eGU)
            {
                ShowMessage("Existe un error: " + eGU.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error al guardar: " + eGU.Message);
            }
        }

        private clsUsuario obtenerUsuario()
        {
            clsUsuario mdl = new clsUsuario();
            try
            {
                mdl.intUsuarioID = user.intUsuarioID;
                mdl.vchNombre = txtNombre.Text;
                mdl.vchApellido = txtApellido.Text;
                mdl.vchCorreo = txtEmail.Text;
                mdl.vchPassword = Security.Encrypt(txtPass.Text);
                mdl.bitGuardarCambios = btnChangePass.Checked;
            }
            catch(Exception eoU)
            {
                mdl = null;
                Log.EscribeLog("Existe un error al obtener los datos: " + eoU.Message);
            }
            return mdl;
        }

        public enum MessageType { Correcto, Error, Informacion, Advertencia };

        protected void ShowMessage(string Message, MessageType type, String container)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "','" + container + "');", true);
            }
            catch (Exception eSM)
            {
                throw eSM;
            }
        }
    }
}