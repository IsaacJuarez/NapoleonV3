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
    public partial class frmDashBoardService : System.Web.UI.Page
    {
        public string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["URL"];
            }
        }
        public int ParametroMinutos
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["ParametroMinutos"].ToString());
            }
        }

        public static int intTipoUsuario = 0;
        public static clsUsuario user = new clsUsuario();


        NapoleonService NapoleonDA = new NapoleonService();
        public static List<clsDashboardService> _lstGridGlobal = new List<clsDashboardService>();

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
                        cargaGrid();
                    }
                }
                else
                {
                    Response.Redirect(URL + "/frmLogin.aspx");
                }
            }
            catch (Exception ePL)
            {
                ShowMessage("Existe un error: " + ePL.Message, MessageType.Error, "alert_container");
            }
        }

        private void cargaGrid()
        {
            try
            {
                List<clsDashboardService> _lstGrid = new List<clsDashboardService>();
                if(intTipoUsuario == 1)
                {
                    user.intProyectoID = 0;
                    user.id_Sitio = 0;
                }
                else
                {
                    if(intTipoUsuario == 2)
                    {
                        user.id_Sitio = 0;
                    }
                    if (intTipoUsuario == 3)
                    {
                        user.intProyectoID = 0;
                    }
                }
                _lstGrid = NapoleonDA.getServicioSitio(user.intProyectoID,user.id_Sitio);
                if (_lstGrid != null)
                {
                    if (_lstGrid.Count > 0)
                    {
                        _lstGridGlobal = _lstGrid;
                        grvServicios.DataSource = _lstGrid;

                    }
                }
                else
                    _lstGridGlobal = null;
                grvServicios.DataBind();
            }
            catch (Exception ecg)
            {
                throw ecg;
            }
        }

        protected void grvServicios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    Label lblTotalNumDePaginas = (Label)e.Row.FindControl("lblBandejaTotal");
                    lblTotalNumDePaginas.Text = grvServicios.PageCount.ToString();

                    TextBox txtIrAlaPagina = (TextBox)e.Row.FindControl("txtBandeja");
                    txtIrAlaPagina.Text = (grvServicios.PageIndex + 1).ToString();

                    DropDownList ddlTamPagina = (DropDownList)e.Row.FindControl("ddlBandeja");
                    ddlTamPagina.SelectedValue = grvServicios.PageSize.ToString();
                }

                if (e.Row.RowType != DataControlRowType.DataRow)
                {
                    return;
                }

                clsDashboardService _mdl = new clsDashboardService();
                _mdl = e.Row.DataItem as clsDashboardService;
                if (_mdl.datFechaSCP != null)
                {
                    if (_mdl.datFechaSCP < DateTime.Now)
                    {
                        DateTime datIni = (DateTime)_mdl.datFechaSCP;
                        Double dif = DateTime.Now.Subtract(datIni).TotalSeconds;
                        if (dif > (ParametroMinutos * 60))
                        {
                            ((Image)e.Row.Cells[2].FindControl("imgLActivo")).ImageUrl = "~/Images/offline.png";
                        }
                        else
                        {
                            ((Image)e.Row.Cells[2].FindControl("imgLActivo")).ImageUrl = "~/Images/online.png";
                        }
                        string leyenda = tiempo2String(dif);
                        ((Label)e.Row.Cells[2].FindControl("lblTiempoTransL")).Text = "...hace " + leyenda;

                    }
                }
                else
                {
                    ((Label)e.Row.Cells[2].FindControl("lblTiempoTransL")).Text = "Sin información.";
                }
                if (_mdl.datFechaSCU != null)
                {
                    if (_mdl.datFechaSCU < DateTime.Now)
                    {
                        DateTime datIni = (DateTime)_mdl.datFechaSCU;
                        Double dif = DateTime.Now.Subtract(datIni).TotalSeconds;
                        if (dif > (ParametroMinutos * 60))
                        {
                            ((Image)e.Row.Cells[2].FindControl("imgHActivo")).ImageUrl = "~/Images/offline.png";
                        }
                        else
                        {
                            ((Image)e.Row.Cells[2].FindControl("imgHActivo")).ImageUrl = "~/Images/online.png";
                        }
                        string leyenda = tiempo2String(dif);
                        ((Label)e.Row.Cells[2].FindControl("lblTiempoTransH")).Text = "...hace " + leyenda;
                    }
                }
                else
                {
                    ((Label)e.Row.Cells[2].FindControl("lblTiempoTransH")).Text = "Sin información.";
                }
            }
            catch (Exception edb)
            {
                throw edb;
            }
        }

        private string tiempo2String(Double tiempo)
        {
            string leyenda = "";
            try
            {
                if (tiempo <= 59)
                {
                    leyenda = Convert.ToInt32(tiempo) + " segundos.";
                }
                else
                {
                    if (tiempo == 60)
                    {
                        leyenda = "1 minuto.";
                    }
                    else
                    {
                        double time = Convert.ToInt32(tiempo) / 60;
                        if (time <= 59)
                        {
                            leyenda = time + " minutos.";
                        }
                        else
                        {
                            if (time == 60)
                            {
                                leyenda = "1 hora.";
                            }
                            else
                            {
                                double timeHoras = Convert.ToInt32(time) / 60;
                                if (timeHoras <= 23)
                                {
                                    leyenda = Convert.ToInt32(timeHoras) + " horas.";
                                }
                                else
                                {
                                    if (timeHoras == 24)
                                    {
                                        leyenda = "1 día.";
                                    }
                                    else
                                    {
                                        double timeDias = timeHoras / 24;
                                        if (Convert.ToInt32(timeDias) == 1)
                                        {
                                            leyenda = "1 día.";
                                        }
                                        else
                                            leyenda = Convert.ToInt32(timeDias) + " días.";
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception et2S)
            {
                throw et2S;
            }
            return leyenda;
        }

        protected void grvServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                if (e.NewPageIndex >= 0)
                {
                    this.grvServicios.PageIndex = e.NewPageIndex;
                    cargaGrid();
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
                    this.grvServicios.AllowPaging = true;
                    this.grvServicios.PageSize = int.Parse(dropDownList.SelectedValue);
                }
                else
                    this.grvServicios.AllowPaging = false;
                this.cargaGrid();
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
                    this.grvServicios.PageIndex = numeroPagina - 1;
                else
                    this.grvServicios.PageIndex = 0;
                this.cargaGrid();
            }
            catch (Exception ex)
            {
                ShowMessage("Existe un error: " + ex.Message, MessageType.Error, "alert_container");
            }
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

    }
}