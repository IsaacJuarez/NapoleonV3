using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using _2.FUJI.Napoleon.AccesoDatos.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.FUJI.Napoleon.AccesoDatos
{
    public class NapoleonDataAccess
    {
        string[] arrColor = new string[] { "#E57373", "#4DB6AC", "#4FC3F7", "#9575CD", "#AED581", "#FFF176", "#FF8A65", "#A1887F", "#90A4AE", "#FFB74D", "#DCE775" };
        string[] arrHover = new string[] { "#FFCDD2", "#B2DFDB", "#B3E5FC", "#D1C4E9", "#DCEDC8", "#FFF9C4", "#FFCCBC", "#D7CCC8", "#CFD8DC", "#FFE0B2", "#F0F4C3" };
        public NAPOLEONEntities NapoleonDA;
        #region login
        public bool Logear(string _Usuario, String _Password, ref clsUsuario _User)
        {
            bool Success = false;
            try
            {
                List<clsUsuario> _lstUser = new List<clsUsuario>();
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    if (NapoleonDA.tbl_CAT_Usuarios.Any(x => (bool)x.bitActivo && x.vchUsuario.Trim().ToUpper() == _Usuario.Trim().ToUpper() && x.vchPassword.Trim() == _Password.Trim()))
                    {
                        var query = NapoleonDA.tbl_CAT_Usuarios.First(x => x.vchUsuario.Trim().ToUpper() == _Usuario.Trim().ToUpper() && x.vchPassword.Trim() == _Password.Trim());
                        if (query != null)
                        {
                            if (Success = query.intUsuarioID > 0 ? true : false)
                            {
                                _User.intUsuarioID = query.intUsuarioID;
                                _User.intTipoUsuarioID = query.intTipoUsuarioID == null ? 0 : (int)query.intTipoUsuarioID;
                                _User.intProyectoID = query.intProyectoID == null ? 0 : (int)query.intProyectoID;
                                _User.id_Sitio = query.id_Sitio == null ? 0 : (int)query.id_Sitio;
                                _User.vchNombre = query.vchNombre;
                                _User.vchApellido = query.vchApellido;
                                _User.vchUsuario = query.vchUsuario;
                                _User.vchPassword = query.vchPassword;
                                _User.Token = Security.Encrypt(query.intUsuarioID + "|" + query.vchUsuario + "|" + query.vchPassword);
                            }
                        }
                    }
                    else
                    {
                        Success = false;
                    }
                }
            }
            catch (Exception eLogear)
            {
                Log.EscribeLog("Existe un error en NapoleonDataAccess.Logear: " + eLogear.Message);
                Success = false;
            }
            return Success;
        }


        #endregion login

        #region catalogos
        public List<tbl_ConfigSitio> getSitios(int intProyectoID, int id_Sitio)
        {
            List<tbl_ConfigSitio> _lstSitio = new List<tbl_ConfigSitio>();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    if(intProyectoID == 0 && id_Sitio == 0)
                    {
                        if (NapoleonDA.tbl_ConfigSitio.Any())
                        {
                            var query = NapoleonDA.tbl_ConfigSitio.ToList();
                            if (query != null)
                            {
                                if (query.Count > 0)
                                {
                                    _lstSitio.AddRange(query);
                                }
                            }
                        }
                    }
                    else
                    {
                        if(intProyectoID > 0)
                        {
                            if (NapoleonDA.tbl_REL_ProyectoSitio.Any(x => x.intProyectoID == intProyectoID))
                            {
                                var query = (from rel in NapoleonDA.tbl_REL_ProyectoSitio
                                             join config in NapoleonDA.tbl_ConfigSitio on rel.id_Sitio equals config.id_Sitio
                                             where rel.intProyectoID == intProyectoID
                                             select new
                                             {
                                                 id_sitio = config.id_Sitio,
                                                 bitActivo = config.bitActivo,
                                                 datFechaSistema = config.datFechaSistema,
                                                 intPuertoCliente = config.intPuertoCliente,
                                                 in_tPuertoServer = config.in_tPuertoServer,
                                                 vchAETitle = config.vchAETitle,
                                                 vchAETitleServer = config.vchAETitleServer,
                                                 vchClaveSitio = config.vchClaveSitio,
                                                 vchIPCliente = config.vchIPCliente,
                                                 vchIPServidor = config.vchIPServidor,
                                                 vchMaskCliente = config.vchMaskCliente,
                                                 vchNombreSitio = config.vchnombreSitio,
                                                 vchPathLocal = config.vchPathLocal,
                                                 vchUserAdmin = config.vchUserAdmin

                                             }).ToList();
                                if (query != null)
                                {
                                    if (query.Count > 0)
                                    {
                                        foreach (var item in query)
                                        {
                                            tbl_ConfigSitio mdl = new tbl_ConfigSitio();
                                            mdl.id_Sitio = (int)item.id_sitio;
                                            mdl.bitActivo = item.bitActivo;
                                            mdl.datFechaSistema = item.datFechaSistema;
                                            mdl.intPuertoCliente = item.intPuertoCliente;
                                            mdl.in_tPuertoServer = item.in_tPuertoServer;
                                            mdl.vchAETitle = item.vchAETitle;
                                            mdl.vchAETitleServer = item.vchAETitleServer;
                                            mdl.vchClaveSitio = item.vchClaveSitio;
                                            mdl.vchIPCliente = item.vchIPCliente;
                                            mdl.vchIPServidor = item.vchIPServidor;
                                            mdl.vchMaskCliente = item.vchMaskCliente;
                                            mdl.vchnombreSitio = item.vchNombreSitio;
                                            mdl.vchPathLocal = item.vchPathLocal;
                                            mdl.vchUserAdmin = item.vchUserAdmin;
                                            _lstSitio.Add(mdl);
                                        }
                                    }
                                }
                            }
                        }
                        if(id_Sitio > 0)
                        {
                            if (NapoleonDA.tbl_ConfigSitio.Any(x=>x.id_Sitio == id_Sitio))
                            {
                                var query = NapoleonDA.tbl_ConfigSitio.Where(x => x.id_Sitio == id_Sitio).ToList();
                                if (query != null)
                                {
                                    if (query.Count > 0)
                                    {
                                        _lstSitio.AddRange(query);
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
            catch (Exception egS)
            {
                _lstSitio = null;
                Log.EscribeLog("Existe un errror en getSitio: " + egS.Message);
            }
            return _lstSitio;
        }
        public List<tbl_CAT_Proyecto> getProyectos()
        {
            List<tbl_CAT_Proyecto> _lstProyecto = new List<tbl_CAT_Proyecto>();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    if (NapoleonDA.tbl_ConfigSitio.Any())
                    {
                        var query = NapoleonDA.tbl_CAT_Proyecto.ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                _lstProyecto.AddRange(query);
                            }
                        }
                    }
                }
            }
            catch (Exception egS)
            {
                _lstProyecto = null;
                Log.EscribeLog("Existe un errror en getProyectos: " + egS.Message);
            }
            return _lstProyecto;
        }

        public List<clsUsuario> getUsuarios()
        {
            List<clsUsuario> _lstUser = new List<clsUsuario>();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    if (NapoleonDA.tbl_CAT_Usuarios.Any())
                    {
                        var query = (from item in NapoleonDA.tbl_CAT_Usuarios
                                     join c in NapoleonDA.tbl_CAT_Proyecto on item.intProyectoID equals c.intProyectoID into CP
                                     join f in NapoleonDA.tbl_ConfigSitio on item.id_Sitio equals f.id_Sitio into fp
                                     from y1 in CP.DefaultIfEmpty()
                                     from y2 in fp.DefaultIfEmpty()
                                     select new
                                     {
                                         intUsuarioID = item.intUsuarioID,
                                         intTipoUsuarioID = item.intTipoUsuarioID,
                                         intProyectoID = item.intProyectoID,
                                         vchProyectoID = y1.vchProyectoDesc,
                                         vchNombre = item.vchNombre,
                                         vchApellido = item.vchApellido,
                                         vchUsuario = item.vchUsuario,
                                         vchPassword = item.vchPassword,
                                         bitActivo = item.bitActivo,
                                         datFecha = item.datFecha,
                                         vchUserAdmin = item.vchUserAdmin,
                                         id_Sitio = item.id_Sitio,
                                         vchSitio = y2.vchClaveSitio
                                     }).ToList();
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                clsUsuario user = new clsUsuario();
                                user.intUsuarioID = item.intUsuarioID;
                                user.intTipoUsuarioID = (int)item.intTipoUsuarioID;
                                user.intProyectoID = item.intProyectoID == null ? 0 : (int)item.intProyectoID;
                                user.vchProyectoID = item.vchProyectoID == null ? "" : item.vchProyectoID;
                                user.vchNombre = item.vchNombre;
                                user.vchApellido = item.vchApellido;
                                user.vchUsuario = item.vchUsuario;
                                user.vchPassword = item.vchPassword;
                                user.bitActivo = (bool)item.bitActivo;
                                user.datFecha = (DateTime)item.datFecha;
                                user.vchUserAdmin = item.vchUserAdmin;
                                user.id_Sitio = item.id_Sitio == null ? 0 : (int)item.id_Sitio;
                                user.vchSitio = item.vchSitio == null ? "" : item.vchSitio;
                                _lstUser.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception eGU)
            {
                _lstUser = null;
                Log.EscribeLog("Error en getUsuarios: " + eGU.Message);
            }
            return _lstUser;
        }

        public List<tbl_CAT_TipoUsuario> getTipoUsuario()
        {
            List<tbl_CAT_TipoUsuario> _lstTipoUser = new List<tbl_CAT_TipoUsuario>();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    if (NapoleonDA.tbl_CAT_TipoUsuario.Any(x => (bool)x.bitEstatus))
                    {
                        var query = NapoleonDA.tbl_CAT_TipoUsuario.Where(x => (bool)x.bitEstatus).ToList();
                        if (query != null)
                        {
                            if (query.Count > 0)
                            {
                                _lstTipoUser.AddRange(query);
                            }
                        }
                    }
                }
            }
            catch (Exception egtu)
            {
                _lstTipoUser = null;
                Log.EscribeLog("Existe un error en getTipoUsuario: " + egtu.Message);
            }
            return _lstTipoUser;
        }

        public List<tbl_REL_ProyectoSitio> getRELProyecto_Sitio(int intProyectoID)
        {
            List<tbl_REL_ProyectoSitio> _lstRel = new List<tbl_REL_ProyectoSitio>();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    if (intProyectoID > 0)
                    {
                        if (NapoleonDA.tbl_REL_ProyectoSitio.Any(x => (bool)x.bitActivo && x.intProyectoID == intProyectoID))
                        {
                            var query = NapoleonDA.tbl_REL_ProyectoSitio.Where(x => (bool)x.bitActivo && x.intProyectoID == intProyectoID).ToList();
                            if (query != null)
                            {
                                if (query.Count > 0)
                                {
                                    _lstRel.AddRange(query);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (NapoleonDA.tbl_REL_ProyectoSitio.Any(x => (bool)x.bitActivo))
                        {
                            var query = NapoleonDA.tbl_REL_ProyectoSitio.Where(x => (bool)x.bitActivo).ToList();
                            if (query != null)
                            {
                                if (query.Count > 0)
                                {
                                    _lstRel.AddRange(query);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception egRPS)
            {
                Log.EscribeLog("Existe un error en getRELProyecto_Sitio: " + egRPS.Message);
                _lstRel = null;
            }
            return _lstRel;
        }

        #endregion catalogos


        #region sets
        public bool setProyecto(tbl_CAT_Proyecto mdlProyecto, List<tbl_REL_ProyectoSitio> lstSitos, ref string mensaje)
        {
            bool valido = false;
            try
            {
                int intProyectoID = 0;
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    if (!NapoleonDA.tbl_CAT_Proyecto.Any(x => x.vchProyectoDesc.ToUpper() == mdlProyecto.vchProyectoDesc.ToUpper()))
                    {
                        NapoleonDA.tbl_CAT_Proyecto.Add(mdlProyecto);
                        NapoleonDA.SaveChanges();
                        intProyectoID = mdlProyecto.intProyectoID;
                        valido = true;
                    }
                    else
                    {
                        mensaje = "El nombre del Proyecto ya existe, favor de verificar.";
                        valido = false;
                    }
                }
                if (intProyectoID > 0)
                {
                    using (NapoleonDA = new NAPOLEONEntities())
                    {
                        foreach (tbl_REL_ProyectoSitio item in lstSitos)
                        {
                            item.intProyectoID = intProyectoID;
                            item.bitActivo = true;
                            item.datFecha = DateTime.Now;
                            NapoleonDA.tbl_REL_ProyectoSitio.Add(item);
                            NapoleonDA.SaveChanges();
                            valido = true;
                        }
                    }
                }
            }
            catch (Exception esP)
            {
                mensaje = esP.Message;
                Log.EscribeLog("Existe un error en setProyecto: " + esP.Message);
            }
            return valido;
        }


        public bool updateProyecto(tbl_CAT_Proyecto mdlProyecto, List<clsConfigSitio> lstSito, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    foreach (clsConfigSitio item in lstSito)
                    {
                        if (NapoleonDA.tbl_REL_ProyectoSitio.Any(x => x.intProyectoID == mdlProyecto.intProyectoID && x.id_Sitio == item.id_Sitio))
                        {
                            tbl_REL_ProyectoSitio mdl = NapoleonDA.tbl_REL_ProyectoSitio.First(x => x.intProyectoID == mdlProyecto.intProyectoID && x.id_Sitio == item.id_Sitio);
                            mdl.bitActivo = item.bitSeleccionado;
                            NapoleonDA.SaveChanges();
                        }
                        else
                        {
                            if (item.bitSeleccionado)
                            {
                                tbl_REL_ProyectoSitio mdls = new tbl_REL_ProyectoSitio();
                                mdls.intProyectoID = mdlProyecto.intProyectoID;
                                mdls.id_Sitio = item.id_Sitio;
                                mdls.bitActivo = true;
                                mdls.datFecha = DateTime.Now;
                                NapoleonDA.tbl_REL_ProyectoSitio.Add(mdls);
                                NapoleonDA.SaveChanges();
                            }
                        }
                    }
                    valido = true;
                }
            }
            catch (Exception esP)
            {
                mensaje = esP.Message;
                valido = false;
                Log.EscribeLog("Existe un error en updateProyecto: " + esP.Message);
            }
            return valido;
        }

        public bool updateEstatusSitio(int id_Sitio, bool activo, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    tbl_ConfigSitio mdl = NapoleonDA.tbl_ConfigSitio.First(x => x.id_Sitio == id_Sitio);
                    mdl.bitActivo = activo;
                    mdl.datFechaSistema = DateTime.Now;
                    NapoleonDA.SaveChanges();
                    valido = true;
                }
            }
            catch (Exception euES)
            {
                mensaje = euES.Message;
                valido = false;
                Log.EscribeLog("Existe un error en updateEstatusSitio: " + euES.Message);
            }
            return valido;
        }

        public bool updateEstatusProyectos(int intProyectoID, bool activo, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    tbl_CAT_Proyecto mdl = NapoleonDA.tbl_CAT_Proyecto.First(x => x.intProyectoID == intProyectoID);
                    mdl.bitActivo = activo;
                    mdl.datFecha = DateTime.Now;
                    NapoleonDA.SaveChanges();
                    valido = true;
                }
            }
            catch (Exception euES)
            {
                mensaje = euES.Message;
                valido = false;
                Log.EscribeLog("Existe un error en updateEstatusProyectos: " + euES.Message);
            }
            return valido;
        }

        public bool updateEstatusUsuario(int intUsuarioID, bool activo, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    tbl_CAT_Usuarios mdl = NapoleonDA.tbl_CAT_Usuarios.First(x => x.intUsuarioID == intUsuarioID);
                    mdl.bitActivo = activo;
                    mdl.datFecha = DateTime.Now;
                    NapoleonDA.SaveChanges();
                    valido = true;
                }
            }
            catch (Exception euES)
            {
                valido = false;
                mensaje = euES.Message;
                Log.EscribeLog("Existe un error en updateEstatusProyectos: " + euES.Message);
            }
            return valido;
        }

        public bool setUsuario(tbl_CAT_Usuarios user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    if (!NapoleonDA.tbl_CAT_Usuarios.Any(x => x.vchUsuario == user.vchUsuario))
                    {
                        NapoleonDA.tbl_CAT_Usuarios.Add(user);
                        NapoleonDA.SaveChanges();
                        valido = true;
                    }
                    else
                    {
                        mensaje = "Ya existe un usuario con el mismo nombre de usuario.";
                        valido = false;
                    }
                }
            }
            catch (Exception esU)
            {
                valido = false;
                Log.EscribeLog("Existe un error en setUsuario: " + esU.Message);
            }
            return valido;
        }

        public bool updateUsuario(tbl_CAT_Usuarios user, ref string mensaje)
        {
            bool valido = false;
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    tbl_CAT_Usuarios mdl = NapoleonDA.tbl_CAT_Usuarios.First(x => x.intUsuarioID == user.intUsuarioID);
                    mdl.vchNombre = user.vchNombre;
                    mdl.vchApellido = user.vchApellido;
                    mdl.vchPassword = user.vchPassword;
                    mdl.intTipoUsuarioID = user.intTipoUsuarioID;
                    mdl.intProyectoID = user.intProyectoID;
                    mdl.datFecha = DateTime.Now;
                    mdl.bitActivo = true;
                    mdl.id_Sitio = user.id_Sitio;
                    NapoleonDA.SaveChanges();
                    valido = true;
                }
            }
            catch (Exception esU)
            {
                valido = false;
                Log.EscribeLog("Existe un error en updateUsuario: " + esU.Message);
            }
            return valido;
        }



        #endregion sets

        #region dashboard
        public List<clsDashboardService> getServicioSitio(int intProyectoiD, int id_Sitio)
        {
            List<clsDashboardService> lstService = new List<clsDashboardService>();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    if (intProyectoiD > 0 || id_Sitio > 0)
                    {
                        if (intProyectoiD > 0)
                        {
                            if (NapoleonDA.tbl_REL_ProyectoSitio.Any(x => x.intProyectoID == intProyectoiD))
                            {
                                var query = (from rel  in NapoleonDA.tbl_REL_ProyectoSitio
                                             join item in NapoleonDA.tbl_DET_ServicioSitio on rel.id_Sitio equals item.id_Sitio
                                             join config in NapoleonDA.tbl_ConfigSitio on item.id_Sitio equals config.id_Sitio
                                             where rel.intProyectoID == intProyectoiD
                                             select new
                                             {
                                                 id_sitio = item.id_Sitio,
                                                 intProyectoiD = rel.intProyectoID,
                                                 vchClaveSitio = config.vchClaveSitio,
                                                 vchNombreSitio = config.vchnombreSitio,
                                                 datFechaSCP = item.datFechaSCP,
                                                 datFechaSCU = item.datFechaSCU,
                                                 datFechaSync = item.datFechaSync
                                             }).ToList();
                                if (query != null)
                                {
                                    if (query.Count > 0)
                                    {
                                        foreach (var item in query)
                                        {
                                            clsDashboardService mdl = new clsDashboardService();
                                            mdl.id_sitio = (int)item.id_sitio;
                                            mdl.vchClaveSitio = item.vchClaveSitio;
                                            mdl.vchNombreSitio = item.vchNombreSitio;
                                            mdl.datFechaSCP = (DateTime)item.datFechaSCP;
                                            mdl.datFechaSCU = (DateTime)item.datFechaSCU;
                                            mdl.datFechaSync = (DateTime)item.datFechaSync;
                                            lstService.Add(mdl);
                                        }
                                    }
                                }
                            }
                        }
                        if (id_Sitio > 0)
                        {
                            if (NapoleonDA.tbl_DET_ServicioSitio.Any())
                            {
                                var query = (from item in NapoleonDA.tbl_DET_ServicioSitio
                                             join config in NapoleonDA.tbl_ConfigSitio on item.id_Sitio equals config.id_Sitio
                                             where item.id_Sitio == id_Sitio
                                             select new
                                             {
                                                 id_sitio = item.id_Sitio,
                                                 vchClaveSitio = config.vchClaveSitio,
                                                 vchNombreSitio = config.vchnombreSitio,
                                                 datFechaSCP = item.datFechaSCP,
                                                 datFechaSCU = item.datFechaSCU
                                             }).ToList();

                                if (query != null)
                                {
                                    if (query.Count > 0)
                                    {
                                        foreach (var item in query)
                                        {
                                            clsDashboardService mdl = new clsDashboardService();
                                            mdl.id_sitio = (int)item.id_sitio;
                                            mdl.vchClaveSitio = item.vchClaveSitio;
                                            mdl.vchNombreSitio = item.vchNombreSitio;
                                            mdl.datFechaSCP = (DateTime)item.datFechaSCP;
                                            mdl.datFechaSCU = (DateTime)item.datFechaSCU;
                                            lstService.Add(mdl);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (NapoleonDA.tbl_DET_ServicioSitio.Any())
                        {
                            var query = (from item in NapoleonDA.tbl_DET_ServicioSitio
                                         join config in NapoleonDA.tbl_ConfigSitio on item.id_Sitio equals config.id_Sitio
                                         select new
                                         {
                                             id_sitio = item.id_Sitio,
                                             vchClaveSitio = config.vchClaveSitio,
                                             vchNombreSitio = config.vchnombreSitio,
                                             datFechaSCP = item.datFechaSCP,
                                             datFechaSCU = item.datFechaSCU,
                                             datFechaSync = item.datFechaSync
                                         }).ToList();

                            if (query != null)
                            {
                                if (query.Count > 0)
                                {
                                    foreach(var item in query)
                                    {
                                        clsDashboardService mdl = new clsDashboardService();
                                        mdl.id_sitio = item.id_sitio != null ?(int)item.id_sitio: 0;
                                        mdl.vchClaveSitio = item.vchClaveSitio != null ? item.vchClaveSitio : "";
                                        mdl.vchNombreSitio = item.vchNombreSitio != null ? item.vchNombreSitio : "";
                                        if (item.datFechaSCP != null)
                                            mdl.datFechaSCP = (DateTime)item.datFechaSCP;
                                        if(item.datFechaSCU != null)
                                            mdl.datFechaSCU = (DateTime)item.datFechaSCU;
                                        if (item.datFechaSync != null)
                                            mdl.datFechaSync = (DateTime)item.datFechaSync;
                                        lstService.Add(mdl);
                                    }   
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception egSe)
            {
                Log.EscribeLog("Existe un error en getServicioSitio: " + egSe.Message);
                lstService = null;
            }
            return lstService;
        }
        #endregion dashboard

        #region addRegistro
        public bool validarSitio(string vchClaveSitio)
        {
            bool valido = false;
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    if (NapoleonDA.tbl_ConfigSitio.Any(x => x.vchClaveSitio == vchClaveSitio))
                    {
                        valido = false;
                    }
                    else
                    {
                        valido = true;
                    }
                }
            }
            catch (Exception evS)
            {
                Log.EscribeLog("Existe un error en validarSitio: " + evS.Message);
            }
            return valido;

        }

        public bool setSitio(tbl_ConfigSitio mdlSitio, tbl_RegistroSitio mdlRegistro, ref string mensaje)
        {
            bool valido = false;
            try
            {
                NapoleonDA = new NAPOLEONEntities();
                NapoleonDA.tbl_ConfigSitio.Add(mdlSitio);
                NapoleonDA.SaveChanges();
                mdlRegistro.id_Sitio = mdlSitio.id_Sitio;
                if (mdlRegistro.id_Sitio > 0)
                {
                    NapoleonDA = new NAPOLEONEntities();
                    NapoleonDA.tbl_RegistroSitio.Add(mdlRegistro);
                    valido = true;
                    mensaje = "";
                    NapoleonDA.SaveChanges();

                    setRelSitioMod(mdlSitio.id_Sitio);
                }

            }
            catch (Exception ess)
            {
                valido = false;
                mensaje = ess.Message;
                Log.EscribeLog("Error al ingresar el sitio: " + ess.Message);
            }
            return valido;
        }
        #endregion addRegistro

        #region admin
        public List<clsEstudio> getListEstudios(int intEstatusID, int id_Sitio, int intModalidadID, ref string mensaje)
        {
            List<clsEstudio> _lstEst = new List<clsEstudio>();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    var query = NapoleonDA.stp_getEstudio(intEstatusID, id_Sitio, intModalidadID).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach (stp_getEstudio_Result item in query)
                            {
                                clsEstudio mdl = new clsEstudio();
                                mdl.intEstudioID = item.intEstudioID;
                                mdl.id_Sitio = (int)item.id_Sitio;
                                mdl.vchClaveSitio = item.vchClaveSitio;
                                mdl.intModalidadID = (int)item.intModalidadID;
                                mdl.vchModalidadID = item.vchModalidadClave;
                                mdl.vchAccessionNumber = item.vchAccessionNumber;
                                mdl.vchPatientBirthDate = item.vchPatientBirthDate;
                                mdl.PatientID = item.PatientID;
                                mdl.PatientName = item.PatientName;
                                mdl.intNumeroArchivo = (int)item.intNumeroArchivo;
                                mdl.intTamanoTotal = (int)item.intTamanoTotal;
                                mdl.datFecha = (DateTime)item.datFecha;
                                mdl.intEstatusID = (int)item.intEstatusID;
                                mdl.vchEstatusID = item.vchEstatusDesc;
                                mdl.intProyectoID = (int)item.intProyectoID;

                                //Prioridad
                                mdl.intPrioridadID = item.intPrioridadID == null ? 0 : (int)item.intPrioridadID;
                                mdl.intSecuencia = item.intSecuencia == null ? 0 : (int)item.intSecuencia;
                                mdl.bitUrgente = item.bitUrgente == null ? false :((bool)item.bitAtendido ? false : (bool)item.bitUrgente);
                                mdl.vchPrioridad = mdl.bitUrgente && !mdl.bitAtendido ? "URGENTE" : "";
                                mdl.bitAtendido = item.bitAtendido == null ? false : (bool)item.bitAtendido;
                                mdl.datAtendido = item.datAtendido == null ? DateTime.MinValue : (DateTime)item.datAtendido;
                                mdl.vchusuarioSol = item.vchusuarioSol == null ? "" : item.vchusuarioSol;

                                _lstEst.Add(mdl);
                            }
                        }
                    }
                }
                mensaje = "";
            }
            catch (Exception egE)
            {
                mensaje = egE.Message;
                _lstEst = null;
                Log.EscribeLog("Existe un error en getListEstudios: " + egE.Message);
            }
            return _lstEst;
        }

        public List<clsModeloCatalogo> getCatalogo(String _TipoCat, int intProyecto, int id_Sitio)
        {
            List<clsModeloCatalogo> _lstCat = new List<clsModeloCatalogo>();
            try
            {
                switch (_TipoCat)
                {
                    case "TipoUsuario":
                        using (NapoleonDA = new NAPOLEONEntities())
                        {
                            var query = (from item in NapoleonDA.tbl_CAT_TipoUsuario
                                         select new
                                         {
                                             vchDescripcion = item.vchTipoUsuario,
                                             vchValue = item.intTipoUsuarioID
                                         }).ToList();
                            if (query != null)
                            {
                                if (query.Count() > 0)
                                {
                                    foreach (var item in query)
                                    {
                                        clsModeloCatalogo mdl = new clsModeloCatalogo();
                                        mdl.vchDescripcion = item.vchDescripcion;
                                        mdl.vchValue = item.vchValue.ToString();
                                        _lstCat.Add(mdl);
                                    }
                                }
                            }
                            NapoleonDA.Dispose();
                        }
                        break;
                    case "Modalidades":
                        using (NapoleonDA = new NAPOLEONEntities())
                        {
                            var query = from item in NapoleonDA.tbl_CAT_Modalidad
                                        select new
                                        {
                                            vchDescripcion = item.vchModalidadDesc,
                                            vchValue = item.intModalidadID
                                        };
                            if (query != null)
                            {
                                if (query.Count() > 0)
                                {
                                    foreach (var item in query)
                                    {
                                        clsModeloCatalogo mdl = new clsModeloCatalogo();
                                        mdl.vchDescripcion = item.vchDescripcion;
                                        mdl.vchValue = item.vchValue.ToString();
                                        _lstCat.Add(mdl);
                                    }
                                }
                            }
                            NapoleonDA.Dispose();
                        }
                        break;
                    case "Sucursales":
                        using (NapoleonDA = new NAPOLEONEntities())
                        {
                            if (intProyecto == 0 && id_Sitio == 0)
                            {
                                if (NapoleonDA.tbl_ConfigSitio.Any())
                                {
                                    var query = from item in NapoleonDA.tbl_ConfigSitio
                                                select new
                                                {
                                                    vchDescripcion = item.vchnombreSitio,
                                                    vchValue = item.id_Sitio
                                                };
                                    if (query != null)
                                    {
                                        if (query.Count() > 0)
                                        {
                                            foreach (var item in query)
                                            {
                                                clsModeloCatalogo mdl = new clsModeloCatalogo();
                                                mdl.vchDescripcion = item.vchDescripcion;
                                                mdl.vchValue = item.vchValue.ToString();
                                                _lstCat.Add(mdl);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (intProyecto > 0)
                                {
                                    if (NapoleonDA.tbl_REL_ProyectoSitio.Any(x => x.intProyectoID == intProyecto))
                                    {
                                        var query = (from item in NapoleonDA.tbl_REL_ProyectoSitio 
                                                     join site in  NapoleonDA.tbl_ConfigSitio on item.id_Sitio equals site.id_Sitio
                                                     where item.intProyectoID == intProyecto
                                                     select new
                                                     {
                                                         vchDescripcion = site.vchnombreSitio,
                                                         vchValue = item.id_Sitio
                                                     }).ToList();
                                        if (query != null)
                                        {
                                            if (query.Count() > 0)
                                            {
                                                foreach (var item in query)
                                                {
                                                    clsModeloCatalogo mdl = new clsModeloCatalogo();
                                                    mdl.vchDescripcion = item.vchDescripcion;
                                                    mdl.vchValue = item.vchValue.ToString();
                                                    _lstCat.Add(mdl);
                                                }
                                            }
                                        }
                                    }
                                }
                                if (id_Sitio > 0)
                                {
                                    if (NapoleonDA.tbl_ConfigSitio.Any(x => x.id_Sitio == id_Sitio))
                                    {
                                        var query = (from item in NapoleonDA.tbl_ConfigSitio
                                                     where item.id_Sitio == id_Sitio
                                                     select new
                                                     {
                                                         vchDescripcion = item.vchnombreSitio,
                                                         vchValue = item.id_Sitio
                                                     }).ToList();
                                        if (query != null)
                                        {
                                            if (query.Count() > 0)
                                            {
                                                foreach (var item in query)
                                                {
                                                    clsModeloCatalogo mdl = new clsModeloCatalogo();
                                                    mdl.vchDescripcion = item.vchDescripcion;
                                                    mdl.vchValue = item.vchValue.ToString();
                                                    _lstCat.Add(mdl);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            NapoleonDA.Dispose();
                        }
                        break;
                    case "Estatus":
                        using (NapoleonDA = new NAPOLEONEntities())
                        {
                            var query = from item in NapoleonDA.tbl_CAT_Estatus
                                        select new
                                        {
                                            vchValue = item.intEstatusID,
                                            vchDetalle = item.vchEstatusDesc
                                        };
                            if (query != null)
                            {
                                if (query.Count() > 0)
                                {
                                    foreach (var item in query)
                                    {
                                        clsModeloCatalogo mdl = new clsModeloCatalogo();
                                        mdl.vchDescripcion = item.vchDetalle;
                                        mdl.vchValue = item.vchValue.ToString();
                                        _lstCat.Add(mdl);
                                    }
                                }
                            }
                            NapoleonDA.Dispose();
                        }
                        break;
                    case "Prioridad":
                        clsModeloCatalogo mdl2 = new clsModeloCatalogo();
                        mdl2.vchDescripcion = "NORMAL";
                        mdl2.vchValue = "1";
                        _lstCat.Add(mdl2);
                        mdl2 = new clsModeloCatalogo();
                        mdl2.vchDescripcion = "URGENTE";
                        mdl2.vchValue = "2";
                        _lstCat.Add(mdl2);

                        break;
                }
            }
            catch (Exception egc)
            {
                Log.EscribeLog("Existe un error en getCatalogo: " + egc.Message);
                _lstCat = null;
            }
            return _lstCat;
        }
        #endregion admin

        #region prioridad
        public clsMensaje updatePrioridadEstudio(tbl_MST_PrioridadEstudio _mdlPrioridad)
        {
            clsMensaje _msgReturn = new clsMensaje();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    //Verificar si existe ya una peticion.
                    List<tbl_MST_PrioridadEstudio> _lstVer = new List<tbl_MST_PrioridadEstudio>();
                    var query = from item in NapoleonDA.tbl_MST_PrioridadEstudio
                                select item;
                    try
                    {
                        if (query.Count() > 0)
                            _lstVer.AddRange(query);
                    }
                    catch
                    {
                        _lstVer = null;
                    }
                    if (_lstVer == null)
                    {
                        if ((bool)_mdlPrioridad.bitUrgente)
                        {
                            _mdlPrioridad.intSecuencia = getSiguientePrioridad() + 1;
                            using (NapoleonDA = new NAPOLEONEntities())
                            {
                                NapoleonDA.tbl_MST_PrioridadEstudio.Add(_mdlPrioridad);
                                NapoleonDA.SaveChanges();
                            }
                            _msgReturn.vchMensaje = "OK";
                        }
                    }
                    else
                    {
                        if (!_lstVer.Exists(x => x.intEstudioID == _mdlPrioridad.intEstudioID))
                        {
                            if ((bool)_mdlPrioridad.bitUrgente)
                            {
                                _mdlPrioridad.intSecuencia = getSiguientePrioridad() + 1;
                                using (NapoleonDA = new NAPOLEONEntities())
                                {
                                    NapoleonDA.tbl_MST_PrioridadEstudio.Add(_mdlPrioridad);
                                    NapoleonDA.SaveChanges();
                                }
                                _msgReturn.vchMensaje = "OK";
                            }
                        }
                        else
                        {
                            using (NapoleonDA = new NAPOLEONEntities())
                            {
                                tbl_MST_PrioridadEstudio mdl = NapoleonDA.tbl_MST_PrioridadEstudio.First(u => u.intEstudioID == _mdlPrioridad.intEstudioID);
                                mdl.bitUrgente = _mdlPrioridad.bitUrgente;
                                mdl.bitAtendido = _mdlPrioridad.bitAtendido;
                                mdl.intSecuencia = getSiguientePrioridad() + 1;
                                mdl.datAtendido = _mdlPrioridad.datAtendido;
                                mdl.datFecha = _mdlPrioridad.datFecha;
                                mdl.vchusuarioSol = _mdlPrioridad.vchusuarioSol;
                                NapoleonDA.SaveChanges();
                            }
                            _msgReturn.vchMensaje = "OK";
                            if (!(bool)_mdlPrioridad.bitUrgente)
                            {
                                setPrioridad((int)_mdlPrioridad.intEstudioID, 0, (int)_mdlPrioridad.intSecuencia);
                            }
                            //NapoleonDA.Dispose();
                        }
                    }
                }
            }
            catch (Exception eUP)
            {
                _msgReturn.vchError = eUP.Message;
            }
            return _msgReturn;
        }

        public int getSiguientePrioridad()
        {
            int next = 0;
            try
            {
                List<tbl_MST_PrioridadEstudio> _lstPrio = new List<tbl_MST_PrioridadEstudio>();
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    _lstPrio = (from item in NapoleonDA.tbl_MST_PrioridadEstudio
                                select item).ToList();
                    if (_lstPrio != null)
                    {
                        if (_lstPrio.Count > 0)
                        {
                            List<tbl_MST_PrioridadEstudio> _lstMax = new List<tbl_MST_PrioridadEstudio>();
                            _lstMax = _lstPrio.Where(x => x.bitAtendido == false).ToList();
                            if (_lstMax != null)
                                if (_lstMax.Count > 0)
                                    next = _lstMax.Max(x => (int)x.intSecuencia);
                                else
                                    next = 0;
                            else
                                next = 0;
                        }
                        else
                            next = 0;
                    }
                    else
                        next = 0;
                }
            }
            catch (Exception enP)
            {
                throw enP;
            }
            return next;
        }

        public string setPrioridad(int idEstudio, int intDireccion, int intSecuenciaActual)
        {
            String result = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Helper.ConnectionString()))
                {
                    try
                    {
                        cn.Open();

                        SqlCommand cmd = new SqlCommand("stp_setPrioridades", cn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idEstudio", idEstudio);
                        cmd.Parameters.AddWithValue("@intDireccion", intDireccion);
                        cmd.Parameters.AddWithValue("@intSecuenciaActual", intSecuenciaActual);
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            result = dr.GetString(0);
                        }
                    }
                    catch (Exception esql)
                    {
                        result = esql.Message;
                    }
                    finally
                    {
                        cn.Close();
                    }
                }
            }
            catch (Exception eSP)
            {
                result = eSP.Message;
            }
            return result;
        }
        #endregion prioridad

        #region Contacto
        public tbl_RegistroSitio getRegistroContacto(int id_Sitio)
        {
            tbl_RegistroSitio reg = new tbl_RegistroSitio();
            try
            {
                using(NapoleonDA = new NAPOLEONEntities())
                {
                    var query = (from item in NapoleonDA.tbl_RegistroSitio
                                 where item.id_Sitio == id_Sitio
                                 select item).ToList();
                    if(query!= null)
                    {
                        if (query.Count > 0)
                        {
                            reg = query.First(x => x.id_Sitio == id_Sitio);
                        }
                    }
                }
            }
            catch(Exception egRC)
            {
                reg = null;
                Log.EscribeLog("Existe un error en getRegistroContacto: " + egRC);
            }
            return reg;
        }
        #endregion Contacto

        #region prioridadMod
        public List<clsPrioridadSucursal> getPrioridadSucursal(int id_Sitio, int intProyectoID)
        {
            List<clsPrioridadSucursal> _lstResult = new List<clsPrioridadSucursal>();
            try
            {
                //Log.EscribeLog("ID_SITIO: " + id_Sitio + "  INTPROYECTO: " + intProyectoID);
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    var query = (from item in NapoleonDA.stp_getPrioridadSucursalModalidad(id_Sitio, intProyectoID)
                                 select item).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach(var row in query)
                            {
                                clsPrioridadSucursal mdl = new clsPrioridadSucursal();
                                mdl.bitActivo = (bool)row.bitActivo;
                                mdl.id_Sitio = (int)row.id_Sitio;
                                mdl.intModalidadID = (int)row.intModalidadID;
                                mdl.intREL_SitioModID = (int)row.intREL_SitioModID;
                                mdl.intSecuencia = row.intSecuencia == 0 ? 99999 : (int)row.intSecuencia;
                                mdl.vchModalidadID = row.vchModalidadClave;
                                mdl.vchSitio = row.vchnombreSitio;

                                _lstResult.Add(mdl);
                            }
                            
                        }
                    }
                }
            }
            catch (Exception egP)
            {
                Log.EscribeLog("Existe un error en getPrioridadSucursal: " + egP.Message);
            }
            return _lstResult;
        }

        public bool setRelSitioMod(int id_Sitio)
        {
            bool valida = false;
            try
            {
                List<clsModeloCatalogo> lstCat = new List<clsModeloCatalogo>();
                lstCat = getCatalogo("Modalidades",0,0).ToList();
                if(lstCat!= null)
                {
                    if(lstCat.Count > 0)
                    {
                        foreach(clsModeloCatalogo item in lstCat)
                        {
                            tbl_REL_SitioModalidad mdl = new tbl_REL_SitioModalidad();
                            mdl.intModalidadID = Convert.ToInt32(item.vchValue);
                            mdl.intSecuencia = 0;
                            mdl.bitActivo = false;
                            mdl.datFecha = DateTime.Now;
                            mdl.id_Sitio = id_Sitio;
                            using (NapoleonDA = new NAPOLEONEntities())
                            {
                                NapoleonDA.tbl_REL_SitioModalidad.Add(mdl);
                                NapoleonDA.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch(Exception esRSM)
            {
                Log.EscribeLog("Existe un error en setRelSitioMod: " + esRSM.Message);
            }
            return valida;
        }

        public string setPrioridadesSucModAcomodar(int mosOID, int intDireccion, int intSecuenciaActual)
        {
            String result = "";
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    var query = NapoleonDA.stp_setPrioridadesSucModAcomodar(mosOID, intDireccion, intSecuenciaActual);
                    if(query != null)
                    {
                        if(query > 1)
                        {
                            result = "OK";
                        }else
                        {
                            result = "";
                        }
                    }
                }
                //using (SqlConnection cn = new SqlConnection(Helper.ConnectionString()))
                //{
                //    try
                //    {
                //        cn.Open();

                //        SqlCommand cmd = new SqlCommand("stp_setPrioridadesSucModAcomodar", cn);
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.AddWithValue("@intREL_SitioModID", mosOID);
                //        cmd.Parameters.AddWithValue("@intDireccion", intDireccion);
                //        cmd.Parameters.AddWithValue("@intSecuenciaActual", intSecuenciaActual);
                //        SqlDataReader dr = cmd.ExecuteReader();

                //        if (dr.Read())
                //        {
                //            result = dr.GetString(0);
                //        }
                //    }
                //    catch (Exception esql)
                //    {
                //        result = esql.Message;
                //    }
                //    finally
                //    {
                //        cn.Close();
                //    }
                //}
            }
            catch (Exception eSP)
            {
                result = eSP.Message;
            }
            return result;
        }

        public clsMensaje updateEstatusPrioridadModalidad(clsPrioridadSucursal _mdlPriModalidad)
        {
            clsMensaje _menReturn = new clsMensaje();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    var query = NapoleonDA.stp_updatePrioridadesSucMod(_mdlPriModalidad.intREL_SitioModID, (_mdlPriModalidad.bitActivo == true ? false : true));
                    if(query != null)
                    {
                        if (query > 0)
                        {
                            _menReturn.vchMensaje = "OK";
                        }
                        else
                        {
                            _menReturn.vchError = "Existe un error.";
                        }
                    }
                }
                //using (SqlConnection cn = new SqlConnection(Helper.ConnectionString()))
                //{
                //    try
                //    {
                //        cn.Open();

                //        SqlCommand cmd = new SqlCommand("stp_updatePrioridadesSucMod", cn);
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.AddWithValue("@intREL_SitioModID", _mdlPriModalidad.intREL_SitioModID);
                //        cmd.Parameters.AddWithValue("@Activar", _mdlPriModalidad.bitActivo == true ? false : true);
                //        SqlDataReader dr = cmd.ExecuteReader();

                //        if (dr.Read())
                //        {
                //            _menReturn.vchMensaje = dr.GetString(0);
                //        }
                //    }
                //    catch (Exception esql)
                //    {
                //        _menReturn.vchError = esql.Message;
                //    }
                //    finally
                //    {
                //        cn.Close();
                //    }
                //}
            }
            catch (Exception eup)
            {
                _menReturn.vchError = eup.Message;
            }
            return _menReturn;
        }

        public string setPrioridadesSucMod(int mosID, bool activar)
        {
            String result = "";
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    var query = NapoleonDA.stp_setPrioridadesSucMod(mosID, activar);
                    if (query != null)
                    {
                        if (query > 0)
                        {
                            result = "OK";
                        }
                        else
                        {
                            result = "NOK";
                        }
                    }
                }
                //using (SqlConnection cn = new SqlConnection(Helper.ConnectionString()))
                //{
                //    try
                //    {
                //        cn.Open();

                //        SqlCommand cmd = new SqlCommand("stp_setPrioridadesSucMod", cn);
                //        cmd.CommandType = CommandType.StoredProcedure;

                //        cmd.Parameters.AddWithValue("@intREL_SitioModID", mosID);
                //        cmd.Parameters.AddWithValue("@Activar", activar);
                //        SqlDataReader dr = cmd.ExecuteReader();

                //        if (dr.Read())
                //        {
                //            result = dr.GetString(0);
                //        }
                //    }
                //    catch (Exception esql)
                //    {
                //        result = esql.Message;
                //    }
                //    finally
                //    {
                //        cn.Close();
                //    }
                //}
            }
            catch (Exception eSP)
            {
                result = eSP.Message;
            }
            return result;
        }

        #endregion prioridadMod

        #region dash
        public List<clsEntidadTabla> getDatosTabla(DateTime FechaIncio, DateTime FechaFin, int sucOID)
        {
            List<clsEntidadTabla> _lst = new List<clsEntidadTabla>();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
                {
                    List<clsEstudio> _listTabla = new List<clsEstudio>();
                    string message = "";
                    _listTabla = getListEstudios(0,sucOID, 0, ref message);
                    if(_listTabla.Any(x => x.datFecha >= FechaIncio && x.datFecha <= FechaFin))
                    {
                        _listTabla = _listTabla.Where(x => x.datFecha >= FechaIncio && x.datFecha <= FechaFin).ToList();
                    }
                    else
                    {
                        _listTabla = null;
                    }
                    
                    if (_listTabla != null)
                    {
                        if (_listTabla.Count > 0)
                        {
                            List<clsEstudio> _listTablaFiltro = new List<clsEstudio>();
                            _listTablaFiltro = _listTabla;
                            foreach (clsEstudio item in _listTablaFiltro)
                            {
                                clsEntidadTabla _mdl = new clsEntidadTabla();
                                _mdl.Numero_Estudio = item.vchAccessionNumber;
                                _mdl.Modalidad = item.vchModalidadID;
                                _mdl.Numero_Archivos = item.intNumeroArchivo;
                                _mdl.Tamaño_Archivos = item.intTamanoTotal;
                                _mdl.Estatus = item.vchEstatusID;
                                _mdl.Sucursal = item.vchClaveSitio;
                                _lst.Add(_mdl);
                            }
                        }
                    }
                }
            }
            catch (Exception eGT)
            {
                Log.EscribeLog("Existe un error en  getDatosTabla: " + eGT.Message);
            }
            return _lst;
        }


        public List<clsGrafica> getDatosGraficas(String _tipo, DateTime _FechaIncio, DateTime _FechaFin, int sucOID)
        {
            List<clsGrafica> _lstReturn = new List<clsGrafica>();
            try
            {
                DateTime FechaIncio;
                DateTime FechaFin;
                FechaIncio = _FechaIncio == null ? DateTime.Now : _FechaIncio;
                FechaFin = _FechaFin == null ? DateTime.Now : _FechaFin;
                List<clsEstudio> _lstNap = new List<clsEstudio>();
                int i = 0;
                string message = "";
                switch (_tipo)
                {

                    case "Completos":
                        
                        _lstNap = getListEstudios(0, sucOID, 0, ref message);
                        if (_lstNap.Any(x => x.datFecha >= FechaIncio && x.datFecha <= FechaFin))
                        {
                            _lstNap = _lstNap.Where(x => x.datFecha >= FechaIncio && x.datFecha <= FechaFin).ToList();
                        }
                        else
                        {
                            _lstNap = null;
                        }
                        if (_lstNap != null)
                        {
                            if (_lstNap.Count > 0)
                            {
                                List<clsEstudio> _lstNapFiltro = new List<clsEstudio>();
                                _lstNapFiltro = _lstNap.Where(item => item.intEstatusID == 3).ToList();
                                i = 0;
                                //Obtener las Etiquetas
                                foreach (String _label in _lstNapFiltro.Select(x => x.vchClaveSitio).Distinct().ToList())
                                {
                                    clsGrafica _mdl = new clsGrafica();
                                    _mdl._Nombre = _label;
                                    //Agregar los colores.
                                    _mdl._Color = arrColor[i];
                                    _mdl._hoverColor = arrHover[i];
                                    i++;
                                    _lstReturn.Add(_mdl);
                                }

                                //Obtener los valores
                                foreach (clsGrafica mdl in _lstReturn)
                                {
                                    _lstReturn.Find(x => x._Nombre == mdl._Nombre)._Valor = _lstNapFiltro.Count(x => x.vchClaveSitio == mdl._Nombre);
                                }

                            }
                        }
                        break;
                    case "PSC":
                        _lstNap = getListEstudios(0, sucOID, 0, ref message);
                        if (_lstNap.Any(x => x.datFecha >= FechaIncio && x.datFecha <= FechaFin))
                        {
                            _lstNap = _lstNap.Where(x => x.datFecha >= FechaIncio && x.datFecha <= FechaFin).ToList();
                        }
                        else
                        {
                            _lstNap = null;
                        }
                        if (_lstNap != null)
                        {
                            if (_lstNap.Count > 0)
                            {
                                List<clsEstudio> _lstNapFiltro = new List<clsEstudio>();
                                _lstNapFiltro = _lstNap.Where(item => item.intEstatusID == 2).ToList();
                                i = 0;
                                //Obtener las Etiquetas
                                foreach (String _label in _lstNapFiltro.Select(x => x.vchClaveSitio).Distinct().ToList())
                                {
                                    clsGrafica _mdl = new clsGrafica();
                                    _mdl._Nombre = _label;
                                    //Agregar los colores.
                                    _mdl._Color = arrColor[i];
                                    _mdl._hoverColor = arrHover[i];
                                    i++;
                                    _lstReturn.Add(_mdl);
                                }

                                //Obtener los valores
                                foreach (clsGrafica mdl in _lstReturn)
                                {
                                    _lstReturn.Find(x => x._Nombre == mdl._Nombre)._Valor = _lstNapFiltro.Count(x => x.vchClaveSitio == mdl._Nombre);
                                }

                            }
                        }
                        break;
                    case "PCSY":
                        _lstNap = getListEstudios(0, sucOID, 0, ref message);
                        if (_lstNap.Any(x => x.datFecha >= FechaIncio && x.datFecha <= FechaFin))
                        {
                            _lstNap = _lstNap.Where(x => x.datFecha >= FechaIncio && x.datFecha <= FechaFin).ToList();
                        }
                        else
                        {
                            _lstNap = null;
                        }
                        if (_lstNap != null)
                        {
                            if (_lstNap.Count > 0)
                            {
                                List<clsEstudio> _lstNapFiltro = new List<clsEstudio>();
                                _lstNapFiltro = _lstNap.Where(item => item.intEstatusID == 2).ToList();
                                i = 0;
                                //Obtener las Etiquetas
                                foreach (String _label in _lstNapFiltro.Select(x => x.vchClaveSitio).Distinct().ToList())
                                {
                                    clsGrafica _mdl = new clsGrafica();
                                    _mdl._Nombre = _label;
                                    //Agregar los colores.
                                    _mdl._Color = arrColor[i];
                                    _mdl._hoverColor = arrHover[i];
                                    i++;
                                    _lstReturn.Add(_mdl);
                                }

                                //Obtener los valores
                                foreach (clsGrafica mdl in _lstReturn)
                                {
                                    _lstReturn.Find(x => x._Nombre == mdl._Nombre)._Valor = _lstNapFiltro.Count(x => x.vchClaveSitio == mdl._Nombre);
                                }

                            }
                        }
                        break;
                    case "Totales":
                        _lstNap = getListEstudios(0, sucOID, 0, ref message);
                        if (_lstNap.Any(x => x.datFecha >= FechaIncio && x.datFecha <= FechaFin))
                        {
                            _lstNap = _lstNap.Where(x => x.datFecha >= FechaIncio && x.datFecha <= FechaFin).ToList();
                        }
                        else
                        {
                            _lstNap = null;
                        }
                        if (_lstNap != null)
                        {
                            if (_lstNap.Count > 0)
                            {
                                List<clsEstudio> _lstNapFiltro = new List<clsEstudio>();
                                _lstNapFiltro = _lstNap.ToList();
                                i = arrColor.Length - 1;
                                //Obtener las Etiquetas
                                foreach (String _label in _lstNapFiltro.Select(x => x.vchClaveSitio).Distinct().ToList())
                                {
                                    clsGrafica _mdl = new clsGrafica();
                                    _mdl._Nombre = _label;
                                    //Agregar los colores.
                                    _mdl._Color = arrColor[i];
                                    _mdl._hoverColor = arrHover[i];
                                    i--;
                                    _lstReturn.Add(_mdl);
                                }

                                //Obtener los valores
                                foreach (clsGrafica mdl in _lstReturn)
                                {
                                    _lstReturn.Find(x => x._Nombre == mdl._Nombre)._Valor = _lstNapFiltro.Count(x => x.vchClaveSitio == mdl._Nombre);
                                }
                            }
                        }
                        break;
                    case "Tiempos":
                        break;
                }
            }
            catch (Exception egDG)
            {
                throw egDG;
            }
            return _lstReturn;
        }

        public clsTop getDatosTop(DateTime fini, DateTime ffin, int sucOID)
        {
            clsTop mdlResult = new clsTop();
            try
            {
                string message = "";
                List<clsEstudio> _list = getListEstudios(0, sucOID,0, ref message);
                if (_list.Any(x => x.datFecha >= fini && x.datFecha <= ffin))
                {
                    _list = _list.Where(x => x.datFecha >= fini && x.datFecha <= ffin).ToList();
                }
                else
                {
                    _list = null;
                }
                if (_list != null)
                {
                    if (_list.Count > 0)
                    {
                        mdlResult.TotalEstEnviados = _list.Count().ToString("N0");
                        var querySC = _list.Where(item => item.intEstatusID == 2);
                        var queryCSY = _list.Where(item => item.intEstatusID == 2);
                        if (querySC != null)
                            if (querySC.Count() > 0)
                                mdlResult.PendientesEnvSC = querySC.Count().ToString("N0");
                        if (queryCSY != null)
                            if (queryCSY.Count() > 0)
                                mdlResult.PendientesEnvCSy = queryCSY.Count().ToString("N0");
                        int sumBytesT = 0;
                        int NumArchivosT = 0;
                        foreach (var mdl in _list)
                        {
                            sumBytesT += (int)mdl.intTamanoTotal;
                            NumArchivosT += (int)mdl.intNumeroArchivo;
                        }
                        mdlResult.TamañoTotalArc = sumBytesT.ToString("N0") + " Kb";
                        mdlResult.PromedioArchivos = (NumArchivosT / _list.Count()).ToString("N0");
                    }
                }
            }
            catch (Exception)
            {
                mdlResult = null;
            }
            return mdlResult;
        }

        public string getPromedioEnvio(DateTime fIni, DateTime fFin, int sucOID)
        {
            string result = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Helper.ConnectionString()))
                {
                    try
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("stp_getPromedioEnvio", cn);
                        cmd.Parameters.AddWithValue("@fIni", fIni);
                        cmd.Parameters.AddWithValue("@fFin", fFin);
                        cmd.Parameters.AddWithValue("@sucOID", sucOID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            result = dr.GetString(0);
                        }
                    }
                    catch (Exception sqlError)
                    {
                        result = sqlError.Message;
                    }
                }
            }
            catch (Exception ep)
            {
                result = ep.Message;
            }
            return result;
        }
        #endregion dash
    }
}

