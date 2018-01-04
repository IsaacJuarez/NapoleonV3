using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using _2.FUJI.Napoleon.AccesoDatos.Extensions;
using _3.FUJI.Napoleon.Site.Services;
using _3.FUJI.Napoleon.Site.Services.DataContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;

namespace _3.FUJI.Napoleon.Site
{
    public partial class frmVersionFeed2 : System.Web.UI.Page
    {
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }
        NapoleonService NapoleonDA = new NapoleonService();
        public static List<tbl_CAT_Feed2Version> lstCATFeed2 = new List<tbl_CAT_Feed2Version>();

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
                        cargarArchivos();
                        enableDIV();
                    }
                }
                else
                {
                    Response.Redirect(URL + "/frmLogin.aspx", false);
                }
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en Page_Load: " + ePL.Message);
            }
        }

        private void enableDIV()
        {
            try
            {
                if(user.intTipoUsuarioID == 1)
                {
                    divVersion.Visible = true;
                }
                else
                {
                    divVersion.Visible = false;
                }
            }
            catch(Exception eED)
            {

            }
        }

        private void cargarArchivos()
        {
            try
            {
                grvFiles.DataSource = null;
                lstCATFeed2 = null;
                List<tbl_CAT_Feed2Version> list = new List<tbl_CAT_Feed2Version>();
                list = NapoleonDA.getListaArchivos();
                if(list!= null)
                {
                    if(list.Count > 0)
                    {
                        lstCATFeed2 = list;
                        grvFiles.DataSource = list.OrderBy(x => x.datFecha).ToList();
                    }

                    grvFiles.DataBind();
                }
            }
            catch(Exception ecA)
            {
                Log.EscribeLog("Existe un error en cargarArchivos : " + ecA.Message);
            }
        }

        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuFile.HasFile)
                {
                    string FileName = Path.GetFileName(fuFile.FileName);
                    //using (Stream fs = fuFile.FileBytes)
                    //{
                    //    using (BinaryReader br = new BinaryReader(fs))
                    //    {
                    //byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byte[] bytes = fuFile.FileBytes;
                    clsMensaje response = new clsMensaje();
                    FileFeed2Request request = new FileFeed2Request();
                    tbl_CAT_Feed2Version file = new tbl_CAT_Feed2Version();
                    file.bitActivo = true;
                    file.datFecha = DateTime.Now;
                    file.vbdata = bytes;
                    file.vchComentario = txtComentarios.Text;
                    file.vchNombreArchivo = FileName;
                    file.vchVersion = txtVersion.Text;
                    request.file = file;
                    request.intUsuarioID = user.intUsuarioID.ToString();
                    request.vchPassword = user.vchPassword;
                    request.Token = user.Token;
                    request.vchUsuario = user.vchUsuario;
                    response = NapoleonDA.setFileVersion(request);
                    if (response != null)
                    {
                        if (response.valido)
                        {
                            cargarArchivos();
                            limpiarControles();
                            ShowMessage("El archivo se cargó correctamente.", MessageType.Correcto, "alert_container");
                        }
                        else
                        {
                            ShowMessage("Existe un error al guardar: " + response.vchMensaje, MessageType.Error, "alert_container");
                        }
                    }
                    else
                    {
                        ShowMessage("Favor de verificar la información.", MessageType.Advertencia, "alert_container");
                    }
                    //    }
                    //}
                }
                else
                {
                    ShowMessage("Favor de cargar un archivo.", MessageType.Advertencia, "alert_container");
                }
            }
            catch (Exception eaF)
            {
                ShowMessage("Existe un error al guardar el archivo: " + eaF.Message, MessageType.Error, "alert_container");
                Log.EscribeLog("Existe un error al guardar el archivo: " + eaF.Message);
            }
        }

        private void limpiarControles()
        {
            try
            {
                txtVersion.Text = "";
                txtComentarios.Text = "";
            }
            catch(Exception elC)
            {
                Log.EscribeLog("Existe un error en limpiarControles de frmVersionFeed2: " + elC.Message);
            }
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
                Log.EscribeLog("Existe un error en  ShowMessage: " + eSM.Message);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        protected void grvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvFiles.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandeja");
                    txtIrAlaPagina.Text = (grvFiles.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandeja");
                    ddlTamPagina.SelectedValue = grvFiles.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {

                    tbl_CAT_Feed2Version _mdl = new tbl_CAT_Feed2Version();
                    _mdl = e.Row.DataItem as tbl_CAT_Feed2Version;
                    ImageButton ibtEstatus = (ImageButton)e.Row.FindControl("imbEstatus");
                    //ImageButton btnVisualizar = (ImageButton)e.Row.FindControl("btnVisualizar"); 
                    ibtEstatus.Attributes.Add("onclick", "javascript:return confirm('¿Desea realizar el cambio de estatus del Sitio seleccionado?');");
                    if ((bool)_mdl.bitActivo)
                    {
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_tick.png";
                    }
                    else
                    {
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_cancel.png";
                    }
                    if (user.intTipoUsuarioID == 1)
                    {
                        ibtEstatus.Enabled = true;
                    }
                    else
                    {
                        ibtEstatus.Enabled = false;
                    }
                }
            }
            catch (Exception eROWDB)
            {
                Log.EscribeLog("Existe un error en grvFiles_RowDataBound: " + eROWDB.Message);
            }
        }

        protected void grvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intVersionID = 0;
                tbl_CAT_Feed2Version mdl = new tbl_CAT_Feed2Version();
                switch (e.CommandName)
                {
                    case "Descargar":
                        string urlabrir = "";
                        intVersionID = Convert.ToInt32(e.CommandArgument.ToString());
                        mdl = lstCATFeed2.First(x => x.intVersionID == intVersionID);
                        Session["File"] = mdl;
                        urlabrir = URL + "/frmDownLoad.aspx?ID=" + Security.Encrypt(mdl.intVersionID.ToString());
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Cerrar", "javascript:Redirecciona('" + urlabrir + "');", true);
                        break;
                    case "Estatus":
                        intVersionID = Convert.ToInt32(e.CommandArgument.ToString());
                        mdl = lstCATFeed2.First(x => x.intVersionID == intVersionID);
                        mdl.bitActivo = !mdl.bitActivo;
                        clsMensaje response = new clsMensaje();
                        response = NapoleonDA.updateEstatusFiles(intVersionID, (bool)mdl.bitActivo);
                        if (response.valido)
                        {
                            cargarArchivos();
                            ShowMessage("Cambios correctos", MessageType.Correcto, "alert_containerSites");
                        }
                        else
                        {
                            ShowMessage("Existe un error al actualizar el estatus: " + response.vchMensaje, MessageType.Error, "alert_containerSites");
                        }
                        break;
                }
            }
            catch (Exception eCommand)
            {
                Log.EscribeLog("Existe un error en grvFiles_RowCommand: " + eCommand.Message);
            }
        }

        //private string GetMimeType(string fileName)
        //{
        //    string mimeType = "application/unknown";
        //    string ext = System.IO.Path.GetExtension(fileName).ToLower();
        //    Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        //    if (regKey != null && regKey.GetValue("Content Type") != null)
        //        mimeType = regKey.GetValue("Content Type").ToString();
        //    return mimeType;
        //}

        
        protected void grvFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvFiles.PageIndex = e.NewPageIndex;
                    cargarArchivos();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_container");
            }
        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvFiles.AllowPaging = true;
                    this.grvFiles.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvFiles.AllowPaging = false;
                this.cargarArchivos();
            }
            catch (Exception eddS)
            {
                ShowMessage("Existe un error: " + eddS.Message, MessageType.Error, "alert_container");
            }
        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvFiles.PageIndex = numeroPagina - 1;
                else
                    this.grvFiles.PageIndex = 0;
                this.cargarArchivos();
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_container");
            }
        }

        protected void btnPreRequisitos_Click(object sender, EventArgs e)
        {
            try
            {
                string urlabrir = "";
                urlabrir = URL + "/frmManual.aspx?ID=" + Security.Encrypt("2");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Cerrar", "javascript:Redirecciona('" + urlabrir + "');", true);
            }
            catch(Exception ePre)
            {
                Log.EscribeLog("Existe un error en btnPreRequisitos_Click: " + ePre.Message);
            }
        }

        protected void btnFeed2_Click(object sender, EventArgs e)
        {
            try
            {
                string urlabrir = "";
                urlabrir = URL + "/frmManual.aspx?ID=" + Security.Encrypt("1");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Cerrar", "javascript:Redirecciona('" + urlabrir + "');", true);
            }
            catch (Exception ePre)
            {
                Log.EscribeLog("Existe un error en btnPreRequisitos_Click: " + ePre.Message);
            }
        }
    }
}