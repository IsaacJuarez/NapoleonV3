using _2.FUJI.Napoleon.AccesoDatos.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _3.FUJI.Napoleon.Site
{
    public partial class Site : System.Web.UI.MasterPage
    {
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
                if (!IsPostBack)
                {
                    if (Session["UserID"] == null || Session["UserID"].ToString() == "")
                    {
                        Session["UserID"] = HttpContext.Current.User.Identity.Name.Substring(HttpContext.Current.User.Identity.Name.IndexOf(@"\") + 1);
                        hfURL.Value = URL;
                    }
                    lblUser.Text = Session["UserID"].ToString();
                }
            }
            catch(Exception ePL)
            {
                Log.EscribeLog("Existe un error en PageLoad de SiteMaster: " + ePL.Message);
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                Response.Redirect(URL + "/frmLogin.aspx", false);
            }
            catch (Exception ebc)
            {
                throw ebc;
            }
        }
    }
}