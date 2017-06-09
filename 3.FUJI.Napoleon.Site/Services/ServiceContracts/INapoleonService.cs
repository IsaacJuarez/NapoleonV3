using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using _3.FUJI.Napoleon.Site.Services.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace _3.FUJI.Napoleon.Site.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "INapoleonService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface INapoleonService
    {
        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        LoginResponse Logear(LoginRequest Request);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        List<tbl_ConfigSitio> getSitios(int intProyectoID, int id_Sitio);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        List<tbl_CAT_Proyecto> getProyectos();

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        List<clsUsuario> getUsuarios();

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        List<tbl_CAT_TipoUsuario> getTipoUsuario();

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        List<tbl_REL_ProyectoSitio> getRELProyecto_Sitio(int intProyectoID);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        ProyectoResponse setProyecto(ProyectoRequest request);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        clsMensaje updateEstatusSitio(int id_Sitio, bool activo);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        clsMensaje updateEstatusProyectos(int intProyectoID, bool activo);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        clsMensaje updateEstatusUsuario(int intUsuarioID, bool activo);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        clsMensaje setUsuario(tbl_CAT_Usuarios user);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        clsMensaje updateUsuario(tbl_CAT_Usuarios user);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        List<clsDashboardService> getServicioSitio(int intProyectoID, int id_Sitio);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        bool validarSitio(string vchClaveSitio);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        clsMensaje setSitio(tbl_ConfigSitio mdlSitio, tbl_RegistroSitio mdlRegistro);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        clsMensaje getListEstudios(int intEstatusID, int id_Sitio, int intModalidadID);


        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        List<clsModeloCatalogo> getCatalogo(String _TipoCat, int intProyecto, int id_Sitio);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        EstudioResponse setPrioridadEstudio(EstudioRequest Request);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        PrioridadResponse setPrioridad(PrioridadRequest _resp);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        tbl_RegistroSitio getRegistroContacto(int id_Sitio);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        PrioridadResponse setPrioridadesSucModAcomodar(PrioridadRequest _resp);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        clsMensaje updateEstatusPrioridadModalidad(PrioridadModalidadRequest Request);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        clsMensaje setPrioridadesSucMod(PrioridadSucModRequest Request);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        List<clsEntidadTabla> getDatosTabla(DateTime FechaIncio, DateTime FechaFin, int sucOID);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        List<clsGrafica> getDatosGraficas(String tipo, DateTime FechaIncio, DateTime FechaFin, int sucOID);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        clsTop getDatosTop(DateTime fini, DateTime ffin, int sucOID);

        [WebInvoke(Method = "POST",
           BodyStyle = WebMessageBodyStyle.WrappedRequest,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json
        )]
        [OperationContract]
        string getPromedioEnvio(DateTime FechaIncio, DateTime FechaFin, int sucOID);
    }
}
