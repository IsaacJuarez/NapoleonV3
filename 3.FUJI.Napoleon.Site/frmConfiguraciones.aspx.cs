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
        public static clsUsuario usuario =  new clsUsuario();
        public static List<clsUsuario> _lstUsuario = new List<clsUsuario>();
        public static List<tbl_ConfigSitio> _lstSitioG = new List<tbl_ConfigSitio>();
        public static List<tbl_CAT_Proyecto> _lstProyecto = new List<tbl_CAT_Proyecto>();
        public static List<clsConfigSitio> _lstSitesG = new List<clsConfigSitio>();
        public static List<clsPrioridadSucursal> _lstPriodidad = new List<clsPrioridadSucursal>();
        public static int intProyectoG = 0;
        public static int intTipoUsuario = 0;
        public static int SecMax = 0;

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
                if (Session["UserID"] != null && Session["UserID"].ToString() != "" && Session["tbl_CAT_Usuarios"] != null &&
               Security.ValidateToken(Session["Token"].ToString(), Session["intUsuarioID"].ToString(), Session["UserID"].ToString(), Session["Password"].ToString()))
                {
                    if (!IsPostBack)
                    {
                        try
                        {
                            Session["intProyectoID"] = 0;
                            Session["intUsuarioIDGrid"] = 0;
                            usuario = (clsUsuario)Session["tbl_CAT_Usuarios"];
                            intTipoUsuario = usuario.intTipoUsuarioID;
                            intProyectoG = usuario.intProyectoID == null ? 0 : usuario.intProyectoID;
                        }
                        catch(Exception eInicializadores)
                        {
                            Log.EscribeLog("Existe un error en Inicalizadores de PageLoad: " + eInicializadores.Message);
                        }
                        if (Session["intTipoUsuario"].ToString() == "1")
                        {
                            cargaSitios();
                            cargaProyectos();
                            cargaSitiosP();
                            cargaSitiosCat();
                            inicializaCombos();
                            cargaGridUsuarios();
                            
                            //lblMensajeConf.Text = "";
                            divUser.Visible = true;
                            divAddUser.Visible = true;
                            btnAddProyecto.Enabled = true;
                        }
                        else
                        {
                            lblMensajeConf.Text = "No cuenta con las facultades para modificar las configuraciones.";
                            divUser.Visible = false;
                            divAddUser.Visible = false;
                            btnAddProyecto.Enabled = false;
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
                Log.EscribeLog("Existe un error en cargaGridUsuarios: " + ecU.Message);
                throw ecU;
            }
        }

        private void cargaSitios()
        {
            try
            {
                grvSites.DataSource = null;
                List<tbl_ConfigSitio> _lstSitio = new List<tbl_ConfigSitio>();
                _lstSitio = NapoleonDA.getSitios(intProyectoG, 0);
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

        private void cargaSitiosCat()
        {
            try
            {
                ddlSitio.DataSource = null;
                List<tbl_ConfigSitio> _lstSitio = new List<tbl_ConfigSitio>();
                _lstSitio = NapoleonDA.getSitios(intProyectoG, 0);
                if (_lstSitio != null)
                {
                    if (_lstSitio.Count > 0)
                    {
                        _lstSitio = _lstSitio.Where(x => (bool)x.bitActivo).ToList();
                        ddlSitio.DataSource = _lstSitio;
                        ddlSitio.DataTextField = "vchClaveSitio";
                        ddlSitio.DataValueField = "id_Sitio";

                    }
                }
                ddlSitio.DataBind();
                ddlSitio.Items.Insert(0, new ListItem("Seleccionar...", "0"));
            }
            catch (Exception ecs)
            {
                Log.EscribeLog("Existe un error en cargarSitios: " + ecs.Message);
            }
        }

        private void cargaproyectosCat()
        {
            try
            {
                ddlProyecto.DataSource = null;
                List<tbl_CAT_Proyecto> _lstSitio = new List<tbl_CAT_Proyecto>();
                _lstSitio = NapoleonDA.getProyectos();
                if (_lstSitio != null)
                {
                    if (_lstSitio.Count > 0)
                    {
                        _lstSitio = _lstSitio.Where(x => (bool)x.bitActivo).ToList();
                        ddlProyecto.DataSource = _lstSitio;
                        ddlProyecto.DataTextField = "vchProyectoDesc";
                        ddlProyecto.DataValueField = "intProyectoID";

                    }
                }
                ddlProyecto.DataBind();
                ddlProyecto.Items.Insert(0, new ListItem("Seleccionar...", "0"));
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
                _lstSitio = NapoleonDA.getSitios(intProyectoG,0);
                
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
                            mdlConfig.datFechaSistema = item.datFechaSistema == null ? DateTime.MinValue : (DateTime)item.datFechaSistema;
                            mdlConfig.intPuertoCliente = item.intPuertoCliente == null ? 0:(int)item.intPuertoCliente;
                            mdlConfig.in_tPuertoServer = item.in_tPuertoServer == null ? 0  : (int)item.in_tPuertoServer;
                            mdlConfig.vchAETitle = item.vchAETitle == null ? "" : item.vchAETitle;
                            mdlConfig.vchAETitleServer = item.vchAETitleServer == null ? "" : item.vchAETitleServer;
                            mdlConfig.vchIPCliente = item.vchIPCliente == null ? "" : item.vchIPCliente;
                            mdlConfig.vchIPServidor = item.vchIPServidor == null ? "" : item.vchIPServidor;
                            mdlConfig.vchMaskCliente = item.vchMaskCliente == null ? "" : item.vchMaskCliente;
                            mdlConfig.vchnombreSitio = item.vchnombreSitio == null ? "" : item.vchnombreSitio;
                            mdlConfig.vchPathLocal = item.vchPathLocal == null ? "" : item.vchPathLocal;
                            mdlConfig.vchUserAdmin = item.vchUserAdmin == null ? "" : item.vchUserAdmin;
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
            try
            {
                cargaGridUsuarios();
            }
            catch(Exception eTXB)
            {
                Log.EscribeLog("Existe un error en txtBusqueda_TextChanged: " + eTXB.Message);
            }
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
                        UserRequest request = new UserRequest();
                        clsUsuario user = new clsUsuario();
                        user.intUsuarioID = intUsuarioID;
                        user.bitActivo = actualiza;
                        request.user = user;
                        clsMensaje response = new clsMensaje();
                        request.intUsuarioID = usuario.intUsuarioID.ToString();
                        request.vchUsuario = usuario.vchUsuario;
                        request.vchPassword = usuario.vchPassword;
                        request.Token = usuario.Token;
                        response = NapoleonDA.updateEstatusUsuario(request);
                        if(response.valido)
                        {
                            cargaGridUsuarios();
                            ShowMessage("Cambios correctos", MessageType.Correcto, "alert_containerSites");
                        }
                        else
                        {
                            ShowMessage("Existe un error al actualizar el estatus: " + response.vchMensaje, MessageType.Error, "alert_containerSites");
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
                if(mdlUsuario.intTipoUsuarioID > 1)
                {
                    ddlProyecto.SelectedValue = mdlUsuario.intProyectoID.ToString();
                }
                else
                {
                    ddlProyecto.SelectedValue = "0";
                }

                if (mdlUsuario.id_Sitio > 0)
                {
                    ddlSitio.SelectedValue = mdlUsuario.id_Sitio.ToString();
                }
                else
                {
                    ddlSitio.SelectedValue = "0";
                }

                if (mdlUsuario.intTipoUsuarioID != 1)
                {
                    if (mdlUsuario.intTipoUsuarioID == 2)
                    {
                        ddlProyecto.Enabled = true;
                        RequiredFieldValidator4.Enabled = true;
                        ddlSitio.Enabled = false;
                        rfvSitio.Enabled = false;
                    }
                    if (mdlUsuario.intTipoUsuarioID == 3)
                    {
                        ddlProyecto.Enabled = false;
                        RequiredFieldValidator4.Enabled = false;
                        ddlSitio.Enabled = true;
                        rfvSitio.Enabled = true;
                    }
                }
                else
                {
                    ddlProyecto.Enabled = false;
                    RequiredFieldValidator4.Enabled = false;
                    ddlSitio.Enabled = false;
                    rfvSitio.Enabled = false;
                }
                txtUsuario.Text = mdlUsuario.vchUsuario;
                txtPassword1.Text = Security.Decrypt(mdlUsuario.vchPassword);
                Session["PasswordAntiguo"] = Security.Decrypt(mdlUsuario.vchPassword);
                Session["intUsuarioIDGrid"] = mdlUsuario.intUsuarioID;
                txtUsuario.Enabled = false;
                txtEmailUser.Text = mdlUsuario.vchCorreo;
            }
            catch(Exception eFU)
            {
                Log.EscribeLog("Existe un error en fillusuario: " + eFU.Message);
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
                this.cargaGridUsuarios();
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
                this.cargaGridUsuarios();
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_container");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarUsuario();
            }
            catch(Exception eC)
            {
                Log.EscribeLog("Existe error en btnCancel_Click: " + eC.Message);
                ShowMessage("Existe un error: " + eC.Message, MessageType.Error, "alert_container");
            }
        }

        private void limpiarUsuario()
        {
            try
            {
                txtNombre.Text ="";
                txtApePat.Text ="";
                ddlTipoUsuario.SelectedValue = "1";
                ddlSitio.SelectedValue = "0";
                ddlProyecto.SelectedValue = "0";
                ddlProyecto.Enabled = false;
                txtUsuario.Text = "";
                txtUsuario.Enabled = true;
                txtPassword1.Text = "";
                Session["PasswordAntiguo"] = "";
                Session["intUsuarioIDGrid"] =0;
            }
            catch(Exception elu)
            {
                Log.EscribeLog("Existe un erro en LimpiarUsuario: " + elu.Message);
                throw elu;
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                tbl_CAT_Usuarios mdl = new tbl_CAT_Usuarios();
                clsMensaje response = new clsMensaje();
                if (Session["intUsuarioIDGrid"] != null)
                {
                    if (Convert.ToInt32(Session["intUsuarioIDGrid"].ToString()) > 0)
                    {
                        //Edicion
                        mdl = obtenerUsuario();
                        if (mdl != null)
                        {
                            mdl.bitSolicitarPass = false;
                            UserRequest request = new UserRequest();
                            request.usuario = mdl;
                            request.intUsuarioID = usuario.intUsuarioID.ToString();
                            request.Token = usuario.Token;
                            request.vchPassword = usuario.vchPassword;
                            request.vchUsuario = usuario.vchUsuario;
                            response = NapoleonDA.updateUsuario(request);
                            if (response.valido)
                            {
                                cargaGridUsuarios();
                                limpiarUsuario();
                                ShowMessage("Cambios correctos", MessageType.Correcto, "alert_container");
                            }
                            else
                            {
                                ShowMessage("Existe un error en: " + response.vchMensaje, MessageType.Error, "alert_container");
                            }
                        }
                    }
                    else
                    {
                        //Nuevo
                        mdl = obtenerUsuario();
                        if (mdl != null)
                        {
                            mdl.bitSolicitarPass = true;
                            UserRequest request = new UserRequest();
                            request.usuario = mdl;
                            request.intUsuarioID = usuario.intUsuarioID.ToString();
                            request.Token = usuario.Token;
                            request.vchPassword = usuario.vchPassword;
                            request.vchUsuario = usuario.vchUsuario;
                            response = NapoleonDA.setUsuario(request);
                            if (response.valido)
                            {
                                cargaGridUsuarios();
                                limpiarUsuario();
                                ShowMessage("Cambios correctos", MessageType.Correcto, "alert_container");
                            }
                            else
                            {
                                ShowMessage("Existe un error en: " + response.vchMensaje, MessageType.Error, "alert_container");

                            }
                        }
                    }
                }
            }
            catch (Exception eBA)
            {
                Log.EscribeLog("Existe un error en btnAddUser_Click: " + eBA.Message);
                ShowMessage("Existe un error al guardar el usuario: " + eBA.Message, MessageType.Error, "alert_container");
            }
        }

        private tbl_CAT_Usuarios obtenerUsuario()
        {
            tbl_CAT_Usuarios mdl = new tbl_CAT_Usuarios();
            try
            {
                mdl.intUsuarioID = Convert.ToInt32(Session["intUsuarioIDGrid"].ToString());
                mdl.vchNombre = txtNombre.Text;
                mdl.vchApellido = txtApePat.Text;
                mdl.intTipoUsuarioID = Convert.ToInt32(ddlTipoUsuario.SelectedValue.ToString());
                
                if(mdl.intTipoUsuarioID != 1 && mdl.intTipoUsuarioID != 4)
                {
                    if(mdl.intTipoUsuarioID == 2)
                    {
                        mdl.intProyectoID = Convert.ToInt32(ddlProyecto.SelectedValue.ToString());
                    }
                    if(mdl.intTipoUsuarioID == 3)
                    {
                        mdl.id_Sitio = Convert.ToInt32(ddlSitio.SelectedValue.ToString());
                    }
                }
                else
                {
                    mdl.intProyectoID = null;
                    mdl.id_Sitio = null;
                }
                
                mdl.vchUsuario = txtUsuario.Text;
                if(txtPassword1.Text == "")
                {
                    mdl.vchPassword = Session["PasswordAntiguo"].ToString();
                }
                else
                {
                    mdl.vchPassword = Security.Encrypt(txtPassword1.Text);
                }
                mdl.datFecha = DateTime.Now;
                mdl.bitActivo = true;
                mdl.vchUserAdmin = Session["UserID"].ToString();
                mdl.vchCorreo = txtEmailUser.Text;
            }
            catch(Exception eOU)
            {
                mdl = null;
                Log.EscribeLog("Existe un error al obtenerUsuario: " + eOU.Message);
            }
            return mdl;
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
                        clsMensaje response = new clsMensaje();
                        response = NapoleonDA.updateEstatusSitio(id_Sitio, (bool)_mdlSitio.bitActivo);
                        if(response.valido)
                        {
                            cargaSitios();
                            cargaSitiosP();
                            cargaSitiosCat();
                            ShowMessage("Cambios correctos", MessageType.Correcto, "alert_containerSites");
                        }
                        else
                        {
                            ShowMessage("Existe un error al actualizar el estatus: " + response.vchMensaje, MessageType.Error, "alert_containerSites");
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
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvSites.AllowPaging = true;
                    this.grvSites.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvSites.AllowPaging = false;
                this.cargaSitios();
            }
            catch (Exception eddS)
            {
                ShowMessage("Existe un error: " + eddS.Message, MessageType.Error, "alert_containerSites");
            }
        }

        protected void txtBandejaSitio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvSites.PageIndex = numeroPagina - 1;
                else
                    this.grvSites.PageIndex = 0;
                this.cargaSitios();
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_containerSites");
            }
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
                        clsMensaje response = new clsMensaje();
                        response = NapoleonDA.updateEstatusProyectos(intProyectoID, activo);
                        if(response.valido)
                        {
                            cargaProyectos();
                            cargaproyectosCat();
                            ShowMessage("Cambios correctos", MessageType.Correcto, "alert_container");
                        }
                        else
                        {
                            ShowMessage("Existe un error al actializar: " + response.vchMensaje, MessageType.Error, "alert_container");
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
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.gridSitiosProyec.AllowPaging = true;
                    this.gridSitiosProyec.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.gridSitiosProyec.AllowPaging = false;
                this.cargaGridSitiosProyec();
            }
            catch (Exception eddS)
            {
                ShowMessage("Existe un error: " + eddS.Message, MessageType.Error, "alert_containerSites");
            }
        }

        protected void txtBandejaPS_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.gridSitiosProyec.PageIndex = numeroPagina - 1;
                else
                    this.gridSitiosProyec.PageIndex = 0;
                this.cargaGridSitiosProyec();
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_container");
            }
        }

        protected void ddlBandejaProy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvProyectos.AllowPaging = true;
                    this.grvProyectos.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvProyectos.AllowPaging = false;
                this.cargaProyectos();
            }
            catch (Exception eddS)
            {
                ShowMessage("Existe un error: " + eddS.Message, MessageType.Error, "alert_containerSites");
            }
        }

        protected void txtBandejaProyec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvProyectos.PageIndex = numeroPagina - 1;
                else
                    this.grvProyectos.PageIndex = 0;
                this.cargaProyectos();
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_containerSites");
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
                        request.intUsuarioID = usuario.intUsuarioID.ToString();
                        request.Token = usuario.Token;
                        request.vchPassword = usuario.vchPassword;
                        request.vchUsuario = usuario.vchUsuario;
                        response = NapoleonDA.updateProyecto(request);
                        if (response != null)
                        {
                            if (response.success)
                            {
                                cargaProyectos();
                                ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_containerSites");
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
                        ShowMessage("Se debe seleccionar al menos un sitio por proyecto.", MessageType.Informacion, "alert_containerSites");
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
                        request.intUsuarioID = usuario.intUsuarioID.ToString();
                        request.Token = usuario.Token;
                        request.vchPassword = usuario.vchPassword;
                        request.vchUsuario = usuario.vchUsuario;
                        response = NapoleonDA.setProyecto(request);
                        if (response != null)
                        {
                            if (response.success)
                            {
                                cargaProyectos();
                                ShowMessage("Cambios correctos.", MessageType.Correcto, "alert_containerSites");
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
                        ShowMessage("Se debe seleccionar al menos un sitio por proyecto.", MessageType.Informacion, "alert_containerSites");
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
                Log.EscribeLog("Existe un error en obtenerSitios: " + eOS.Message);
                throw eOS;
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
                Log.EscribeLog("Existe un erro en obtenerSitiosEdicion: " + eOS.Message);
                throw eOS;
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

        protected void ddlTipoUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTipoUsuario.SelectedValue.ToString() != "")
                {
                    if (Convert.ToInt32(ddlTipoUsuario.SelectedValue.ToString()) != 1)
                    {
                        if (Convert.ToInt32(ddlTipoUsuario.SelectedValue.ToString()) == 2)
                        {
                            ddlProyecto.Enabled = true;
                            RequiredFieldValidator4.Enabled = true;
                            ddlSitio.Enabled = false;
                            rfvSitio.Enabled = false;
                        }
                        if (Convert.ToInt32(ddlTipoUsuario.SelectedValue.ToString()) == 3)
                        {
                            ddlProyecto.Enabled = false;
                            RequiredFieldValidator4.Enabled = false;
                            ddlSitio.Enabled = true;
                            rfvSitio.Enabled = true;
                        }
                    }
                    else
                    {
                        ddlProyecto.Enabled = false;
                        RequiredFieldValidator4.Enabled = false;
                        ddlSitio.Enabled = false;
                        rfvSitio.Enabled = false;
                    }
                }
            }
            catch(Exception eddP)
            {
                Log.EscribeLog("Existe un error en ddlTipoUsuario_SelectedIndexChanged: " + eddP.Message);
                ShowMessage("Existe un error: " + eddP.Message, MessageType.Error, "alert_containerSites");
            }
        }

        
    }
}