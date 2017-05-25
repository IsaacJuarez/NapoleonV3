using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using _3.FUJI.Napoleon.Site.Services.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace _3.FUJI.Napoleon.Site.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "NapoleonService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione NapoleonService.svc o NapoleonService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class NapoleonService : INapoleonService
    {
        public LoginResponse Logear(LoginRequest Request)
        {
            LoginResponse Response = new LoginResponse();
            NapoleonDataAccess controller = new NapoleonDataAccess();
            clsUsuario entidad = new clsUsuario();
            try
            {
                Response.Success = controller.Logear(Request.username, Request.password, ref entidad);
                Response.CurrentUser = entidad;
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return Response;
        }

        public List<tbl_ConfigSitio> getSitios()
        {
            List<tbl_ConfigSitio> _lstSitio = new List<tbl_ConfigSitio>();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                _lstSitio = controller.getSitios();
            }
            catch (Exception egS)
            {
                throw egS;
            }
            return _lstSitio;
        }

        public List<tbl_CAT_Proyecto> getProyectos()
        {
            List<tbl_CAT_Proyecto> _lstSitio = new List<tbl_CAT_Proyecto>();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                _lstSitio = controller.getProyectos();
            }
            catch (Exception egS)
            {
                throw egS;
            }
            return _lstSitio;
        }

        public List<clsUsuario> getUsuarios()
        {
            List<clsUsuario> _lstUser = new List<clsUsuario>();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                _lstUser = controller.getUsuarios();
            }
            catch (Exception egS)
            {
                throw egS;
            }
            return _lstUser;
        }

        public List<tbl_CAT_TipoUsuario> getTipoUsuario()
        {
            List<tbl_CAT_TipoUsuario> _lstTipoUser = new List<tbl_CAT_TipoUsuario>();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                _lstTipoUser = controller.getTipoUsuario();
            }
            catch (Exception egS)
            {
                throw egS;
            }
            return _lstTipoUser;
        }

        public List<tbl_REL_ProyectoSitio> getRELProyecto_Sitio(int intProyectoID)
        {
            List<tbl_REL_ProyectoSitio> _lstRELPS = new List<tbl_REL_ProyectoSitio>();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                _lstRELPS = controller.getRELProyecto_Sitio(intProyectoID);
            }
            catch (Exception egS)
            {
                throw egS;
            }
            return _lstRELPS;
        }

        public ProyectoResponse setProyecto(ProyectoRequest request) 
        {
            ProyectoResponse response = new ProyectoResponse();
            string mensaje = "";
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                response.success = controller.setProyecto(request.mdlProyecto, request.lstSitos, ref mensaje);
                response.mensaje = mensaje;
            }
            catch(Exception eSP)
            {
                throw eSP;
            }
            return response;
        }
        public ProyectoResponse updateProyecto(ProyectoRequest request)
        {
            ProyectoResponse response = new ProyectoResponse();
            string mensaje = "";
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                response.success = controller.updateProyecto(request.mdlProyecto, request.lstSites, ref mensaje);
                response.mensaje = mensaje;
            }
            catch (Exception eSP)
            {
                throw eSP;
            }
            return response;
        }

        public bool updateEstatusSitio(int id_Sitio, bool activo, ref string mensaje)
        {
            bool valido = false;
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                valido = controller.updateEstatusSitio(id_Sitio, activo, ref mensaje);
            }
            catch(Exception euS)
            {
                throw euS;
            }
            return valido;
        }

        public bool updateEstatusProyectos(int intProyectoID, bool activo, ref string mensaje)
        {
            bool valido = false;
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                valido = controller.updateEstatusProyectos(intProyectoID, activo, ref mensaje);
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return valido;
        }

        public bool updateEstatusUsuario(int intUsuarioID, bool activo, ref string mensaje)
        {
            bool valido = false;
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                valido = controller.updateEstatusUsuario(intUsuarioID, activo, ref mensaje);
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return valido;
        }
    }
}
