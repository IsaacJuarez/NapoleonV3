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
    public partial class frmPrioridadSitio : System.Web.UI.Page
    {
        NapoleonService NapoleonDA = new NapoleonService();
        public static clsUsuario usuario = new clsUsuario();
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
                if (Session["UserID"] != null && Session["UserID"].ToString() != "" && Session["tbl_CAT_Usuarios"] != null &&
               Security.ValidateToken(Session["Token"].ToString(), Session["intUsuarioID"].ToString(), Session["UserID"].ToString(), Session["Password"].ToString()))
                {
                    if (!IsPostBack)
                    {
                        Session["intUsuarioIDGrid"] = 0;
                        usuario = (clsUsuario)Session["tbl_CAT_Usuarios"];
                        intTipoUsuario = usuario.intTipoUsuarioID;
                        intProyectoG = usuario.intProyectoID == null ? 0 : usuario.intProyectoID;
                        cargaSitioPrioridad();
                        cargaGridPrioridad();
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

        public void cargaGridPrioridad()
        {
            try
            {
                List<clsPrioridadSucursal> _lst = new List<clsPrioridadSucursal>();
                _lst = NapoleonDA.getPrioridadSucursal((usuario.id_Sitio == null ? 0 : (int)usuario.id_Sitio), (usuario.intProyectoID == null ? 0 : (int)usuario.intProyectoID));
                if (_lst != null)
                {
                    if (_lst.Count > 0)
                    {
                        //Log.EscribeLog("Se tienen objetos.");
                        if (ddlSitioPriridad.SelectedValue != "0")
                        {
                            _lst = _lst.Where(x => x.id_Sitio == Convert.ToInt32(ddlSitioPriridad.SelectedValue)).ToList();
                        }
                        //Log.EscribeLog("ddl.");
                        List<clsPrioridadSucursal> _lstGridOrder = new List<clsPrioridadSucursal>();
                        int order = 0;
                        foreach (clsPrioridadSucursal mdl in _lst)
                        {
                            if (mdl.intSecuencia > 0)
                                order++;
                        }
                        //Log.EscribeLog("despues del foreach.");
                        if (order > 0)
                        {
                            //        SecMax = _lst.Max(x => x.intSecuencia);
                            //    //    List<clsPrioridadSucursal> _lstGridConPrioridad = new List<clsPrioridadSucursal>();
                            //    //    List<clsPrioridadSucursal> _lstGridSinPrioridad = new List<clsPrioridadSucursal>();
                            //    //    _lstGridConPrioridad = _lst.Where(z => z.intSecuencia > 0).ToList();
                            //    //    _lstGridSinPrioridad = _lst.Where(z => z.intSecuencia == 0).ToList();
                            //    //    _lstGridOrder = _lstGridConPrioridad.OrderBy(x => x.intSecuencia).ToList();
                            //    //    _lstGridOrder.AddRange(_lstGridSinPrioridad);
                            _lstGridOrder = _lst.OrderBy(x => x.id_Sitio).ThenBy(x => x.intSecuencia).ToList();
                        }
                        else
                        {
                            _lstGridOrder = _lst.OrderBy(x => x.id_Sitio).ToList();
                        }
                        Log.EscribeLog("Sort.");
                        _lstPriodidad = _lstGridOrder;
                        grvPrioridad.DataSource = _lstPriodidad;
                    }
                    else
                        grvPrioridad.DataSource = null;
                }
                else
                    grvPrioridad.DataSource = null;
                grvPrioridad.DataBind();
            }
            catch (Exception ecU)
            {
                Log.EscribeLog("Existe un error en cargaGridPrioridad: " + ecU.Message);
            }
        }

        protected void ddlSitioPriridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargaGridPrioridad();
            }
            catch (Exception edSi)
            {
                ShowMessage("Existe un error al consultar la información por sitio: " + edSi.Message, MessageType.Error, "alert_containerPrioridad");
            }
        }

        private void cargaSitioPrioridad()
        {
            try
            {
                //ddlSitioPriridad
                List<clsModeloCatalogo> _lstCatSuc = new List<clsModeloCatalogo>();
                _lstCatSuc = NapoleonDA.getCatalogo("Sucursales", intProyectoG, (usuario.id_Sitio == null ? 0 : usuario.id_Sitio));
                if (_lstCatSuc != null)
                {
                    if (_lstCatSuc.Count > 0)
                    {
                        ddlSitioPriridad.DataSource = _lstCatSuc;
                        ddlSitioPriridad.DataTextField = "vchDescripcion";
                        ddlSitioPriridad.DataValueField = "vchValue";
                        ddlSitioPriridad.DataBind();
                    }
                }
                if (intTipoUsuario == 1 || intTipoUsuario == 2)
                    ddlSitioPriridad.Items.Insert(0, new ListItem("Todos los sitios...", "0"));
            }
            catch (Exception ecSP)
            {
                Log.EscribeLog("Existe un error en cargaSitioPrioridad: " + ecSP.Message);
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

        protected void grvPrioridad_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotalP");
                    lblTotalNumDePaginas.Text = grvPrioridad.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandejaP");
                    txtIrAlaPagina.Text = (grvPrioridad.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandejaP");
                    ddlTamPagina.SelectedValue = grvPrioridad.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                if (e.Row.DataItem != null)
                {
                    clsPrioridadSucursal _mdl = (clsPrioridadSucursal)e.Row.DataItem;
                    ImageButton ibtEstatus = (ImageButton)e.Row.FindControl("imbEstatusP");
                    ibtEstatus.Attributes.Add("onclick", "javascript:return confirm('¿Desea realizar el cambio de estatus de la modalidad seleccionada?');");
                    if (_mdl.intSecuencia > 0 && _mdl.intSecuencia != 99999)
                    {
                        ibtEstatus.Enabled = true;
                        if (_mdl.bitActivo)
                            ibtEstatus.ImageUrl = @"~/Images/ic_action_tick.png";
                        else
                            ibtEstatus.ImageUrl = @"~/Images/ic_action_cancel.png";
                    }
                    else
                    {
                        ibtEstatus.Enabled = false;
                        ibtEstatus.ImageUrl = @"~/Images/ic_action_cancel.png";
                    }
                    int SecMax = 0;
                    if (_lstPriodidad.Any(x => x.id_Sitio == _mdl.id_Sitio && x.intSecuencia != 99999))
                    {
                        SecMax = _lstPriodidad.Where(x => x.id_Sitio == _mdl.id_Sitio && x.intSecuencia != 99999).OrderByDescending(t => t.intSecuencia).FirstOrDefault().intSecuencia;
                    }
                    if (SecMax == 99999)
                        SecMax = 0;
                    //Agregar botones para manejar las prioridades
                    if (_mdl.intSecuencia > 0 && _mdl.intSecuencia != 99999)
                    {
                        ((CheckBox)e.Row.Cells[4].FindControl("chkRow")).Checked = true;
                        if (_mdl.intSecuencia != 1 && _mdl.intSecuencia != SecMax && _mdl.intSecuencia != 99999)
                        {
                            ((LinkButton)e.Row.Cells[4].FindControl("btnUp")).Visible = true;
                            ((LinkButton)e.Row.Cells[4].FindControl("btnDown")).Visible = true;
                        }
                        if (_mdl.intSecuencia != 1 && _mdl.intSecuencia == SecMax && _mdl.intSecuencia != 99999)
                        {
                            ((LinkButton)e.Row.Cells[4].FindControl("btnUp")).Visible = true;
                            ((LinkButton)e.Row.Cells[4].FindControl("btnDown")).Visible = false;
                        }
                        if (_mdl.intSecuencia == 1 && _mdl.intSecuencia != SecMax && _mdl.intSecuencia != 99999)
                        {
                            ((LinkButton)e.Row.Cells[4].FindControl("btnUp")).Visible = false;
                            ((LinkButton)e.Row.Cells[4].FindControl("btnDown")).Visible = true;
                        }
                        if (_mdl.intSecuencia == 1 && _mdl.intSecuencia == SecMax && _mdl.intSecuencia != 99999)
                        {
                            ((LinkButton)e.Row.Cells[4].FindControl("btnUp")).Visible = false;
                            ((LinkButton)e.Row.Cells[4].FindControl("btnDown")).Visible = false;
                        }
                    }
                    else
                    {
                        ((LinkButton)e.Row.Cells[4].FindControl("btnUp")).Visible = false;
                        ((LinkButton)e.Row.Cells[4].FindControl("btnDown")).Visible = false;
                        ((CheckBox)e.Row.Cells[4].FindControl("chkRow")).Checked = false;
                    }
                }
            }
            catch (Exception egrdb)
            {
                Log.EscribeLog("Existe un erorr en grvPrioridad_RowDataBound:" + egrdb.Message);
            }
        }

        protected void grvPrioridad_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvPrioridad.PageIndex = e.NewPageIndex;
                    cargaGridPrioridad();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_containerPrioridad");
            }
        }

        protected void grvPrioridad_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int mosID = 0;
                clsPrioridadSucursal _mdlPrioSuc;
                switch (e.CommandName)
                {
                    case "addPrioridad":
                        PrioridadRequest _request = new PrioridadRequest();
                        PrioridadResponse _response = new PrioridadResponse();
                        _request.intDireccion = 2;
                        _request.intEstudioID = Convert.ToInt32(e.CommandArgument.ToString());
                        _request.intSecuenciaActual = _lstPriodidad.First(x => x.intREL_SitioModID == Convert.ToInt32(e.CommandArgument.ToString())).intSecuencia;
                        _request.Token = Session["Token"].ToString();
                        _request.intUsuarioID = Session["intUsuarioID"].ToString();
                        _request.vchUsuario = Session["UserID"].ToString();
                        _request.vchPassword = Session["Password"].ToString();
                        _response = NapoleonDA.setPrioridadesSucModAcomodar(_request);
                        if (_response != null)
                        {
                            if (_response._mensaje.vchError == "" && _response._mensaje.vchMensaje == "OK")
                            {
                                cargaGridPrioridad();
                            }
                            else
                            {
                                ShowMessage("Existe un error al aumentar la prioridad: " + _response._mensaje.vchError + ". " + _response._mensaje.vchMensaje, MessageType.Error, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Existe un error al aumentar la prioridad.", MessageType.Error, "alert_container");
                        }
                        break;
                    case "lessPrioridad":
                        PrioridadRequest _requestl = new PrioridadRequest();
                        PrioridadResponse _responsel = new PrioridadResponse();
                        _requestl.intDireccion = 3;
                        _requestl.intEstudioID = Convert.ToInt32(e.CommandArgument.ToString());
                        _requestl.intSecuenciaActual = _lstPriodidad.First(x => x.intREL_SitioModID == Convert.ToInt32(e.CommandArgument.ToString())).intSecuencia;
                        _requestl.Token = Session["Token"].ToString();
                        _requestl.intUsuarioID = Session["intUsuarioID"].ToString();
                        _requestl.vchUsuario = Session["UserID"].ToString();
                        _requestl.vchPassword = Session["Password"].ToString();
                        _responsel = NapoleonDA.setPrioridadesSucModAcomodar(_requestl);
                        if (_responsel != null)
                        {
                            if (_responsel._mensaje.vchError == "" && _responsel._mensaje.vchMensaje == "OK")
                            {
                                cargaGridPrioridad();
                            }
                            else
                            {
                                ShowMessage("Existe un error al aumentar la prioridad: " + _responsel._mensaje.vchError + ". " + _responsel._mensaje.vchMensaje, MessageType.Error, "alert_container");
                            }
                        }
                        else
                        {
                            ShowMessage("Existe un error al aumentar la prioridad.", MessageType.Error, "alert_container");
                        }
                        break;
                    case "Estatus":
                        mosID = Convert.ToInt32(e.CommandArgument.ToString());
                        _mdlPrioSuc = _lstPriodidad.Where(x => x.intREL_SitioModID == mosID).ToList().First();
                        PrioridadModalidadRequest request = new PrioridadModalidadRequest();
                        clsPrioridadSucursal _mdl = new clsPrioridadSucursal();
                        _mdl.bitActivo = _mdlPrioSuc.bitActivo;
                        _mdl.intREL_SitioModID = _mdlPrioSuc.intREL_SitioModID;
                        request.mdlPrioridad = _mdl;
                        request.Token = Session["Token"].ToString();
                        request.intUsuarioID = Session["intUsuarioID"].ToString();
                        request.vchUsuario = Session["UserID"].ToString();
                        request.vchPassword = Session["Password"].ToString();
                        clsMensaje response = new clsMensaje();
                        response = NapoleonDA.updateEstatusPrioridadModalidad(request);
                        if (response != null)
                        {
                            if (response.vchError == "")
                            {
                                cargaGridPrioridad();
                                ShowMessage("Se actualizaron correctamente los datos. ", MessageType.Correcto, "alert_containerPrioridad");
                            }
                            else
                                ShowMessage("Existe un error al actualizar los datos del usuario: " + response.vchError, MessageType.Error, "alert_containerPrioridad");
                        }
                        else
                        {
                            ShowMessage("Existe un error al actualizar los datos del usuario", MessageType.Error, "alert_containerPrioridad");
                        }
                        break;
                }
            }
            catch (Exception egrRc)
            {
                throw new Exception(egrRc.Message);
            }
        }

        protected void chkRow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                int index = row.RowIndex;
                CheckBox chk = (CheckBox)sender;
                int mosID = Convert.ToInt32(grvPrioridad.DataKeys[index].Value.ToString());
                clsMensaje _men = new clsMensaje();
                PrioridadSucModRequest _req = new PrioridadSucModRequest();
                _req.mosID = mosID;
                _req.activar = chk.Checked;
                _req.Token = Session["Token"].ToString();
                _req.intUsuarioID = Session["intUsuarioID"].ToString();
                _req.vchUsuario = Session["UserID"].ToString();
                _req.vchPassword = Session["Password"].ToString();
                _men = NapoleonDA.setPrioridadesSucMod(_req);
                if (_men != null)
                {
                    if (_men.vchMensaje == "OK")
                    {
                        ShowMessage("Se guardo correctamente la información.", MessageType.Correcto, "alert_containerPrioridad");
                        cargaGridPrioridad();
                    }
                    else
                    {
                        ShowMessage("Existe un error al realziar el proceso: " + (_men.vchMensaje == "" ? _men.vchError : _men.vchMensaje), MessageType.Error, "alert_containerPrioridad");
                    }
                }
                else
                {
                    ShowMessage("No se pudo guardar el proceso. Favor de verificar la información.", MessageType.Informacion, "alert_containerPrioridad");
                }
            }
            catch (Exception eck)
            {
                ShowMessage("Existe un error: " + eck.Message, MessageType.Error, "alert_containerPrioridad");
            }
        }

        protected void ddlBandejaP_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                if (int.Parse(dropDownList.SelectedValue) != 0)
                {
                    this.grvPrioridad.AllowPaging = true;
                    this.grvPrioridad.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvPrioridad.AllowPaging = false;
                this.cargaGridPrioridad();
            }
            catch (Exception eddS)
            {
                ShowMessage("Existe un error: " + eddS.Message, MessageType.Error, "alert_containerPrioridad");
            }
        }

        protected void txtBandejaP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBandejaAvaluosGoToPage = (TextBox)sender;
                int numeroPagina;
                if (int.TryParse(txtBandejaAvaluosGoToPage.Text.Trim(), out numeroPagina))
                    this.grvPrioridad.PageIndex = numeroPagina - 1;
                else
                    this.grvPrioridad.PageIndex = 0;
                this.cargaGridPrioridad();
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_containerPrioridad");
            }
        }
    }
}