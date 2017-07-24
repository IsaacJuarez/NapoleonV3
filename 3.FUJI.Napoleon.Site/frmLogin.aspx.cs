using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.Extensions;
using _3.FUJI.Napoleon.Site.Services;
using _3.FUJI.Napoleon.Site.Services.DataContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3.FUJI.Napoleon.Site
{
    public partial class frmLogin : System.Web.UI.Page
    {
        NapoleonService NapoleonDA = new NapoleonService();
        public static clsUsuario user = new clsUsuario();
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }

        public string URLFEED2
        {
            get
            {
                return ConfigurationManager.AppSettings["URLFEED2"];
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
                lblLogin.Text = "";
                //tbl_CAT_Usuarios _user = new tbl_CAT_Usuarios();
                LoginRequest _req = new LoginRequest();
                _req.password = Security.Encrypt(txtContrasenaUser.Text);
                _req.username = txtUsuarioUser.Text;
                if(txtUsuarioUser.Text.Contains("@"))
                {
                    string[] log = txtUsuarioUser.Text.Split('@');
                    if (log.Count() > 0)
                    {
                        _req.username = log[0];
                        _req.vchSitio = log[1];
                    }
                    else
                    {
                        _req.username = txtUsuarioUser.Text;
                        _req.vchSitio = "";
                    }
                }
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
                        user = access.CurrentUser;
                        Session["Token"] = access.CurrentUser.Token;
                        //Session["sucOID"] = access.CurrentUser.sucOID;
                        if (access.CurrentUser.bitSolicitarPass)
                        {
                            //Response.Redirect(URL + "/AgregarSitio.aspx", false);
                            limpiarCambioPass();
                            Session.Clear();
                            mdlChangePass.Show();
                        }
                        else
                        {
                            Response.Redirect(URL + "/Default.aspx", false);
                        }
                        lblLogin.Text = "Inicio de Sesion correcta";
                        lblLogin.ForeColor = System.Drawing.Color.DarkGreen;
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

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                //verificar que existe 
                //no 
                LoginResponse response = new LoginResponse();
                response = NapoleonDA.getNewPassword(txtRecPass.Text);
                if(response!= null)
                {
                    if(response.Success)
                    {
                        clsCorreo email = new clsCorreo();
                        email.asunto = "Recuperar contraseña";
                        email.correo = Security.Decrypt(ConfigurationManager.AppSettings["CorreoString"].ToString());
                        email.passwordCorreo = Security.Decrypt(ConfigurationManager.AppSettings["PassString"].ToString());
                        email.toEmail = txtRecPass.Text;
                        string formato = "";
                        try
                        {
                            formato = obtenerMachote();
                        }
                        catch(Exception eom)
                        {
                            formato = "";
                        }
                        string nuevaContraseña = DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + Guid.NewGuid().ToString().Substring(0,8);
                        formato = formato.Replace("URLFEED2", URLFEED2).Replace("USER**",response.CurrentUser.vchUsuario);
                        formato = formato.Replace("PASS**", nuevaContraseña);
                        clsMensaje responsePass = new clsMensaje();
                        responsePass = NapoleonDA.updatePassword(response.CurrentUser.intUsuarioID, Security.Encrypt(nuevaContraseña), true);
                        if (responsePass != null)
                        {
                            if (responsePass.valido)
                            {
                                lblMesaje.ForeColor = System.Drawing.Color.DarkGreen;
                                lblMesaje.Text = "Cambios correctos";
                            }
                            else
                            {
                                lblMesaje.Text = "Verificar: " + response.vchMensaje;
                            }
                        }
                        else
                        {
                            lblMesaje.Text = "Verificar la información";
                        }
                        email.htmlCorreo = formato;
                        try
                        {
                            if (enviarCorreo(email))
                            {
                                mdlRecPass.Hide();
                                lblLogin.ForeColor = System.Drawing.Color.DarkGreen;
                                lblLogin.Text = "Verificar el correo electrónico.";
                            }
                        }
                        catch (Exception enviar)
                        {
                            Log.EscribeLog("Error al enviar el correo: " + enviar.Message);
                        }
                    }
                    else
                    {
                        lblMesaje.Text = response.vchMensaje;
                    }
                }
                else
                {
                    lblMesaje.Text = "Favor de verificar la información.";
                }
            }
            catch(Exception ebE)
            {
                Log.EscribeLog("Existe un error en btnEnviar_Click de frmLogin:" + ebE.Message);
            }
        }

        protected void btnRecPass_Click(object sender, EventArgs e)
        {

        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensajePass.Text = "";
                if (Security.Encrypt(txtPassActual.Text) == user.vchPassword)
                {
                    if (Security.Encrypt(txtPassNueva.Text) != user.vchPassword)
                    {

                        if (Security.Encrypt(txtPassNueva.Text) == Security.Encrypt(txtPassNueva1.Text))
                        {
                            clsMensaje response = new clsMensaje();
                            response = NapoleonDA.updatePassword(user.intUsuarioID, Security.Encrypt(txtPassNueva.Text), false);
                            if (response != null)
                            {
                                if (response.valido)
                                {
                                    lblLogin.ForeColor = System.Drawing.Color.DarkGreen;
                                    lblLogin.Text = "Cambios correctos";
                                    mdlChangePass.Hide();
                                }
                                else
                                {
                                    lblMensajePass.Text = "Verificar: " + response.vchMensaje;
                                }
                            }
                            else
                            {
                                lblMensajePass.Text = "Verificar la información";
                            }
                        }
                        else
                        {
                            lblMensajePass.Text = "Las contraseñas no coindice.";
                        }
                    }
                    else
                    {
                        lblMensajePass.Text = "Capturar una contraseña diferente.";
                    }
                }
                else
                {
                    lblMensajePass.Text = "La contraseña actual no es correcta.";
                }
            }
            catch(Exception eChange)
            {
                Log.EscribeLog("Existe un error en btnChange_Click: " + eChange.Message);
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarCambioPass();
            }
            catch(Exception eSalir)
            {
                Log.EscribeLog("Existe un error en: btnSalir_Click: " + eSalir.Message);
            }
        }

        private void limpiarCambioPass()
        {
            try
            {
                txtPassActual.Text = "";
                txtPassNueva.Text = "";
                txtPassNueva1.Text = "";
            }
            catch(Exception elCP)
            {
                Log.EscribeLog("Existe un error en limpiarCambioPass: " + elCP.Message);
            }
        }

        private bool enviarCorreo(clsCorreo correo)
        {
            bool valido = false;
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(correo.correo);
                string[] lista_correos = correo.toEmail.Split(';');

                foreach (string destino in lista_correos)
                {
                    mail.To.Add(destino);
                }
                mail.Subject = correo.asunto;
                mail.IsBodyHtml = true;
                mail.Body = correo.htmlCorreo;
                try
                {
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Credentials = new System.Net.NetworkCredential(correo.correo, correo.passwordCorreo);

                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    valido = true;
                }
                catch (Exception except)
                {
                    valido = false;
                    Log.EscribeLog("Existe un error al intentar enviar el correo: " + except.Message);
                }
            }
            catch (Exception eeC)
            {
                Log.EscribeLog("Existe un error en enviarCorreo: " + eeC.Message);
                valido = false;
            }
            return valido;
        }

        private string obtenerMachote()
        {
            string texto = "";
            try
            {
                if (File.Exists(Server.MapPath("~/Content/formatoHTML.html")))
                {
                    texto = File.ReadAllText(Server.MapPath("~/Content/formatoHTML.html"));
                }
                else
                {
                    texto = "<table width='350px' style='FONT-SIZE:11px;font-family:Tahoma,Helvetica,sans-serif;padding:2px;background-color:#fdfffe;BORDER-RIGHT:#0c922e 3px solid;BORDER-TOP:#0c922e 3px solid;BORDER-LEFT:#0c922e 3px solid;BORDER-BOTTOM:#0c922e 3px solid'>" +
                            "<tbody>" +
                            "<tr><td colspan='2'><H1>FEED2CLOUD</H1></td></tr><tr><td colspan='2'><hr></td></tr><tr><td colspan='2' align='center' style='background-color:#fefefe'>Reestablecer <span class='il'>contraseña</span></td></tr>" +
                            "<tr><td colspan='2'><hr></td></tr>" +
                            "<tr><td colspan='2'>Para restablecer su cuenta, iniciar sesion con la siguiente contraseña temporal y cambiarla.</td></tr>" +
                            "<tr><td ><h2>Usuario:</h2></td><td>USER**</td></tr>" +
                            "<tr><td ><h2>Contraseña:</h2></td><td>PASS**</td></tr>" +
                            "<tr><td colspan='2'><center>INICIAR:</center></td></tr>" + 
                            "<tr><td colspan='2'><center><h3>URLFEED2</h3></center></td></tr>" +
                            "<tr><td align='center' colspan='2'><font color='#014615' size='3'>FUJIFILM MEXICO</font></td></tr>" +
                            "<tr><td colspan='2'>Este correo&nbsp; electronico&nbsp; es&nbsp; confidencial, esta&nbsp; legalmente&nbsp; protegido y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o&nbsp; tomar&nbsp; ninguna&nbsp; accion&nbsp; basada&nbsp; en este correo electronico o cualquier otra informacion incluida en el (incluyendo todos los documentos adjuntos). </td></tr>" +
                            "</tbody></table>";
                }
            }
            catch (Exception eoM)
            {
                texto = "<table width='350px' style='FONT-SIZE:11px;font-family:Tahoma,Helvetica,sans-serif;padding:2px;background-color:#fdfffe;BORDER-RIGHT:#0c922e 3px solid;BORDER-TOP:#0c922e 3px solid;BORDER-LEFT:#0c922e 3px solid;BORDER-BOTTOM:#0c922e 3px solid'>" +
                            "<tbody>" +
                            "<tr><td colspan='2'><H1>FEED2CLOUD</H1></td></tr><tr><td colspan='2'><hr></td></tr><tr><td colspan='2' align='center' style='background-color:#fefefe'>Reestablecer <span class='il'>contraseña</span></td></tr>" +
                            "<tr><td colspan='2'><hr></td></tr>" +
                            "<tr><td colspan='2'>Para restablecer su cuenta, iniciar sesion con la siguiente contraseña temporal y cambiarla.</td></tr>" +
                            "<tr><td ><h2>Usuario:</h2></td><td>USER**</td></tr>" +
                            "<tr><td ><h2>Contraseña:</h2></td><td>PASS**</td></tr>" +
                            "<tr><td colspan='2'><center>INICIAR:</center></td></tr>" +
                            "<tr><td colspan='2'><center><h3>URLFEED2</h3></center></td></tr>" +
                            "<tr><td align='center' colspan='2'><font color='#014615' size='3'>FUJIFILM MEXICO</font></td></tr>" +
                            "<tr><td colspan='2'>Este correo&nbsp; electronico&nbsp; es&nbsp; confidencial, esta&nbsp; legalmente&nbsp; protegido y/o puede contener informacion privilegiada. Si usted no es su destinatario o no es alguna persona autorizada por este para recibir sus correos electronicos, NO debera usted utilizar, copiar, revelar, o&nbsp; tomar&nbsp; ninguna&nbsp; accion&nbsp; basada&nbsp; en este correo electronico o cualquier otra informacion incluida en el (incluyendo todos los documentos adjuntos). </td></tr>" +
                            "</tbody></table>";
                Log.EscribeLog("Existe un error al obtener el machote del correo: " + eoM.Message);
            }
            return texto;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                mdlRecPass.Hide();
                txtRecPass.Text = "";
            }
            catch(Exception eCerrar)
            {
                Log.EscribeLog("Existe un error en btnClose_Click: " + eCerrar);
            }
        }
    }
}