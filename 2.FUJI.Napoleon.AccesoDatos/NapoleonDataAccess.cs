using _1.FUJI.Napoleon.Entidades;
using _2.FUJI.Napoleon.AccesoDatos.DataAccess;
using _2.FUJI.Napoleon.AccesoDatos.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.FUJI.Napoleon.AccesoDatos
{
    public class NapoleonDataAccess
    {
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
        public List<tbl_ConfigSitio> getSitios()
        {
            List<tbl_ConfigSitio> _lstSitio = new List<tbl_ConfigSitio>();
            try
            {
                using (NapoleonDA = new NAPOLEONEntities())
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
                                     from y1 in CP.DefaultIfEmpty()
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
                                         id_Sitio = item.id_Sitio
                                     }).ToList();
                        if (query.Count > 0)
                        {
                            foreach (var item in query)
                            {
                                clsUsuario user = new clsUsuario();
                                user.intUsuarioID = item.intUsuarioID;
                                user.intTipoUsuarioID = (int)item.intTipoUsuarioID;
                                user.intProyectoID = item.intProyectoID == null ? 0 : (int)item.intProyectoID;
                                user.vchProyectoID = item.vchProyectoID;
                                user.vchNombre = item.vchNombre;
                                user.vchApellido = item.vchApellido;
                                user.vchUsuario = item.vchUsuario;
                                user.vchPassword = item.vchPassword;
                                user.bitActivo = (bool)item.bitActivo;
                                user.datFecha = (DateTime)item.datFecha;
                                user.vchUserAdmin = item.vchUserAdmin;
                                user.id_Sitio = item.id_Sitio == null ? 0 : (int)item.id_Sitio;
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
                                             datFechaSCU = item.datFechaSCU
                                         }).ToList();

                            if (query != null)
                            {
                                if (query.Count > 0)
                                {
                                    foreach(var item in query)
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
        public List<clsEstudio> getListEstudios(int intEstatusID, int id_Sitio, int intModalidadID,ref string mensaje)
        {
            List<clsEstudio> _lstEst = new List<clsEstudio>();
            try
            {
                using(NapoleonDA = new NAPOLEONEntities())
                {
                    var query = NapoleonDA.stp_getEstudio(intEstatusID, id_Sitio, intModalidadID).ToList();
                    if(query != null)
                    {
                        if (query.Count > 0)
                        {
                            foreach(stp_getEstudio_Result item in query)
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
                                mdl.vchStudyInstanceUID = item.vchStudyInstanceUID;
                                mdl.intNumeroArchivo = (int)item.intNumeroArchivo;
                                mdl.intTamanoTotal = (int)item.intTamanoTotal;
                                mdl.datFecha = (DateTime)item.datFecha;
                                mdl.intEstatusID = (int)item.intEstatusID;
                                mdl.vchEstatusID = item.vchEstatusDesc;
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

        public List<clsModeloCatalogo> getCatalogo(String _TipoCat)
        {
            List<clsModeloCatalogo> _lstCat = new List<clsModeloCatalogo>();
            try
            {
                switch (_TipoCat)
                {
                    case "TipoUsuario":
                        using (NapoleonDA = new NAPOLEONEntities())
                        {
                            var query = from item in NapoleonDA.tbl_CAT_TipoUsuario
                                        select new clsModeloCatalogo
                                        {
                                            vchDescripcion = item.vchTipoUsuario,
                                            vchValue = item.intTipoUsuarioID.ToString()
                                        };
                            _lstCat.AddRange(query);
                            NapoleonDA.Dispose();
                        }
                        break;
                    case "Modalidades":
                        using (NapoleonDA = new NAPOLEONEntities())
                        {
                            var query = from item in NapoleonDA.tbl_CAT_Modalidad
                                        select new clsModeloCatalogo
                                        {
                                            vchDescripcion = item.vchModalidadClave,
                                            vchValue = item.intModalidadID.ToString()
                                        };
                            _lstCat.AddRange(query);
                            NapoleonDA.Dispose();
                        }
                        break;
                    case "Sucursales":
                        using (NapoleonDA = new NAPOLEONEntities())
                        {
                            var query = from item in NapoleonDA.tbl_ConfigSitio
                                        select new clsModeloCatalogo
                                        {
                                            vchDescripcion = item.vchnombreSitio,
                                            vchValue = item.id_Sitio.ToString()
                                        };
                            _lstCat.AddRange(query);
                            NapoleonDA.Dispose();
                        }
                        break;
                    case "Estatus":
                        using (NapoleonDA = new NAPOLEONEntities())
                        {
                            var query = from item in NapoleonDA.tbl_CAT_Estatus
                                        select new clsModeloCatalogo
                                        {
                                            vchDescripcion = item.vchEstatusDesc,
                                            vchValue = item.intEstatusID.ToString()
                                        };
                            _lstCat.AddRange(query);
                            NapoleonDA.Dispose();
                        }
                        break;
                        //case "Prioridad":
                        //    using (NapoleonDA = new NAPOLEONEntities())
                        //    {
                        //        var query = from item in dc.catPrioridad
                        //                    select new clsModeloCatalogo
                        //                    {
                        //                        vchDescripcion = item.prioDescripcion,
                        //                        vchValue = item.prioID.ToString()
                        //                    };
                        //        _lstCat.AddRange(query);
                        //        NapoleonDA.Dispose();
                        //    }
                        //    break;
                }
            }
            catch (Exception)
            {
                _lstCat = null;
            }
            return _lstCat;
        }
        #endregion admin
    }
}

