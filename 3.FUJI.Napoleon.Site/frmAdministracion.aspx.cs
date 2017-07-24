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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace _3.FUJI.Napoleon.Site
{
    public partial class frmAdministracion : System.Web.UI.Page
    {
        NapoleonService NapoleonDA = new NapoleonService();
        private static List<clsEstudio> _lstGlo = new List<clsEstudio>();
        public static int intTipoUsuario = 0;
        public static clsUsuario user = new clsUsuario();
        public static List<tbl_MST_PrioridadEstudio> _lstPrioridades = new List<tbl_MST_PrioridadEstudio>();
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
            if (Session["UserID"] != null && Session["UserID"].ToString() != "" && Session["tbl_CAT_Usuarios"] != null &&
                Security.ValidateToken(Session["Token"].ToString(), Session["intUsuarioID"].ToString(), Session["UserID"].ToString(), Session["Password"].ToString()))
            {
                if (!IsPostBack)
                {
                    //_lstPrioridades = new List<tbl_MST_PrioridadEstudio>();
                    intTipoUsuario = Convert.ToInt32(Session["intTipoUsuario"].ToString());
                    user = (clsUsuario)Session["tbl_CAT_Usuarios"];
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
                    inicializaCombos();
                    cargaGridAdministracion();
                }
            }
            else
            {
                Response.Redirect(URL + "/frmLogin.aspx");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openPopover();", true);
        }

        private void inicializaCombos()
        {
            try
            {
                List<clsModeloCatalogo> _lstCat = new List<clsModeloCatalogo>();
                //Combo de Estatus
                _lstCat = NapoleonDA.getCatalogo("Estatus", 0, 0);
                ddlBusEstatus.DataSource = _lstCat;
                ddlBusEstatus.DataTextField = "vchDescripcion";
                ddlBusEstatus.DataValueField = "vchValue";
                ddlBusEstatus.DataBind();
                ddlBusEstatus.Items.Insert(0, new ListItem("Todos...", "0"));
                //Combo de Sucursales
                _lstCat = null;
                _lstCat = NapoleonDA.getCatalogo("Sucursales", user.intProyectoID, user.id_Sitio);
                ddlBusSucursal.DataSource = _lstCat;
                ddlBusSucursal.DataTextField = "vchDescripcion";
                ddlBusSucursal.DataValueField = "vchValue";
                ddlBusSucursal.DataBind();
                if (intTipoUsuario == 1 || intTipoUsuario == 2)
                    ddlBusSucursal.Items.Insert(0, new ListItem("Todas...", "0"));
                //Combo de Prioridad
                _lstCat = null;
                _lstCat = NapoleonDA.getCatalogo("Prioridad", 0, 0);
                ddlBusPrioridad.DataSource = _lstCat;
                ddlBusPrioridad.DataTextField = "vchDescripcion";
                ddlBusPrioridad.DataValueField = "vchValue";
                ddlBusPrioridad.DataBind();
                ddlBusPrioridad.Items.Insert(0, new ListItem("Todas...", "0"));
                //Combo de Modalidad
                _lstCat = null;
                _lstCat = NapoleonDA.getCatalogo("Modalidades", 0, 0);
                ddlBusModalidad.DataSource = _lstCat;
                ddlBusModalidad.DataTextField = "vchDescripcion";
                ddlBusModalidad.DataValueField = "vchValue";
                ddlBusModalidad.DataBind();
                ddlBusModalidad.Items.Insert(0, new ListItem("Todas...", "0"));
            }
            catch (Exception eIC)
            {
                throw eIC;
            }
        }

        private void cargaGridAdministracion()
        {
            try
            {
                List<clsEstudio> _lstGrid = new List<clsEstudio>();
                clsMensaje response = new clsMensaje();
                int intModalidadID = Convert.ToInt32(ddlBusModalidad.SelectedValue);
                int intEstatusID = Convert.ToInt32(ddlBusEstatus.SelectedValue);
                int id_Sitio = Convert.ToInt32(ddlBusSucursal.SelectedValue);
                response = NapoleonDA.getListEstudios(intEstatusID, id_Sitio, intModalidadID, user.intProyectoID);
                if (response._lstEst.Count > 0)
                {
                    _lstGrid = response._lstEst;
                    _lstGlo = _lstGrid.Where(x => x.vchAccessionNumber.ToUpper().Contains(txtBusNumEstudio.Text.ToUpper())
                    //&& x.EstatusID != EstatusCompleto
                    && x.PatientName.ToUpper().Contains(txtBusNombre.Text.ToUpper())).ToList();

                    //Prioridad
                    if (ddlBusPrioridad.SelectedValue != "")
                        if (Convert.ToInt32(ddlBusPrioridad.SelectedValue) > 0)
                            _lstGlo = _lstGlo.Where(z => z.bitUrgente == (ddlBusPrioridad.SelectedValue == "1" ? false : true)).ToList();

                    ////Estatus
                    //if (ddlBusEstatus.SelectedValue != "")
                    //    if (Convert.ToInt32(ddlBusEstatus.SelectedValue) > 0)
                    //        _lstGlo = _lstGlo.Where(z => z.intEstatusID == Convert.ToInt32(ddlBusEstatus.SelectedValue)).ToList();

                    //// Sucursal
                    //if (ddlBusSucursal.SelectedValue != "")
                    //    if (Convert.ToInt32(ddlBusSucursal.SelectedValue) > 0)
                    //        _lstGlo = _lstGlo.Where(z => z.id_Sitio == Convert.ToInt32(ddlBusSucursal.SelectedValue)).ToList();

                    //// Modalidad
                    //if (ddlBusModalidad.SelectedValue != "")
                    //    if (Convert.ToInt32(ddlBusModalidad.SelectedValue) > 0)
                    //        _lstGlo = _lstGlo.Where(z => z.intModalidadID == Convert.ToInt32(ddlBusModalidad.SelectedValue)).ToList();

                    List<clsEstudio> _lstGridOrder = new List<clsEstudio>();
                    int order = 0;
                    foreach (clsEstudio mdl in _lstGlo)
                    {
                        if (mdl.intPrioridadID > 0 && mdl.intSecuencia > 0)
                            order++;
                    }
                    if (order > 0)
                    {
                        List<clsEstudio> _lstGridConPrioridad = new List<clsEstudio>();
                        List<clsEstudio> _lstGridSinPrioridad = new List<clsEstudio>();
                        _lstGridConPrioridad = _lstGlo.Where(z => z.intPrioridadID > 0 && z.intSecuencia > 0).ToList();
                        _lstGridSinPrioridad = _lstGlo.Where(z => z.intPrioridadID == 0 || z.intSecuencia == 0).ToList();
                        _lstGridOrder = _lstGridConPrioridad.OrderBy(x => x.intSecuencia).ToList();
                        SecMax = _lstGridConPrioridad.Max(x => x.intSecuencia);
                        _lstGridOrder.AddRange(_lstGridSinPrioridad);
                        _lstPrioridades.Clear();
                    }
                    else
                        _lstGridOrder = _lstGlo.OrderByDescending(x => x.intEstudioID).ToList();
                    _lstGlo = _lstGridOrder;
                    grvBusqueda.DataSource = _lstGridOrder;
                }
                else
                    grvBusqueda.DataSource = null;
                grvBusqueda.DataBind();
            }
            catch (Exception ecgA)
            {
                throw ecgA;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                cargaGridAdministracion();
            }
            catch (Exception eTimer)
            {
                Log.EscribeLog("Existe un error al actualizar el timmer: " + eTimer.Message);
            }
        }

        protected void btnBusquedaEst_Click(object sender, EventArgs e)
        {
            try
            {
                cargaGridAdministracion();
            }
            catch (Exception eBS)
            {
                ShowMessage("Existe un error: " + eBS.Message, MessageType.Error, "alert_container");
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                autoguardar();
            }
            catch (Exception ebGc)
            {
                Log.EscribeLog("Existe un error en btnGuardarCambios_Click: " + ebGc.Message);
                throw ebGc;
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

                clsEstudio _mdl = new clsEstudio();
                _mdl = e.Row.DataItem as clsEstudio;
                LinkButton lblEstatus = (LinkButton)e.Row.FindControl("lblEstatus");
                //HtmlAnchor atag = (HtmlAnchor)e.Row.FindControl("lblTooltip");
                //string contenido = "- Archivo 1: En proceso. " + "<br/>";
                //contenido += "- Archivo 2: En proceso. " + "<br/>";
                //atag.Attributes.Add("data-content", contenido);
                lblEstatus.Text = _mdl.vchEstatusID;
                //Agregar botones para manejar las prioridades
                if (_mdl.intSecuencia > 0)
                {
                    if (_mdl.intSecuencia != 1 && _mdl.intSecuencia != SecMax)
                    {
                        ((LinkButton)e.Row.Cells[13].FindControl("btnUp")).Visible = true;
                        ((LinkButton)e.Row.Cells[13].FindControl("btnDown")).Visible = true;
                    }
                    if (_mdl.intSecuencia != 1 && _mdl.intSecuencia == SecMax)
                    {
                        ((LinkButton)e.Row.Cells[13].FindControl("btnUp")).Visible = true;
                        ((LinkButton)e.Row.Cells[13].FindControl("btnDown")).Visible = false;
                    }
                    if (_mdl.intSecuencia == 1 && _mdl.intSecuencia != SecMax)
                    {
                        ((LinkButton)e.Row.Cells[13].FindControl("btnUp")).Visible = false;
                        ((LinkButton)e.Row.Cells[13].FindControl("btnDown")).Visible = true;
                    }
                    if (_mdl.intSecuencia == 1 && _mdl.intSecuencia == SecMax)
                    {
                        ((LinkButton)e.Row.Cells[13].FindControl("btnUp")).Visible = false;
                        ((LinkButton)e.Row.Cells[13].FindControl("btnDown")).Visible = false;
                    }
                }
                else
                {
                    ((LinkButton)e.Row.Cells[13].FindControl("btnUp")).Visible = false;
                    ((LinkButton)e.Row.Cells[13].FindControl("btnDown")).Visible = false;
                }
                if (_mdl.intEstatusID >= 3)
                {
                    ((CheckBox)e.Row.Cells[13].FindControl("ckhPrioridad")).Enabled = false;
                }
                else
                {
                    ((CheckBox)e.Row.Cells[13].FindControl("ckhPrioridad")).Enabled = true;
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
                    cargaGridAdministracion();
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
                switch (e.CommandName)
                {
                    case "addPrioridad":
                        PrioridadRequest _request = new PrioridadRequest();
                        PrioridadResponse _response = new PrioridadResponse();
                        _request.intDireccion = 2;
                        _request.intEstudioID = Convert.ToInt32(e.CommandArgument.ToString());
                        _request.intSecuenciaActual = _lstGlo.First(x => x.intEstudioID == Convert.ToInt32(e.CommandArgument.ToString())).intSecuencia;
                        _request.Token = Session["Token"].ToString();
                        _request.intUsuarioID = Session["intUsuarioID"].ToString();
                        _request.vchUsuario = Session["UserID"].ToString();
                        _request.vchPassword = Session["Password"].ToString();
                        _response = NapoleonDA.setPrioridad(_request);
                        if (_response != null)
                        {
                            if (_response._mensaje.vchError == "" && _response._mensaje.vchMensaje == "OK")
                            {
                                cargaGridAdministracion();
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
                        _requestl.intSecuenciaActual = _lstGlo.First(x => x.intEstudioID == Convert.ToInt32(e.CommandArgument.ToString())).intSecuencia;
                        _requestl.Token = Session["Token"].ToString();
                        _requestl.intUsuarioID = Session["intUsuarioID"].ToString();
                        _requestl.vchUsuario = Session["UserID"].ToString();
                        _requestl.vchPassword = Session["Password"].ToString();
                        _responsel = NapoleonDA.setPrioridad(_requestl);
                        if (_responsel != null)
                        {
                            if (_responsel._mensaje.vchError == "" && _responsel._mensaje.vchMensaje == "OK")
                            {
                                cargaGridAdministracion();
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
                }
            }
            catch (Exception egrRc)
            {
                ShowMessage("Existe un error: " + egrRc.Message, MessageType.Error, "alert_container");
            }
        }

        protected void ckhPrioridad_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                int index = row.RowIndex;
                CheckBox cb1 = (CheckBox)grvBusqueda.Rows[index].FindControl("ckhPrioridad");

                if (chkAutomatico.Checked)
                {
                    string yourvalue = cb1.Text;
                    //here you can find your control and get value(Id).

                    string _id = grvBusqueda.DataKeys[index]["intEstudioID"].ToString();
                    EstudioRequest request = new EstudioRequest();
                    tbl_MST_PrioridadEstudio _mdl = new tbl_MST_PrioridadEstudio();
                    _mdl.intEstudioID = Convert.ToInt32(_id);
                    _mdl.bitUrgente = cb1.Checked;
                    _mdl.bitAtendido = false;
                    _mdl.datAtendido = null;
                    _mdl.datFecha = DateTime.Now;
                    _mdl.vchusuarioSol = Session["UserID"].ToString();
                    if (_lstGlo.Exists(x => x.intEstatusID == _mdl.intEstudioID))
                        _mdl.intSecuencia = _lstGlo.First(x => x.intEstatusID == (int)_mdl.intEstudioID).intSecuencia;
                    request.Token = Session["Token"].ToString();
                    request.intUsuarioID = Session["intUsuarioID"].ToString();
                    request.vchUsuario = Session["UserID"].ToString();
                    request.vchPassword = Session["Password"].ToString();
                    request._mdlPrioridad = _mdl;
                    EstudioResponse response = new EstudioResponse();
                    response = NapoleonDA.setPrioridadEstudio(request);
                    if (response._mensaje != null)
                    {
                        if (response._mensaje.vchError == "")
                        {
                            cargaGridAdministracion();
                            ShowMessage("Se agrego correctamente el estudio a la lista de prioridades.", MessageType.Correcto, "alert_container");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPrioridad", "$('#modalPrioridad').modal('hide');", true);
                        }
                        else
                            ShowMessage("Existe un error al actualizar los datos del Estudio: " + response._mensaje.vchError, MessageType.Error, "alert_container");
                    }
                    else
                        ShowMessage("Existe un error al actualizar los datos del Estudio", MessageType.Error, "alert_container");

                }
                else
                {
                    string _id = grvBusqueda.DataKeys[index]["intEstudioID"].ToString();
                    string _numEst = grvBusqueda.DataKeys[index]["vchAccessionNumber"].ToString();
                    //Guardar en un modelo.
                    tbl_MST_PrioridadEstudio _mdlPrio = new tbl_MST_PrioridadEstudio();
                    if (_lstPrioridades.Exists(x => x.intEstudioID == Convert.ToInt32(_id)))
                    {
                        if (_lstGlo.First(x => x.vchAccessionNumber == _numEst).bitUrgente != cb1.Checked)
                        {
                            _lstPrioridades.Single(x => x.intEstudioID == Convert.ToInt32(_id)).bitUrgente = cb1.Checked;
                            _lstPrioridades.Single(x => x.intEstudioID == Convert.ToInt32(_id)).datFecha = DateTime.Now;
                            if (_lstGlo.Exists(x => x.intEstudioID == Convert.ToInt32(_id)))
                                _lstPrioridades.Single(x => x.intEstudioID == Convert.ToInt32(_id)).intSecuencia = _lstGlo.First(x => x.intEstudioID == Convert.ToInt32(_id)).intSecuencia;
                        }
                        else
                            _lstPrioridades.RemoveAll(z => z.intEstudioID == Convert.ToInt32(_id));
                    }
                    else
                    {
                        _mdlPrio.intEstudioID = Convert.ToInt32(_id);
                        if (_lstGlo.Exists(x => x.intEstudioID == _mdlPrio.intEstudioID))
                        {
                            _mdlPrio.intSecuencia = _lstGlo.First(x => x.intEstudioID == _mdlPrio.intEstudioID).intSecuencia;
                        }
                        _mdlPrio.bitUrgente = cb1.Checked;
                        _mdlPrio.bitAtendido = false;
                        _mdlPrio.datAtendido = null;
                        _mdlPrio.datFecha = DateTime.Now;
                        _mdlPrio.vchusuarioSol = Session["UserID"].ToString();
                        if (_lstGlo.First(x => x.vchAccessionNumber == _numEst).bitUrgente != cb1.Checked)
                            _lstPrioridades.Add(_mdlPrio);
                    }
                }
            }
            catch (Exception chk)
            {
                Log.EscribeLog("Existe un error en ckhPrioridad_CheckedChanged: " + chk.Message);
                ShowMessage("Existe un error al guardar la prioridad: " + chk.Message, MessageType.Error, "alert_container");
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
                this.cargaGridAdministracion();
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
                this.cargaGridAdministracion();
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_container");
            }
        }

        protected void chkAutomatico_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAutomatico.Checked)
                {
                    btnGuardarCambios.Visible = false;
                    autoguardar();
                }
                else
                {
                    btnGuardarCambios.Visible = true;
                }
            }
            catch (Exception eckAut)
            {
                Log.EscribeLog("Existe un error en chkAutomatico_CheckedChanged: " + eckAut.Message);
                throw eckAut;
            }
        }

        private void autoguardar()
        {
            try
            {
                if (_lstPrioridades != null)
                {
                    if (_lstPrioridades.Count > 0)
                    {
                        EstudioRequest request = new EstudioRequest();
                        String _error = "";
                        foreach (tbl_MST_PrioridadEstudio _ope in _lstPrioridades)
                        {

                            request._mdlPrioridad = _ope;
                            request.Token = Session["Token"].ToString();
                            request.intUsuarioID = Session["intUsuarioID"].ToString();
                            request.vchUsuario = Session["UserID"].ToString();
                            request.vchPassword = Session["Password"].ToString();
                            EstudioResponse response = new EstudioResponse();
                            response = NapoleonDA.setPrioridadEstudio(request);
                            if (response._mensaje != null)
                            {
                                if (response._mensaje.vchError != "")
                                    _error += "- Existe un error al actualizar los datos del Estudio con ID: " + _ope.intEstudioID + ". Error : " + response._mensaje.vchError + ". ";
                            }
                            else
                                _error += "- Existe un error al actualizar los datos del Estudio con ID: " + _ope.intEstudioID + ". Error : " + response._mensaje.vchError + ". ";

                        }

                        if (_error == "")
                        {
                            //Todo Correcto
                            cargaGridAdministracion();
                            ShowMessage("Cambios guardados correctamente.", MessageType.Correcto, "alert_container");
                        }
                        else
                        {
                            ShowMessage("Existen los siguientes errores: " + _error + ".", MessageType.Correcto, "alert_container");
                        }
                        _lstPrioridades.Clear();
                    }
                }
            }
            catch (Exception eAuto)
            {
                Log.EscribeLog("Existe un error en autoguardar: " + eAuto.Message);
                throw eAuto;
            }
        }

        protected void ddlBusModalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargaGridAdministracion();
            }
            catch (Exception eB)
            {
                Log.EscribeLog("Existe un error en ddlBusModalidad_SelectedIndexChanged: " + eB.Message);
            }
        }

        protected void ddlBusEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargaGridAdministracion();
            }
            catch (Exception eB)
            {
                Log.EscribeLog("Existe un error en ddlBusEstatus_SelectedIndexChanged: " + eB.Message);
            }
        }

        protected void txtBusNumEstudio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargaGridAdministracion();
            }
            catch (Exception eB)
            {
                Log.EscribeLog("Existe un error en txtBusNumEstudio_TextChanged: " + eB.Message);
            }
        }

        protected void txtBusNombre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargaGridAdministracion();
            }
            catch (Exception eB)
            {
                Log.EscribeLog("Existe un error en txtBusNombre_TextChanged: " + eB.Message);
            }
        }

        protected void ddlBusPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargaGridAdministracion();
            }
            catch (Exception eB)
            {
                Log.EscribeLog("Existe un error en ddlBusPrioridad_SelectedIndexChanged: " + eB.Message);
            }
        }

        protected void btnBusLimp_Click(object sender, EventArgs e)
        {
            try
            {
                ddlBusModalidad.SelectedValue = "0";
                ddlBusEstatus.SelectedValue = "0";
                txtBusNumEstudio.Text = "";
                txtBusNombre.Text = "";
                ddlBusPrioridad.SelectedValue = "0";
                if (user.id_Sitio > 0)
                    ddlBusSucursal.SelectedValue = user.id_Sitio.ToString();
                else
                    ddlBusSucursal.SelectedValue = "0";
                cargaGridAdministracion();
            }
            catch (Exception eB)
            {
                Log.EscribeLog("Existe un error en btnBusLimp_Click: " + eB.Message);
            }
        }

        protected void ddlBusSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cargaGridAdministracion();
            }
            catch (Exception eB)
            {
                Log.EscribeLog("Existe un error en ddlBusSucursal_SelectedIndexChanged: " + eB.Message);
            }
        }
    }
}