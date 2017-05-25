using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
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
    public partial class frmConfiguraciones : System.Web.UI.Page
    {
        NapoleonService NapoleonDA = new NapoleonService();
        public static List<clsUsuario> _lstUsuario = new List<clsUsuario>();
        public static List<tbl_ConfigSitio> _lstSitioG = new List<tbl_ConfigSitio>();
        public static List<tbl_CAT_Proyecto> _lstProyecto = new List<tbl_CAT_Proyecto>();
        public static List<clsConfigSitio> _lstSitesG = new List<clsConfigSitio>();

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
                hflURL.Value = URL;
                if (Session["UserID"] != null && Session["UserID"].ToString() != "" &&
               Security.ValidateToken(Session["Token"].ToString(), Session["intUsuarioID"].ToString(), Session["UserID"].ToString(), Session["Password"].ToString()))
                {
                    if (!IsPostBack)
                    {
                        Session["intProyectoID"] = 0;
                        if (Session["intTipoUsuario"].ToString() == "1")
                        {
                            cargaSitios();
                            cargaProyectos();
                            cargaSitiosP();
                            inicializaCombos();
                            cargaGridUsuarios();
                            //cargaGridPrioridad();
                            //lblMensajeConf.Text = "";
                            divUser.Visible = true;
                            divAddUser.Visible = true;
                        }
                        else
                        {
                            lblMensajeConf.Text = "No cuenta con las facultades para modificar las configuraciones.";
                            divUser.Visible = false;
                            divAddUser.Visible = false;
                        }
                    }
                }
                else
                {
                    Response.Redirect(URL + "/frmLogin.aspx", false);
                }
            }
            catch (Exception ePL)
            {
                Log.EscribeLog("Existe un error en PageLoad de frmConfiguraciones: " + ePL.Message);
                throw ePL;
            }
        }

        private void inicializaCombos()
        {
            try
            {
                cargaproyectosCat();
                cargaTipoUserCat();
            }
            catch (Exception eIC)
            {
                Log.EscribeLog("Existe un error al inicializar los combos: " + eIC.Message);
                throw eIC;
            }
        }

        private void cargaGridUsuarios()
        {
            try
            {
                grvBusqueda.DataSource = null;
                List<clsUsuario> _lst = new List<clsUsuario>();
                _lst = NapoleonDA.getUsuarios();
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        _lstUsuario = _lst.Where(x => x.vchUsuario.ToUpper().Contains(txtBusqueda.Text.ToUpper()) || x.vchNombre.ToUpper().Contains(txtBusqueda.Text.ToUpper()) || x.vchProyectoID.ToUpper().Contains(txtBusqueda.Text.ToUpper())).ToList();
                        grvBusqueda.DataSource = _lstUsuario;
                    }
                }
                grvBusqueda.DataBind();
            }
            catch (Exception ecU)
            {
                throw ecU;
            }
        }

        private void cargaSitios()
        {
            try
            {
                grvSites.DataSource = null;
                List<tbl_ConfigSitio> _lstSitio = new List<tbl_ConfigSitio>();
                _lstSitio = NapoleonDA.getSitios();
                if(_lstSitio!= null)
                {
                    if (_lstSitio.Count > 0)
                    {
                        _lstSitio = _lstSitio.Where(x => x.vchClaveSitio.ToUpper().Contains(txtBusquedaSite.Text.ToString()) || x.vchnombreSitio.ToUpper().Contains(txtBusquedaSite.Text.ToString())).ToList();
                        _lstSitioG = _lstSitio;
                        grvSites.DataSource = _lstSitio;
                    }
                }
                grvSites.DataBind();
            }
            catch(Exception ecs)
            {
                Log.EscribeLog("Existe un error en cargarSitios: " + ecs.Message);
            }
        }

        private void cargaproyectosCat()
        {
            try
            {
                ddlProyecto.DataSource = null;
                List<tbl_ConfigSitio> _lstSitio = new List<tbl_ConfigSitio>();
                _lstSitio = NapoleonDA.getSitios();
                if (_lstSitio != null)
                {
                    if (_lstSitio.Count > 0)
                    {
                        //_lstSitio = _lstSitio.Where(x => x.vchClaveSitio.ToUpper().Contains(txtBusquedaSite.Text.ToString()) || x.vchnombreSitio.ToUpper().Contains(txtBusquedaSite.Text.ToString())).ToList();
                        ddlProyecto.DataSource = _lstSitio;
                        ddlProyecto.DataTextField = "vchClaveSitio";
                        ddlProyecto.DataValueField = "id_Sitio";
                    }
                }
                ddlProyecto.DataBind();
            }
            catch (Exception ecs)
            {
                Log.EscribeLog("Existe un error en cargarSitios: " + ecs.Message);
            }
        }

        private void cargaTipoUserCat()
        {
            try
            {
                ddlTipoUsuario.DataSource = null;
                List<tbl_CAT_TipoUsuario> _lstSitio = new List<tbl_CAT_TipoUsuario>();
                _lstSitio = NapoleonDA.getTipoUsuario();
                if (_lstSitio != null)
                {
                    if (_lstSitio.Count > 0)
                    {
                        //_lstSitio = _lstSitio.Where(x => x.vchClaveSitio.ToUpper().Contains(txtBusquedaSite.Text.ToString()) || x.vchnombreSitio.ToUpper().Contains(txtBusquedaSite.Text.ToString())).ToList();
                        ddlTipoUsuario.DataSource = _lstSitio;
                        ddlTipoUsuario.DataTextField = "vchTipoUsuario";
                        ddlTipoUsuario.DataValueField = "intTipoUsuarioID";
                    }
                }
                ddlTipoUsuario.DataBind();
            }
            catch (Exception ecs)
            {
                Log.EscribeLog("Existe un error en cargarSitios: " + ecs.Message);
            }
        }

        private void cargaSitiosP()
        {
            try
            {
                gridSitiosProyec.DataSource = null;
                List<tbl_ConfigSitio> _lstSitio = new List<tbl_ConfigSitio>();
                List<clsConfigSitio> _lstSites = new List<clsConfigSitio>();
                _lstSitio = NapoleonDA.getSitios();
                
                if (_lstSitio != null)
                {
                    if (_lstSitio.Count > 0)
                    {
                        foreach(tbl_ConfigSitio item in _lstSitio)
                        {
                            clsConfigSitio mdlConfig = new clsConfigSitio();
                            mdlConfig.id_Sitio = item.id_Sitio;
                            mdlConfig.vchClaveSitio = item.vchClaveSitio;
                            mdlConfig.bitActivo = (bool)item.bitActivo;

                            mdlConfig.bitSeleccionado = false;
                            mdlConfig.datFechaSistema = (DateTime)item.datFechaSistema;
                            mdlConfig.intPuertoCliente = (int)item.intPuertoCliente;
                            mdlConfig.in_tPuertoServer = (int)item.in_tPuertoServer;
                            mdlConfig.vchAETitle = item.vchAETitle;
                            mdlConfig.vchAETitleServer = item.vchAETitleServer;
                            mdlConfig.vchIPCliente = item.vchIPCliente;
                            mdlConfig.vchIPServidor = item.vchIPServidor;
                            mdlConfig.vchMaskCliente = item.vchMaskCliente;
                            mdlConfig.vchnombreSitio = item.vchnombreSitio;
                            mdlConfig.vchPathLocal = item.vchPathLocal;
                            mdlConfig.vchUserAdmin = item.vchUserAdmin;
                            _lstSites.Add(mdlConfig);
                        }
                        //_lstSitio = _lstSitio.Where(x => x.vchClaveSitio.ToUpper().Contains(txtBusquedaSite.Text.ToString()) || x.vchnombreSitio.ToUpper().Contains(txtBusquedaSite.Text.ToString())).ToList();
                        _lstSitesG = _lstSites;
                        gridSitiosProyec.DataSource = _lstSites;
                    }
                }
                gridSitiosProyec.DataBind();
            }
            catch (Exception ecs)
            {
                Log.EscribeLog("Existe un error en cargarSitios: " + ecs.Message);
            }
        }

        private void cargaProyectos()
        {
            try
            {
                grvProyectos.DataSource = null;
                List<tbl_CAT_Proyecto> _lstProy = new List<tbl_CAT_Proyecto>();
                _lstProy = NapoleonDA.getProyectos();
                if (_lstProy != null)
                {
                    if (_lstProy.Count > 0)
                    {
                        _lstProy = _lstProy.Where(x => x.vchProyectoDesc.ToUpper().Contains(txtProeyct.Text.ToString())).ToList();
                        _lstProyecto = _lstProy;
                        grvProyectos.DataSource = _lstProy;
                    }
                }
                grvProyectos.DataBind();
            }
            catch (Exception ecs)
            {
                Log.EscribeLog("Existe un error en cargaProyectos: " + ecs.Message);
            }
        }

        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {

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
                    clsUsuario _mdl = (clsUsuario)e.Row.DataItem;
                    ImageButton ibtEstatus = (ImageButton)e.Row.FindControl("imbEstatus");
                    ibtEstatus.Attributes.Add("onclick", "javascript:return confirm('¿Desea realizar el cambio de estatus del Usuario seleccionado?');");
                    if ((bool)_mdl.bitActivo)
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_tick.png";
                    else
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_cancel.png";
                }
            }
            catch(Exception eGUP)
            {
                throw eGUP;
            }
        }

        protected void grvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvBusqueda.PageIndex = e.NewPageIndex;
                    cargaGridUsuarios();
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
                int intUsuarioID = 0;
                clsUsuario mdlUsuario = new clsUsuario();
                switch (e.CommandName)
                {
                    case "Estatus":
                        intUsuarioID = Convert.ToInt32(e.CommandArgument.ToString());
                        mdlUsuario = _lstUsuario.First(x => x.intUsuarioID == intUsuarioID);
                        bool actualiza = !mdlUsuario.bitActivo;
                        string mensaje = "";
                        bool success = NapoleonDA.updateEstatusUsuario(intUsuarioID, actualiza, ref mensaje);
                        if(success)
                        {
                            cargaGridUsuarios();
                            ShowMessage("Cambios correctos", MessageType.Success, "alert_containerSites");
                        }
                        else
                        {
                            ShowMessage("Existe un error al actualizar el estatus: " + mensaje, MessageType.Error, "alert_containerSites");
                        }
                        break;
                    case "viewEditar":
                        intUsuarioID = Convert.ToInt32(e.CommandArgument.ToString());
                        mdlUsuario = _lstUsuario.First(x => x.intUsuarioID  == intUsuarioID);
                        fillUsuario(mdlUsuario);
                        break;
                }
            }
            catch (Exception eRU)
            {
                Log.EscribeLog("Existe un error en grvBusqueda_RowCommand: " + eRU.Message);
            }
        }

        private void fillUsuario(clsUsuario mdlUsuario)
        {
           try
            {
                txtNombre.Text = mdlUsuario.vchNombre;
                txtApePat.Text = mdlUsuario.vchApellido;
                ddlTipoUsuario.SelectedValue = mdlUsuario.intTipoUsuarioID.ToString();
                ddlProyecto.SelectedValue = mdlUsuario.intProyectoID.ToString();
                txtUsuario.Text = mdlUsuario.vchUsuario;
                txtPassword1.Text = Security.Decrypt(mdlUsuario.vchPassword);
                Session["intUsuarioIDGrid"] = mdlUsuario.intUsuarioID;

            }
            catch(Exception eFU)
            {
                Log.EscribeLog("Existe un error en fillusuario: " + eFU.Message);
            }
        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {

        }

        protected void ddlSitioPriridad_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grvPrioridad_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grvPrioridad_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grvPrioridad_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void chkRow_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void ddlBandejaP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtBandejaP_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtBusquedaSite_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargaSitios();
            }
            catch(Exception eTSite)
            {
                Log.EscribeLog("Error al buscar sitio: " + eTSite.Message);
            }
        }

        protected void btnBusquedaSite_Click(object sender, EventArgs e)
        {
            try
            {
                cargaSitios();
            }
            catch(Exception ebBS)
            {
                ShowMessage("Existe un error al realizar la búsqueda: " + ebBS.Message,MessageType.Error, "alert_containerSites");
                Log.EscribeLog("Existe un error al realizar la búsqueda: " + ebBS.Message);
            }
        }

        protected void grvSites_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotalSitio");
                    lblTotalNumDePaginas.Text = grvBusqueda.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaSitio");
                    txtIrAlaPagina.Text = (grvBusqueda.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaSitio");
                    ddlTamPagina.SelectedValue = grvBusqueda.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    tbl_ConfigSitio _mdl = (tbl_ConfigSitio)e.Row.DataItem;
                    ImageButton ibtEstatus = (ImageButton)e.Row.FindControl("imbEstatus");
                    //ImageButton btnVisualizar = (ImageButton)e.Row.FindControl("btnVisualizar"); 
                    ibtEstatus.Attributes.Add("onclick", "javascript:return confirm('¿Desea realizar el cambio de estatus del Sitio seleccionado?');");
                    //btnVisualizar.Attributes.Add("onclick", "javascript:return alert('¿Visualizar los detalles del Sitio "+ _mdl.vchClaveSitio + "?');");
                    if ((bool)_mdl.bitActivo)
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_tick.png";
                    else
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_cancel.png";
                }
            }
            catch(Exception eSB)
            {
                throw eSB;
            }
        }

        protected void grvSites_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvSites.PageIndex = e.NewPageIndex;
                    cargaSitios();
                }
            }
            catch(Exception eSitesPC)
            {
                Log.EscribeLog("Existe un error en grvSites_PageIndexChanged: " + eSitesPC.Message);
                ShowMessage("Existe un error: " + eSitesPC.Message, MessageType.Error, "alert_containerSites");
            }
        }

        protected void grvSites_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id_Sitio = 0;
                tbl_ConfigSitio _mdlSitio;
                switch (e.CommandName)
                {
                    case "viewEditar":
                        id_Sitio = Convert.ToInt32(e.CommandArgument.ToString());
                        _mdlSitio = _lstSitioG.First(x => x.id_Sitio == id_Sitio);
                        fillSitioModal(_mdlSitio);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        upModal.Update();
                        break;
                    case "Estatus":
                        id_Sitio = Convert.ToInt32(e.CommandArgument.ToString());
                        _mdlSitio = _lstSitioG.First(x => x.id_Sitio == id_Sitio);
                        _mdlSitio.bitActivo = !_mdlSitio.bitActivo;
                        string mensaje = "";
                        bool valido = NapoleonDA.updateEstatusSitio(id_Sitio, (bool)_mdlSitio.bitActivo, ref mensaje);
                        if(valido)
                        {
                            cargaSitios();
                            cargaSitiosP();
                            ShowMessage("Cambios correctos", MessageType.Success, "alert_containerSites");
                        }
                        else
                        {
                            ShowMessage("Existe un error al actualizar el estatus: " + mensaje, MessageType.Error, "alert_containerSites");
                        }
                        break;
                }
               
            }
            catch(Exception eSRC)
            {
                ShowMessage("Existe un error al abrir el sitio: " + eSRC.Message, MessageType.Error, "alert_containerSites");
            }
        }

        private void fillSitioModal(tbl_ConfigSitio _mdlSitio)
        {
            try
            {
                txtClaveSit.Text = _mdlSitio.vchClaveSitio;
                lblIDSitio.Text = _mdlSitio.id_Sitio.ToString();
                txtNomSite.Text = _mdlSitio.vchnombreSitio;
                txtAETitle.Text = _mdlSitio.vchAETitle;
                txtIPSite.Text = _mdlSitio.vchIPCliente;
                txtMaskSite.Text = _mdlSitio.vchMaskCliente;
                txtPortSite.Text = _mdlSitio.intPuertoCliente.ToString();
                txtPath.Text = _mdlSitio.vchPathLocal;
                txtIPServer.Text = _mdlSitio.vchIPServidor;
                txtPortServer.Text = _mdlSitio.in_tPuertoServer.ToString();
                txtAETitleServer.Text = _mdlSitio.vchAETitleServer;
            }
            catch (Exception eFSM)
            {
                Log.EscribeLog("Existe un error en FillSitioModal: " + eFSM.Message);
                throw eFSM;
            }
        }

        protected void ddlBandejaSitio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtBandejaSitio_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtProeyct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargaProyectos();
            }
            catch(Exception eP)
            {
                Log.EscribeLog("Existe un error en txtProeyct_TextChanged: " + eP.Message);
                throw eP;
            }
        }

        protected void btnProyect_Click(object sender, EventArgs e)
        {
            try
            {
                cargaProyectos();
            }
            catch (Exception eP)
            {
                Log.EscribeLog("Existe un error en btnProyect_Click: " + eP.Message);
                ShowMessage("Existe un error al realizar la búsqueda: " + eP.Message, MessageType.Error, "alert_containerSites");
            }
        }

        protected void Unnamed_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grvProyectos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotalP");
                    lblTotalNumDePaginas.Text = grvBusqueda.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaProyec");
                    txtIrAlaPagina.Text = (grvBusqueda.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaProy");
                    ddlTamPagina.SelectedValue = grvBusqueda.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    tbl_CAT_Proyecto _mdl = (tbl_CAT_Proyecto)e.Row.DataItem;
                    ImageButton ibtEstatus = (ImageButton)e.Row.FindControl("imbEstatus");
                    //ImageButton btnVisualizar = (ImageButton)e.Row.FindControl("btnVisualizar");
                    ibtEstatus.Attributes.Add("onclick", "javascript:return confirm('¿Desea realizar el cambio de estatus del Proyecto seleccionado?');");
                    //btnVisualizar.Attributes.Add("onclick", "javascript:return alert('¿Desea realizar la edición del Proyecto seleccionado?');");
                    if ((bool)_mdl.bitActivo)
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_tick.png";
                    else
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_cancel.png";
                }
            }
            catch(Exception eP)
            {
                throw eP;
            }
        }

        protected void grvProyectos_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvProyectos.PageIndex = e.NewPageIndex;
                    cargaProyectos();
                }
            }
            catch (Exception eSitesPC)
            {
                Log.EscribeLog("Existe un error en grvProyectos_PageIndexChanged: " + eSitesPC.Message);
                ShowMessage("Existe un error: " + eSitesPC.Message, MessageType.Error, "alert_containerSites");
            }
        }

        protected void grvProyectos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int intProyectoID = 0;
                tbl_CAT_Proyecto _mdlProyecto;
                switch (e.CommandName)
                {
                    case "viewEditar":
                        intProyectoID = Convert.ToInt32(e.CommandArgument.ToString());
                        _mdlProyecto = _lstProyecto.First(x => x.intProyectoID == intProyectoID);
                        fillProyectoModal(_mdlProyecto);
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                        //upModal.Update();
                        break;
                    case "Estatus":
                        intProyectoID = Convert.ToInt32(e.CommandArgument.ToString());
                        _mdlProyecto = _lstProyecto.First(x => x.intProyectoID == intProyectoID);
                        bool activo = (bool)!_mdlProyecto.bitActivo;
                        string mensaje = "";
                        bool valido = NapoleonDA.updateEstatusProyectos(intProyectoID, activo, ref mensaje);
                        if(valido)
                        {
                            cargaProyectos();
                            cargaproyectosCat();
                            ShowMessage("Cambios correctos", MessageType.Success, "alert_container");
                        }
                        else
                        {
                            ShowMessage("Existe un error al actializar: " + mensaje, MessageType.Error, "alert_container");
                        }
                        break;
                }
            }
            catch(Exception eRCP)
            {
                Log.EscribeLog("Existe un error en grvProyectos_RowCommand:" + eRCP.Message);
                throw eRCP;
            }
        }

        private void fillProyectoModal(tbl_CAT_Proyecto _mdlProyecto)
        {
            try
            {
                txtNombreProyect.Text = _mdlProyecto.vchProyectoDesc;
                Session["intProyectoID"] = _mdlProyecto.intProyectoID;
                List<tbl_REL_ProyectoSitio> _lstRel = new List<tbl_REL_ProyectoSitio>();
                _lstRel = NapoleonDA.getRELProyecto_Sitio(_mdlProyecto.intProyectoID);
                foreach(clsConfigSitio item in _lstSitesG)
                {
                    foreach(tbl_REL_ProyectoSitio Sites in _lstRel)
                    {
                        if(Sites.id_Sitio == item.id_Sitio)
                        {
                            item.bitSeleccionado = true;
                        }
                    }
                }
                gridSitiosProyec.DataSource = _lstSitesG;
                gridSitiosProyec.DataBind();
            }
            catch(Exception efMP)
            {
                Log.EscribeLog("Existe un error en fillProyectoModal: " + efMP.Message);
                throw efMP;
            }
        }

        protected void gridSitiosProyec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvBusqueda.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaPS");
                    txtIrAlaPagina.Text = (grvBusqueda.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaPS");
                    ddlTamPagina.SelectedValue = grvBusqueda.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    clsConfigSitio _mdl = (clsConfigSitio)e.Row.DataItem;
                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                    //ibtEstatus.Attributes.Add("onclick", "javascript:return confirm('¿Desea realizar el cambio de estatus del Proyecto seleccionado?');");
                    chkSelect.Checked = (bool)_mdl.bitSeleccionado;
                }
            }
            catch (Exception eP)
            {
                throw eP;
            }
        }

        protected void gridSitiosProyec_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.gridSitiosProyec.PageIndex = e.NewPageIndex;
                    cargaGridSitiosProyec();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_container");
            }
        }

        private void cargaGridSitiosProyec()
        {
            try
            {
                gridSitiosProyec.DataSource = _lstSitesG;
                gridSitiosProyec.DataBind();
            }
            catch(Exception ecGS)
            {
                Log.EscribeLog("Existe un error al cargarGrigSitiosProyec: " + ecGS.Message);
            }
        }

        protected void gridSitiosProyec_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
               
            }
            catch(Exception eSRC)
            {
                ShowMessage("Existe un error al cargar el sitio: " + eSRC.Message,MessageType.Error, "alert_containerSites");
            }
        }

        protected void ddlBandejaPS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtBandejaPS_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlBandejaProy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtBandejaProyec_TextChanged(object sender, EventArgs e)
        {

        }

        public enum MessageType { Success, Error, Info, Warning };

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

        protected void btnAddProyecto_Click(object sender, EventArgs e)
        {
            try
            {
                tbl_CAT_Proyecto mdlProyect = new tbl_CAT_Proyecto();
                ProyectoResponse response = new ProyectoResponse();
                ProyectoRequest request = new ProyectoRequest();
                //edicion
                if (Convert.ToInt32(Session["intProyectoID"].ToString()) > 0)
                {
                    if (validarPS())
                    {
                        mdlProyect.intProyectoID = Convert.ToInt32(Session["intProyectoID"].ToString());
                        request.mdlProyecto = mdlProyect;
                        List<clsConfigSitio> list = new List<clsConfigSitio>();
                        list = obtenerSitiosEdicion();
                        request.lstSites = list;
                        response = NapoleonDA.updateProyecto(request);
                        if (response != null)
                        {
                            if (response.success)
                            {
                                cargaProyectos();
                                ShowMessage("Cambios correctos.", MessageType.Success, "alert_containerSites");
                            }
                            else
                            {
                                ShowMessage(response.mensaje, MessageType.Error, "alert_containerSites");
                            }
                        }
                        else
                        {
                            ShowMessage("Existe un error, favor de verificar", MessageType.Error, "alert_containerSites");
                        }
                    }
                    else
                    {
                        ShowMessage("Se debe seleccionar al menos un sitio por proyecto.", MessageType.Info, "alert_containerSites");
                    }
                }//Nuevo
                else
                {
                    if (validarPS())
                    {
                        List<tbl_REL_ProyectoSitio> _lstSitios = new List<tbl_REL_ProyectoSitio>();
                        _lstSitios = obtenerSitios();
                        mdlProyect = obtenerProyecto();

                        request.mdlProyecto = mdlProyect;
                        request.lstSitos = _lstSitios;

                        response = NapoleonDA.setProyecto(request);
                        if (response != null)
                        {
                            if (response.success)
                            {
                                cargaProyectos();
                                ShowMessage("Cambios correctos.", MessageType.Success, "alert_containerSites");
                            }
                            else
                            {
                                ShowMessage(response.mensaje, MessageType.Error, "alert_containerSites");
                            }
                        }
                        else
                        {
                            ShowMessage("Existe un error, favor de verificar", MessageType.Error, "alert_containerSites");
                        }
                    }
                    else
                    {
                        ShowMessage("Se debe seleccionar al menos un sitio por proyecto.", MessageType.Info, "alert_containerSites");
                    }
                }
            }
            catch (Exception ebAP)
            {
                ShowMessage("Existe un error al agregar el proyecto: " + ebAP.Message, MessageType.Error, "alert_containerSites");
            }
        }

        private List<tbl_REL_ProyectoSitio> obtenerSitios()
        {
            List<tbl_REL_ProyectoSitio> _lst = new List<tbl_REL_ProyectoSitio>();
            try
            {
                foreach (GridViewRow row in gridSitiosProyec.Rows)
                {
                    tbl_REL_ProyectoSitio mdl = new tbl_REL_ProyectoSitio();
                    CheckBox check = row.FindControl("chkSelect") as CheckBox;
                    if (check.Checked)
                    {
                        mdl.id_Sitio = Convert.ToInt32(gridSitiosProyec.DataKeys[row.RowIndex]["id_Sitio"]);
                        _lst.Add(mdl);
                    }
                }
            }
            catch (Exception eOS)
            {

            }
            return _lst;
        }

        private List<clsConfigSitio> obtenerSitiosEdicion()
        {
            List<clsConfigSitio> _lst = new List<clsConfigSitio>();
            try
            {
                foreach (GridViewRow row in gridSitiosProyec.Rows)
                {
                    clsConfigSitio mdl = new clsConfigSitio();
                    CheckBox check = row.FindControl("chkSelect") as CheckBox;
                    mdl.id_Sitio = Convert.ToInt32(gridSitiosProyec.DataKeys[row.RowIndex]["id_Sitio"]);
                    mdl.bitSeleccionado = check.Checked;
                    _lst.Add(mdl);
                }
            }
            catch (Exception eOS)
            {

            }
            return _lst;
        }

        private tbl_CAT_Proyecto obtenerProyecto()
        {
            tbl_CAT_Proyecto mdl = new tbl_CAT_Proyecto();
            try
            {
                mdl.vchProyectoDesc = txtNombreProyect.Text;
                mdl.vchUserAdmin = Session["UserID"] != null ? Session["UserID"].ToString() : "";
                mdl.datFecha = DateTime.Now;
                mdl.bitActivo = true;
            }
            catch(Exception eOP)
            {
                Log.EscribeLog("Existe un error en Obtener Proyecto: " + eOP.Message);
                throw eOP;
            }
            return mdl;
        }

        private bool validarPS()
        {
            bool valido = false;
            try
            {
                foreach (GridViewRow row in gridSitiosProyec.Rows)
                {
                    CheckBox check = row.FindControl("chkSelect") as CheckBox;
                    if (check.Checked)
                    {
                        valido = true;
                    }
                }
            }
            catch(Exception eVS)
            {
                valido = false;
                Log.EscribeLog("Error al validar Proyecto: " + eVS.Message);
                throw eVS;
            }
            return valido;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                txtNombreProyect.Text = "";
                Session["intProyectoID"] = 0;
                cargaSitiosP();
            }
            catch(Exception eBL)
            {
                Log.EscribeLog("Existe un error en cancelar proyecto: " + eBL.Message);
                ShowMessage("Existe un error al cancelar: " + eBL.Message, MessageType.Error, "alert_containerSites");
            }
        }
    }
}