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
    public partial class frmConfigSites : System.Web.UI.Page
    {
        NapoleonService NapoleonDA = new NapoleonService();
        public static int intTipoUsuario = 0;
        public static clsUsuario user = new clsUsuario();
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
                if (Session["UserID"] != null && Session["UserID"].ToString() != "" && Session["tbl_CAT_Usuarios"] != null &&
                Security.ValidateToken(Session["Token"].ToString(), Session["intUsuarioID"].ToString(), Session["UserID"].ToString(), Session["Password"].ToString()))
                {
                    if (!IsPostBack)
                    {
                        intTipoUsuario = Convert.ToInt32(Session["intTipoUsuario"].ToString());
                        user = (clsUsuario)Session["tbl_CAT_Usuarios"];
                        cargaSitiosConfig();
                        //inicializaCombos();
                    }
                }
                else
                {
                    Response.Redirect(URL + "/frmLogin.aspx");
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openPopover();", true);
            }
            catch (Exception ePL)
            {
                ShowMessage("Existe un error: " + ePL.Message, MessageType.Error, "alert_container");
            }
        }

        private void cargaSitiosConfig()
        {
            try
            {
                List<tbl_ConfigSitio> _lstGrid = new List<tbl_ConfigSitio>();
                if (intTipoUsuario == 1)
                {
                    user.intProyectoID = 0;
                    user.id_Sitio = 0;
                }
                else
                {
                    if (intTipoUsuario == 2)
                    {
                        user.id_Sitio = 0;
                    }
                    if (intTipoUsuario == 3)
                    {
                        user.intProyectoID = 0;
                    }
                }
                _lstGrid = NapoleonDA.getSitios(user.intProyectoID, user.id_Sitio);
                if (_lstGrid.Count > 0)
                {
                    if (txtBusqueda.Text != "")
                    {
                        _lstGrid = _lstGrid.Where(x => x.vchClaveSitio.Contains(txtBusqueda.Text)).ToList();
                    }
                    grvBusqueda.DataSource = _lstGrid;
                    grvBusqueda.DataBind();
                }
            }
            catch (Exception ecsc)
            {
                ShowMessage("Existe un error: " + ecsc.Message, MessageType.Error, "alert_container");
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
                throw eSM;
            }
        }

        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargaSitiosConfig();
            }
            catch (Exception etC)
            {
                ShowMessage("Existe un error al consultar la búsqueda. " + etC.Message, MessageType.Error, "alert_container");
            }
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                cargaSitiosConfig();
            }
            catch (Exception eBs)
            {
                ShowMessage("Existe un error al consultar la búsqueda. " + eBs.Message, MessageType.Error, "alert_container");
            }
        }

        protected void grvBusqueda_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvBusqueda.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandeja");
                    txtIrAlaPagina.Text = (grvBusqueda.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandeja");
                    ddlTamPagina.SelectedValue = grvBusqueda.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    //tbl_ConfigSitio _mdl = (tbl_ConfigSitio)e.Row.DataItem;
                    //ImageButton ibtEstatus = (ImageButton)e.Row.FindControl("imbEstatus");
                    //ibtEstatus.Attributes.Add("onclick", "javascript:return confirm('¿Desea realizar el cambio de estatus del Usuario seleccionado?');");
                    //if ((bool)_mdl.bitActivo)
                    //    ibtEstatus.ImageUrl = @"~/Images/ic_action_tick.png";
                    //else
                    //    ibtEstatus.ImageUrl = @"~/Images/ic_action_cancel.png";
                }
            }
            catch (Exception egrdb)
            {
                throw new Exception(egrdb.Message);
            }
        }

        protected void grvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvBusqueda.PageIndex = e.NewPageIndex;
                    cargaSitiosConfig();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_container");
            }
        }

        protected void grvBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id_Site = 0;
                tbl_RegistroSitio mdlUsuario = new tbl_RegistroSitio();
                switch (e.CommandName)
                {
                    case "viewEditar":
                        id_Site = Convert.ToInt32(e.CommandArgument.ToString());
                        tbl_RegistroSitio mdl = new tbl_RegistroSitio();
                        mdl = NapoleonDA.getRegistroContacto(id_Site);
                        if (mdl != null)
                        {
                            fillContacto(mdl);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                            upModal.Update();
                        }
                        break;
                    case "Configuracion":
                        id_Site = Convert.ToInt32(e.CommandArgument.ToString());
                        tbl_DET_Sitio mdlDetalle = new tbl_DET_Sitio();
                        mdlDetalle = NapoleonDA.getDetalleSitio(id_Site);
                        if (mdlDetalle != null)
                        {
                            Control ctl = e.CommandSource as Control;
                            GridViewRow CurrentRow = ctl.NamingContainer as GridViewRow;
                            Label1.Text = grvBusqueda.DataKeys[CurrentRow.RowIndex].Values["vchNombreSitio"].ToString(); //vchNombreSitio
                            lblIDSite.Text = id_Site.ToString();
                            fillDetallleSitio(mdlDetalle);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ConfigModal", "$('#ConfigModal').modal();", true);
                            upConfigModal.Update();
                        }
                        break;
                }
            }
            catch (Exception eRC)
            {
                Log.EscribeLog("Existe un error en grvBusqueda_RowCommand: " + eRC.Message);
            }
        }

        private void fillDetallleSitio(tbl_DET_Sitio mdlDetalle)
        {
            try
            {
                Label2.Text = mdlDetalle.intDETSitioID.ToString();
                txtSCULocal.Text = mdlDetalle.vchAETitleSCU;
                txtSCPVNA.Text = mdlDetalle.vchAETitleSCP;
                string[] ipformato = mdlDetalle.vchVNAIP.Split('.');
                txtIPVNA1.Text = ipformato[0].ToString();
                txtIPVNA2.Text = ipformato[1].ToString();
                txtIPVNA3.Text = ipformato[2].ToString();
                txtIPVNA4.Text = ipformato[3].ToString();
                txtPuertoVNA.Text = mdlDetalle.intPuertoSCP.ToString();
            }
            catch(Exception efDS)
            {
                Log.EscribeLog("Existe un error en frmConfigSites en fillDetallleSitio: " + efDS.Message);
            }
        }

        private void fillContacto(tbl_RegistroSitio mdl)
        {
            try
            {
                lblIDSitio.Text = mdl.id_Sitio == null ? "" : mdl.id_Sitio.ToString();
                txtNombreContacto.Text = mdl.vchNombreCliente == null ? "" : mdl.vchNombreCliente.ToString();
                txtEmailContacto.Text = mdl.vchEmail == null ? "" : mdl.vchEmail;
                txtTelefono.Text = mdl.vchNumeroContacto == null ? "" : mdl.vchNumeroContacto;
                txtVendedor.Text = mdl.vchVendedor == null ? "" : mdl.vchVendedor;
            }
            catch (Exception efC)
            {
                Log.EscribeLog("Existe un error en fillContacto: " + efC.Message);
            }
        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvBusqueda.AllowPaging = true;
                    this.grvBusqueda.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvBusqueda.AllowPaging = false;
                this.cargaSitiosConfig();
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
                    this.grvBusqueda.PageIndex = numeroPagina - 1;
                else
                    this.grvBusqueda.PageIndex = 0;
                this.cargaSitiosConfig();
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_container");
            }
        }

        protected void btnGuardarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                tbl_DET_Sitio mdlDetalle = new tbl_DET_Sitio();
                mdlDetalle = obtenerDetalle();
                if (mdlDetalle != null)
                {
                    bool valido = false;
                    valido = NapoleonDA.setDetalleSitioEdicion(mdlDetalle);
                    if (valido)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ConfigModal", "$('#ConfigModal').modal('hide');", true);
                        ShowMessage("Se actualizó correctamente los datos.", MessageType.Correcto, "alert_container");
                    }
                    else
                    {
                        ShowMessage("Existe un error, favor de verificar la información", MessageType.Error, "alert_container");
                    }
                }
            }
            catch (Exception eBCD)
            {
                Log.EscribeLog("Existe un error en btnGuardarDetalle_Click: " + eBCD.Message);
            }
        }

        private tbl_DET_Sitio obtenerDetalle()
        {
            tbl_DET_Sitio mdl = new tbl_DET_Sitio();
            try
            {
                mdl.bitActivo = true;
                mdl.datFecha = DateTime.Now;
                mdl.id_Sitio = Convert.ToInt32(lblIDSite.Text);
                mdl.intDETSitioID = Convert.ToInt32(Label2.Text);
                mdl.intPuertoSCP = Convert.ToInt32(txtPuertoVNA.Text);
                mdl.vchAETitleSCP = txtSCPVNA.Text;
                mdl.vchAETitleSCU = txtSCULocal.Text;
                mdl.vchUserAdmin = user.vchUsuario;
                mdl.vchVNAIP = txtIPVNA1.Text + "." + txtIPVNA2.Text + "." + txtIPVNA3.Text + "." + txtIPVNA4.Text;
            }
            catch (Exception eBCD)
            {
                mdl = null;
                Log.EscribeLog("Existe un error en obtenerDetalle: " + eBCD.Message);
            }
            return mdl;
        }

        protected void btnCancelDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarControlesDetalle();
            }
            catch(Exception eBCD)
            {
                Log.EscribeLog("Existe un error en btnCancelDetalle_Click: " + eBCD.Message);
            }
        }

        private void limpiarControlesDetalle()
        {
            try
            {
                Label2.Text = "";
                txtSCULocal.Text = "";
                txtSCPVNA.Text = "";
                txtIPVNA1.Text = "";
                txtIPVNA2.Text = "";
                txtIPVNA3.Text = "";
                txtIPVNA4.Text = "";
                txtPuertoVNA.Text = "";
            }
            catch (Exception eBCD)
            {
                Log.EscribeLog("Existe un error en limpiarControlesDetalle: " + eBCD.Message);
            }
        }
    }
}