using _1.FUJI.Napoleon.Entidades;
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
    public partial class frmAdministracion : System.Web.UI.Page
    {
        NapoleonService NapoleonDA = new NapoleonService();
        private static List<clsEstudio> _lstGlo = new List<clsEstudio>();
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
            if (Session["UserID"] != null && Session["UserID"].ToString() != "" && Session["tbl_CAT_Usuarios"] != null &&
                Security.ValidateToken(Session["Token"].ToString(), Session["intUsuarioID"].ToString(), Session["UserID"].ToString(), Session["Password"].ToString()))
            {
                if (!IsPostBack)
                {
                    //_lstPrioridades = new List<opePrioridad>();
                    intTipoUsuario = Convert.ToInt32(Session["intTipoUsuario"].ToString());
                    user = (clsUsuario)Session["tbl_CAT_Usuarios"];
                    cargaGridAdministracion();
                    inicializaCombos();
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
                _lstCat = NapoleonDA.getCatalogo("Estatus");
                ddlBusEstatus.DataSource = _lstCat;
                ddlBusEstatus.DataTextField = "vchDescripcion";
                ddlBusEstatus.DataValueField = "vchValue";
                ddlBusEstatus.DataBind();
                ddlBusEstatus.Items.Insert(0, new ListItem("Todos...", "0"));
                //Combo de Sucursales
                _lstCat = null;
                _lstCat = NapoleonDA.getCatalogo("Sucursales");
                ddlBusSucursal.DataSource = _lstCat;
                ddlBusSucursal.DataTextField = "vchDescripcion";
                ddlBusSucursal.DataValueField = "vchValue";
                ddlBusSucursal.DataBind();
                ddlBusSucursal.Items.Insert(0, new ListItem("Todas...", "0"));
                //Combo de Prioridad
                _lstCat = null;
                _lstCat = NapoleonDA.getCatalogo("Prioridad");
                ddlBusPrioridad.DataSource = _lstCat;
                ddlBusPrioridad.DataTextField = "vchDescripcion";
                ddlBusPrioridad.DataValueField = "vchValue";
                ddlBusPrioridad.DataBind();
                ddlBusPrioridad.Items.Insert(0, new ListItem("Todas...", "0"));
                //Combo de Modalidad
                _lstCat = null;
                _lstCat = NapoleonDA.getCatalogo("Modalidades");
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
                string mensaje = "";
                int intModalidadID = 0;
                int intEstatusID = 0;
                int id_Sitio = 0;
                _lstGrid = NapoleonDA.getListEstudios(intEstatusID,id_Sitio, intModalidadID,ref mensaje);
                if (_lstGrid.Count > 0)
                {
                    _lstGlo = _lstGrid.Where(x => x.vchAccessionNumber.ToUpper().Contains(txtBusNumEstudio.Text.ToUpper())
                    //&& x.EstatusID != EstatusCompleto
                    && x.PatientName.ToUpper().Contains(txtBusNombre.Text.ToUpper())).ToList();

                    ////Prioridad
                    //if (ddlBusPrioridad.SelectedValue != "")
                    //    if (Convert.ToInt32(ddlBusPrioridad.SelectedValue) > 0)
                    //        _lstGlo = _lstGlo.Where(z => z.priUrgente == (ddlBusPrioridad.SelectedValue == "1" ? false : true)).ToList();

                    //Estatus
                    if (ddlBusEstatus.SelectedValue != "")
                        if (Convert.ToInt32(ddlBusEstatus.SelectedValue) > 0)
                            _lstGlo = _lstGlo.Where(z => z.intEstatusID == Convert.ToInt32(ddlBusEstatus.SelectedValue)).ToList();

                    // Sucursal
                    if (ddlBusSucursal.SelectedValue != "")
                        if (Convert.ToInt32(ddlBusSucursal.SelectedValue) > 0)
                            _lstGlo = _lstGlo.Where(z => z.id_Sitio == Convert.ToInt32(ddlBusSucursal.SelectedValue)).ToList();

                    // Modalidad
                    if (ddlBusModalidad.SelectedValue != "")
                        if (Convert.ToInt32(ddlBusModalidad.SelectedValue) > 0)
                            _lstGlo = _lstGlo.Where(z => z.intModalidadID == Convert.ToInt32(ddlBusModalidad.SelectedValue)).ToList();

                    List<clsEstudio> _lstGridOrder = new List<clsEstudio>();
                    //int order = 0;
                    //foreach (clsEstudio mdl in _lstGlo)
                    //{
                    //    if (mdl.priID > 0 && mdl.priSecuencia > 0)
                    //        order++;
                    //}
                    //if (order > 0)
                    //{
                    //List<clsEstudio> _lstGridConPrioridad = new List<clsEstudio>();
                    //List<clsEstudio> _lstGridSinPrioridad = new List<clsEstudio>();
                    //_lstGridConPrioridad = _lstGlo.Where(z => z.priID > 0 && z.priSecuencia > 0).ToList();
                    //_lstGridSinPrioridad = _lstGlo.Where(z => z.priID == 0 || z.priSecuencia == 0).ToList();
                    //_lstGridOrder = _lstGridConPrioridad.OrderBy(x => x.priSecuencia).ToList();
                    //SecMax = _lstGridConPrioridad.Max(x => x.priSecuencia);
                    //_lstGridOrder.AddRange(_lstGridSinPrioridad);
                    //_lstPrioridades.Clear();
                    //}
                    //else
                    //    _lstGridOrder = _lstGlo.OrderByDescending(x => x.id).ToList();
                    _lstGridOrder = _lstGlo;
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

        }

        protected void btnBusquedaEst_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {

        }

        protected void grvBusqueda_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grvBusqueda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grvBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void ckhPrioridad_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void ddlBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtBandeja_TextChanged(object sender, EventArgs e)
        {

        }
    }
}