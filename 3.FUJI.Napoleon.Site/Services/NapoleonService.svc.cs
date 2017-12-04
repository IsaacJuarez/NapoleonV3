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
                Response.Success = controller.Logear(Request.username, Request.password, Request.vchSitio, ref entidad);
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
                if (Security.ValidateToken(request.Token, request.intUsuarioID, request.vchUsuario, request.vchPassword))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    response.success = controller.setProyecto(request.mdlProyecto, request.lstSitos, ref mensaje);
                    response.mensaje = mensaje;
                }
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
                if (Security.ValidateToken(request.Token, request.intUsuarioID, request.vchUsuario, request.vchPassword))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    response.success = controller.updateProyecto(request.mdlProyecto, request.lstSites, ref mensaje);
                    response.mensaje = mensaje;
                }
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

        public clsMensaje updateEstatusFiles(int intVersionID, bool activo)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                string mensaje = "";
                NapoleonDataAccess controller = new NapoleonDataAccess();
                response.valido = controller.updateEstatusFiles(intVersionID, activo, ref mensaje);
                response.vchMensaje = mensaje;
            }
            catch (Exception euS)
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

        public clsMensaje updateEstatusUsuario(UserRequest request)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                if (Security.ValidateToken(request.Token, request.intUsuarioID, request.vchUsuario, request.vchPassword))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    string mensaje = "";
                    response.valido = controller.updateEstatusUsuario(request.user.intUsuarioID, request.user.bitActivo, ref mensaje);
                    response.vchMensaje = mensaje;
                }
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return response;
        }

        public clsMensaje updatePassword(int intUsuarioID, string vchPassword, bool SolRe)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                string mensaje = "";
                response.valido = controller.updatePassword(intUsuarioID, vchPassword, SolRe, ref mensaje);
                response.vchMensaje = mensaje;
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return response;
        }

        public clsMensaje setActualizaUser(UserRequest request)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                if (Security.ValidateToken(request.Token, request.intUsuarioID, request.vchUsuario, request.vchPassword))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    string mensaje = "";
                    response.valido = controller.setActualizaUser(request.user, ref mensaje);
                    response.vchMensaje = mensaje;
                }
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return response;
        }

        public clsMensaje setFileVersion(FileFeed2Request request)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                if (Security.ValidateToken(request.Token, request.intUsuarioID, request.vchUsuario, request.vchPassword))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    string mensaje = "";
                    response.valido = controller.setFileVersion(request.file, ref mensaje);
                    response.vchMensaje = mensaje;
                }
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return response;
        }

        public List<tbl_CAT_Feed2Version> getListaArchivos()
        {
            List<tbl_CAT_Feed2Version> response = new List<tbl_CAT_Feed2Version>();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                response = controller.getListaArchivos();
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return response;
        }
        
        public clsMensaje setUsuario(UserRequest request)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                string mensaje = "";
                if (Security.ValidateToken(request.Token, request.intUsuarioID, request.vchUsuario, request.vchPassword))
                {
                    response.valido = controller.setUsuario(request.usuario, ref mensaje);
                    response.vchMensaje = mensaje;
                }
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return response;
        }

        public clsMensaje updateUsuario(UserRequest request)
        {
            clsMensaje response = new clsMensaje();
            try
            {
                string mensaje = "";
                NapoleonDataAccess controller = new NapoleonDataAccess();
                if (Security.ValidateToken(request.Token, request.intUsuarioID, request.vchUsuario, request.vchPassword))
                {
                    response.valido = controller.updateUsuario(request.usuario, ref mensaje);
                    response.vchMensaje = mensaje;
                }
            }
            catch (Exception euS)
            {
                throw euS;
            }
            return response;
        }

        public LoginResponse getNewPassword(string vchCorreo)
        {
            LoginResponse response = new LoginResponse();
            try
            {
                string mensaje = "";
                NapoleonDataAccess controller = new NapoleonDataAccess();
                clsUsuario mdl = new clsUsuario();
                response.Success = controller.getNewPassword(vchCorreo, ref mensaje, ref mdl);
                response.CurrentUser = mdl;
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

        public clsMensaje getListEstudios(int intEstatusID, int id_Sitio, int intModalidadID, int intProyectoID)
        {
            clsMensaje response = new clsMensaje();
            List<clsEstudio> lst = new List<clsEstudio>();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                string mensaje = "";
                lst = controller.getListEstudios(intEstatusID, id_Sitio, intModalidadID, intProyectoID, ref mensaje);
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

        public List<clsEntidadTabla> getDatosTabla(DateTime FechaIncio, DateTime FechaFin, int sucOID, int intProyectoID)
        {
            List<clsEntidadTabla> valida;
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                valida = controller.getDatosTabla(FechaIncio, FechaFin, sucOID, intProyectoID);
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return valida;
        }

        public List<clsGrafica> getDatosGraficas(String tipo, DateTime FechaIncio, DateTime FechaFin, int sucOID, int intProyectoID)
        {
            List<clsGrafica> valida;
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                valida = controller.getDatosGraficas(tipo, FechaIncio, FechaFin, sucOID, intProyectoID);
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return valida;
        }

        public clsTop getDatosTop(DateTime fini, DateTime ffin, int sucOID, int intProyectoID)
        {
            clsTop NumEst = new clsTop();
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                NumEst = controller.getDatosTop(fini, ffin, sucOID, intProyectoID);
            }
            catch (Exception egV)
            {
                throw egV;
            }
            return NumEst;
        }

        public string getPromedioEnvio(DateTime FechaIncio, DateTime FechaFin, int sucOID, int intProyectoID)
        {
            string NumEst = "";
            try
            {
                NapoleonDataAccess controller = new NapoleonDataAccess();
                NumEst = controller.getPromedioEnvio(FechaIncio, FechaFin, sucOID, intProyectoID);
            }
            catch (Exception egV)
            {
                NumEst = null;
            }
            return NumEst;
        }

        #region feed2Cliente
        public ClienteF2CResponse getConeccion(ClienteF2CRequest request)
        {
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                if (Security.ValidateTokenSitio(request.Token, request.id_Sitio.ToString(), request.vchClaveSitio))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    response.ConfigSitio = controller.getConeccion(request.vchClaveSitio);
                    response.valido = true;
                }
                else
                {
                    response.valido = false;
                    response.message = "Los datos de validación son erroneos.";
                }
            }
            catch (Exception getC)
            {
                throw getC;
            }
            return response;
        }

        public ClienteF2CResponse setService(ClienteF2CRequest request)
        {
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                if (Security.ValidateTokenSitio(request.Token, request.id_Sitio.ToString(), request.vchClaveSitio))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    string mensaje = "";
                    response.valido = controller.setService(request.id_Sitio, request.vchClaveSitio, request.tipoServicio, ref mensaje);
                    response.message = mensaje;
                }
                else
                {
                    response.valido = false;
                    response.message = "Los datos de validación son erroneos.";
                }
            }
            catch (Exception getC)
            {
                Log.EscribeLog("Existe un error en el servicio setService: " + getC.Message);
            }
            return response;
        }

        public ClienteF2CResponse setEstudioServer(ClienteF2CRequest request)
        {
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                if (Security.ValidateTokenSitio(request.Token, request.id_Sitio.ToString(), request.vchClaveSitio))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    string mensaje = "";
                    response.valido = controller.setEstudioServer(request.estudio, ref mensaje);
                    response.message = mensaje;
                }
                else
                {
                    response.valido = false;
                    response.message = "Los datos de validación son erroneos.";
                }
            }
            catch (Exception getC)
            {
                response.valido = false;
                Log.EscribeLog("Existe un error en el servicio setEstudioServer: " + getC.Message);
            }
            return response;
        }

        public ClienteF2CResponse getEstudiosEnviar(ClienteF2CRequest request)
        {
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                if (Security.ValidateTokenSitio(request.Token, request.id_Sitio.ToString(), request.vchClaveSitio))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    string mensaje = "";
                    response.lstEstudio = controller.getEstudiosEnviar(request.id_Sitio, ref mensaje);
                    response.message = mensaje;
                }
                else
                {
                    response.valido = false;
                    response.message = "Los datos de validación son erroneos.";
                }
            }
            catch (Exception getC)
            {
                response.valido = false;
                response.message = "Existe un error en el servicio getEstudiosEnviar: " + getC.Message;
                Log.EscribeLog("Existe un error en el servicio getEstudiosEnviar: " + getC.Message);
            }
            return response;
        }

        public ClienteF2CResponse updateEstatus(ClienteF2CRequest request)
        {
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                if (Security.ValidateTokenSitio(request.Token, request.id_Sitio.ToString(), request.vchClaveSitio))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    string mensaje = "";
                    response.valido = controller.updateEstatus(request.intDetEstudioID, ref mensaje);
                    response.message = mensaje;
                }
                else
                {
                    response.valido = false;
                    response.message = "Los datos de validación son erroneos.";
                }
            }
            catch (Exception getC)
            {
                response.valido = false;
                response.message = "Existe un error en el servicio updateEstatus: " + getC.Message;
                Log.EscribeLog("Existe un error en el servicio updateEstatus: " + getC.Message);
            }
            return response;
        }

        public ClienteF2CResponse getVerificaSitio(ClienteF2CRequest request)
        {
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                if (Security.ValidateTokenSitio(request.Token, request.id_Sitio.ToString(), request.vchClaveSitio))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    string mensaje = "";
                    response.valido = controller.getVerificaSitio(request.vchClaveSitio, ref mensaje);
                    response.message = mensaje;
                }
                else
                {
                    response.valido = false;
                    response.message = "Los datos de validación son erroneos.";
                }
            }
            catch (Exception getC)
            {
                response.valido = false;
                response.message = "Existe un error en el servicio getVerificaSitio: " + getC.Message;
                Log.EscribeLog("Existe un error en el servicio getVerificaSitio: " + getC.Message);
            }
            return response;
        }

        public ClienteF2CResponse setConfiguracion(ClienteF2CRequest request)
        {
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                if (Security.ValidateTokenSitio(request.Token, request.id_Sitio.ToString(), request.vchClaveSitio))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    string mensaje = "";
                    int id_Sitio = 0;
                    response.valido = controller.setConfiguracion(request.mdlConfig, ref mensaje, ref id_Sitio);
                    response.id_Sitio = id_Sitio;
                    response.message = mensaje;
                }
                else
                {
                    response.valido = false;
                    response.message = "Los datos de validación son erroneos.";
                }
            }
            catch (Exception getC)
            {
                response.valido = false;
                response.message = "Existe un error en el servicio setConfiguracion: " + getC.Message;
                Log.EscribeLog("Existe un error en el servicio setConfiguracion: " + getC.Message);
            }
            return response;
        }

        public ClienteF2CResponse updateConfiguracion(ClienteF2CRequest request)
        {
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                if (Security.ValidateTokenSitio(request.Token, request.id_Sitio.ToString(), request.vchClaveSitio))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    string mensaje = "";
                    int id_Sitio = 0;
                    response.valido = controller.updateConfiguracion(request.mdlConfiguracion, ref mensaje);
                    response.id_Sitio = id_Sitio;
                    response.message = mensaje;
                }
                else
                {
                    response.valido = false;
                    response.message = "Los datos de validación son erroneos.";
                }
            }
            catch (Exception getC)
            {
                response.valido = false;
                response.message = "Existe un error en el servicio updateConfiguracion: " + getC.Message;
                Log.EscribeLog("Existe un error en el servicio updateConfiguracion: " + getC.Message);
            }
            return response;
        }

        public ClienteF2CResponse updateConfiguracionServer(ClienteF2CRequest request)
        {
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                if (Security.ValidateTokenSitio(request.Token, request.id_Sitio.ToString(), request.vchClaveSitio))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    string mensaje = "";
                    int id_Sitio = 0;
                    response.valido = controller.updateConfiguracionServer(request.mdlConfiguracion, ref mensaje);
                    response.id_Sitio = id_Sitio;
                    response.message = mensaje;
                }
                else
                {
                    response.valido = false;
                    response.message = "Los datos de validación son erroneos.";
                }
            }
            catch (Exception getC)
            {
                response.valido = false;
                response.message = "Existe un error en el servicio updateConfiguracionServer: " + getC.Message;
                Log.EscribeLog("Existe un error en el servicio updateConfiguracionServer: " + getC.Message);
            }
            return response;
        }

        public ClienteF2CResponse getXMLFileConfig(ClienteF2CRequest request)
        {
            ClienteF2CResponse response = new ClienteF2CResponse();
            try
            {
                if (Security.ValidateTokenSitio(request.Token, request.id_Sitio.ToString(), request.vchClaveSitio))
                {
                    NapoleonDataAccess controller = new NapoleonDataAccess();
                    response.vchFormato = controller.getXMLFileConfig(request.vchPassword);
                    response.valido = true;
                }
                else
                {
                    response.valido = false;
                    response.message = "Los datos de validación son erroneos.";
                }
            }
            catch (Exception getC)
            {
                response.valido = false;
                response.message = "Existe un error en el servicio getXMLFileConfig: " + getC.Message;
                Log.EscribeLog("Existe un error en el servicio getXMLFileConfig: " + getC.Message);
            }
            return response;
        }
        #endregion feed2Clinete
    }
}
