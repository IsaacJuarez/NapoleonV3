using _1.FUJI.Napoleon.Entidades;
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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "INapoleonService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface INapoleonService
    {
        [OperationContract]
        LoginResponse Logear(LoginRequest Request);

        [OperationContract]
        List<tbl_ConfigSitio> getSitios();

        [OperationContract]
        List<tbl_CAT_Proyecto> getProyectos();

        [OperationContract]
        List<clsUsuario> getUsuarios();

        [OperationContract]
        List<tbl_CAT_TipoUsuario> getTipoUsuario();

        [OperationContract]
        List<tbl_REL_ProyectoSitio> getRELProyecto_Sitio(int intProyectoID);


        [OperationContract]
        ProyectoResponse setProyecto(ProyectoRequest request);

        [OperationContract]
        bool updateEstatusSitio(int id_Sitio, bool activo, ref string mensaje);

        [OperationContract]
        bool updateEstatusProyectos(int intProyectoID, bool activo, ref string mensaje);

        [OperationContract]
        bool updateEstatusUsuario(int intUsuarioID, bool activo, ref string mensaje);
    }
}
