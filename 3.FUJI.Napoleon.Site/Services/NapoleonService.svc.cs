using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using _2.FUJI.Napoleon.AccesoDatos.Extensions;
using _3.FUJI.Napoleon.Site.Services.DataContracts;
using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;

namespace _3.FUJI.Napoleon.Site.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "NapoleonService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione NapoleonService.svc o NapoleonService.svc.cs en el Explorador de soluciones e inicie la depuración.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
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

        public List<tbl_ConfigSitio> getSitios(int intProyectoID, int id_Sitio)
        {
            List<tbl_ConfigSitio> _lstSitio = new List<tbl_ConfigSitio>();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                _lstSitio = controller.getSitios(intProyectoID, id_Sitio);
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

        public clsMensaje updateEstatusSitio(int id_Sitio, bool activo)
        {
            clsMensaje response = new clsMensaje();
            bool valido = false;
            try
            {
                string mensaje = "";
                NapoleonDataAccess controller = new NapoleonDataAccess();
                response.valido = controller.updateEstatusSitio(id_Sitio, activo, ref mensaje);
                response.vchMensaje = mensaje;
            }
            catch(Exception euS)
            {
                throw euS;
            }
            return response;
        }

        public clsMensaje updateEstatusProyectos(int intProyectoID, bool activo)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                string mensaje = "";
                response.valido = controller.updateEstatusProyectos(intProyectoID, activo, ref mensaje);
                response.vchMensaje = mensaje;
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return response;
        }

        public clsMensaje updateEstatusUsuario(int intUsuarioID, bool activo)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                string mensaje = "";
                response.valido = controller.updateEstatusUsuario(intUsuarioID, activo,ref mensaje);
                response.vchMensaje = mensaje;
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return response;
        }

        public clsMensaje setUsuario(tbl_CAT_Usuarios user)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                string mensaje = "";
                response.valido = controller.setUsuario(user,ref mensaje);
                response.vchMensaje = mensaje;
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return response;
        }

        public clsMensaje updateUsuario(tbl_CAT_Usuarios user)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                string mensaje = "";
                NapoleonDataAccess controller = new NapoleonDataAccess();
                response.valido = controller.updateUsuario(user, ref mensaje);
                response.vchMensaje = mensaje;
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return response;
        }
        
        public List<clsDashboardService> getServicioSitio(int intProyectoID, int id_Sitio)
        {
            List<clsDashboardService> valido = new List<clsDashboardService>();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                valido = controller.getServicioSitio(intProyectoID,id_Sitio);
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return valido;
        }

        public bool validarSitio(string vchClaveSitio)
        {
            bool valida;
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                valida = controller.validarSitio(vchClaveSitio);
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return valida;
        }

        public clsMensaje setSitio(tbl_ConfigSitio mdlSitio, tbl_RegistroSitio mdlRegistro)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                string mensaje = "";
                response.valido = controller.setSitio(mdlSitio, mdlRegistro, ref mensaje);
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return response;
        }

        public clsMensaje getListEstudios(int intEstatusID, int id_Sitio, int intModalidadID)
        {
            clsMensaje response = new clsMensaje();
            List<clsEstudio> lst = new List<clsEstudio>();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                string mensaje = "";
                lst = controller.getListEstudios(intEstatusID, id_Sitio, intModalidadID,ref mensaje);
                response.vchMensaje = mensaje;
                response._lstEst = lst;
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return response;
        }

        public List<clsModeloCatalogo> getCatalogo(String _TipoCat, int intProyecto, int id_Sitio)
        {
            List<clsModeloCatalogo> lst = new List<clsModeloCatalogo>();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                lst = controller.getCatalogo(_TipoCat, intProyecto, id_Sitio);
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return lst;
        }

        public EstudioResponse setPrioridadEstudio(EstudioRequest Request)
        {
            EstudioResponse _estudioResponse = new EstudioResponse();
            try
            {
                if (Security.ValidateToken(Request.Token, Request.intUsuarioID, Request.vchUsuario, Request.vchPassword))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    _estudioResponse._mensaje = controller.updatePrioridadEstudio(Request._mdlPrioridad);
                }
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return _estudioResponse;
        }

        public PrioridadResponse setPrioridad(PrioridadRequest _resp)
        {
            PrioridadResponse _prioResponse = new PrioridadResponse();
            try
            {
                if (Security.ValidateToken(_resp.Token, _resp.intUsuarioID, _resp.vchUsuario, _resp.vchPassword))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    _prioResponse._mensaje.vchMensaje = controller.setPrioridad(_resp.intEstudioID, _resp.intDireccion, _resp.intSecuenciaActual);
                }
            }
            catch (Exception egV)
            {
                _prioResponse._mensaje.vchError = egV.Message;
            }
            return _prioResponse;
        }

        public tbl_RegistroSitio getRegistroContacto(int id_Sitio)
        {
            tbl_RegistroSitio _response = new tbl_RegistroSitio();
            try
            {
                //if (Security.ValidateToken(_resp.Token, _resp.intUsuarioID, _resp.vchUsuario, _resp.vchPassword))
                //{
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    _response = controller.getRegistroContacto(id_Sitio);
                //}
            }
            catch (Exception egV)
            {
                _response = null;
                Log.EscribeLog("Existe un error en getRegistroContacto: " + egV.Message);
            }
            return _response;
        }
        
        public List<clsPrioridadSucursal> getPrioridadSucursal(int id_Sitio, int intProyectoID)
        {
            List<clsPrioridadSucursal> _response = new List<clsPrioridadSucursal>();
            try
            {
                //if (Security.ValidateToken(_resp.Token, _resp.intUsuarioID, _resp.vchUsuario, _resp.vchPassword))
                //{
                NapoleonDataAccess controller = new NapoleonDataAccess();
                _response = controller.getPrioridadSucursal(id_Sitio, intProyectoID);
                //}
            }
            catch (Exception egV)
            {
                _response = null;
                Log.EscribeLog("Existe un error en getRegistroContacto: " + egV.Message);
            }
            return _response;
        }

        public PrioridadResponse setPrioridadesSucModAcomodar(PrioridadRequest _resp)
        {
            PrioridadResponse _prioResponse = new PrioridadResponse();
            try
            {
                if (Security.ValidateToken(_resp.Token, _resp.intUsuarioID, _resp.vchUsuario, _resp.vchPassword))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    _prioResponse._mensaje.vchMensaje = controller.setPrioridadesSucModAcomodar(_resp.intEstudioID, _resp.intDireccion, _resp.intSecuenciaActual);
                }
            }
            catch (Exception egV)
            {
                _prioResponse._mensaje.vchError = egV.Message;
            }
            return _prioResponse;
        }

        public clsMensaje updateEstatusPrioridadModalidad(PrioridadModalidadRequest Request)
        {
            clsMensaje _userResponse = new clsMensaje();
            try
            {
                if (Security.ValidateToken(Request.Token, Request.intUsuarioID, Request.vchUsuario, Request.vchPassword))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    _userResponse = controller.updateEstatusPrioridadModalidad(Request.mdlPrioridad);
                }
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return _userResponse;
        }

        public clsMensaje setPrioridadesSucMod(PrioridadSucModRequest Request)
        {
            clsMensaje _userResponse = new clsMensaje();
            try
            {
                if (Security.ValidateToken(Request.Token, Request.intUsuarioID, Request.vchUsuario, Request.vchPassword))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    _userResponse.vchMensaje = controller.setPrioridadesSucMod(Request.mosID, Request.activar);
                }
            }
            catch (Exception egV)
            {
                _userResponse.vchError = egV.Message;
            }
            return _userResponse;
        }

        public List<clsEntidadTabla> getDatosTabla(DateTime FechaIncio, DateTime FechaFin, int sucOID)
        {
            List<clsEntidadTabla> valida;
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                valida = controller.getDatosTabla(FechaIncio, FechaFin, sucOID);
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return valida;
        }

        public List<clsGrafica> getDatosGraficas(String tipo, DateTime FechaIncio, DateTime FechaFin, int sucOID)
        {
            List<clsGrafica> valida;
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                valida = controller.getDatosGraficas(tipo, FechaIncio, FechaFin, sucOID);
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return valida;
        }

        public clsTop getDatosTop(DateTime fini, DateTime ffin, int sucOID)
        {
            clsTop NumEst = new clsTop();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                NumEst = controller.getDatosTop(fini, ffin, sucOID);
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return NumEst;
        }

        public string getPromedioEnvio(DateTime FechaIncio, DateTime FechaFin, int sucOID)
        {
            string NumEst = "";
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                NumEst = controller.getPromedioEnvio(FechaIncio, FechaFin, sucOID);
            }
            catch (Exception egV)
            {
                NumEst = null;
            }
            return NumEst;
        }
    }
}
