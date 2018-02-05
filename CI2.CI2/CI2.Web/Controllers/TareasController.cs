using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using CI2.Persistencia;
using CI2.Web.Models;

namespace CI2.Web.Controllers
{
    /// <summary>
    /// Controlador del sevicio web expuesto en CI2
    /// </summary>
    [Authorize]
    [RoutePrefix("api/tareas")]
    public class TareasController : ApiController
    {
        private CI2Entities db = new CI2Entities();
        /// <summary>
        /// Permite consultar las tareas de un usuario del sistema
        /// </summary>
        /// <param name="parametros">Recibe argumentos de tipo ConsultaParametrosViewMode:l string Usuario, int Estado, string ordenar</param>
        /// <returns>Devuelve un objeto de tipo ConsultaResultadoViewModel: Int64 IdTarea, string Descripcion, DateTime FechaVencimiento, bool Estado, DateTime FechaCreacion, DateTime FechaActualizacion,string IdUsuario </returns>
        [HttpGet]
        [Route("consultar")]
        public IEnumerable<ConsultaResultadoViewModel> Consultar([FromUri]ConsultaParametrosViewModel parametros)
        {
            parametros = parametros ?? new ConsultaParametrosViewModel();

            IQueryable<TabTareaUsuario> consulta = null;
            try
            {
                if (string.IsNullOrWhiteSpace(parametros.Usuario))
                {
                    consulta = db.TabTareaUsuario;
                }
                else
                {
                    consulta = db.TabTareaUsuario.Where(item => item.IdUsuario == parametros.Usuario);
                }
                switch (parametros.Estado)
                {
                    case Estados.finalizada:
                        consulta.Where(item => item.Estado == true);
                        break;
                    case Estados.pendiente:
                        consulta.Where(item => item.Estado == false);
                        break;
                    case Estados.sinespecificar:
                        break;
                    default:
                        break;
                }
                switch (parametros.Ordenar)
                {
                    case Ordenar.ascendente:
                        consulta.OrderBy(item => item.FechaVencimieno);
                        break;
                    case Ordenar.descendente:
                        consulta.OrderByDescending(item => item.FechaVencimieno);
                        break;
                    case Ordenar.sinespecificar:
                        consulta.OrderBy(item => item.FechaVencimieno);
                        break;
                    default:
                        break;
                }

                return consulta.Select(item => new ConsultaResultadoViewModel() { IdTarea = item.IdTarea, Descripcion = item.Descripcion, FechaVencimiento = item.FechaVencimieno, Estado = item.Estado, FechaCreacion = item.FechaCreacion, FechaActualizacion = item.FechaActualizacion, IdUsuario = item.IdUsuario }).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("crear")]
        public CrearResultadoViewModel Crear([FromBody]CrearParametrosViewModel parametros)
        {
            CrearResultadoViewModel CrearResultadoViewModel = new CrearResultadoViewModel();
            try
            {
                string IdUsuario = "";
                if (ModelState.IsValid)
                {
                    var nombreUsuarioActual = User.Identity.Name;
                    if (nombreUsuarioActual != null)
                    {
                        IdUsuario = db.TabUsuario.Where(item => item.NombreUsuario == nombreUsuarioActual).Select(item => item.IdUsuario).SingleOrDefault();
                        if (IdUsuario != "")
                        {
                            TabTareaUsuario tareaUsuario = new TabTareaUsuario();
                            tareaUsuario.Descripcion = parametros.Descripcion;

                            switch (parametros.Estado)
                            {
                                case Estados.finalizada:
                                    tareaUsuario.Estado = true;
                                    break;
                                case Estados.pendiente:
                                    tareaUsuario.Estado = false;
                                    break;
                                case Estados.sinespecificar:
                                    tareaUsuario.Estado = false;
                                    break;
                                default:
                                    break;
                            }
                            tareaUsuario.FechaCreacion = DateTime.Now;
                            tareaUsuario.FechaActualizacion = DateTime.Now;
                            tareaUsuario.FechaVencimieno = Convert.ToDateTime(parametros.FechaVencimiento);
                            tareaUsuario.IdUsuario = IdUsuario;
                            db.TabTareaUsuario.Add(tareaUsuario);
                            db.SaveChanges();
                            CrearResultadoViewModel.IdTarea = tareaUsuario.IdTarea;
                            CrearResultadoViewModel.Descripcion = tareaUsuario.Descripcion;
                            CrearResultadoViewModel.Estado = tareaUsuario.Estado;
                            CrearResultadoViewModel.FechaActualizacion = tareaUsuario.FechaActualizacion;
                            CrearResultadoViewModel.FechaCreacion = tareaUsuario.FechaCreacion;
                            CrearResultadoViewModel.FechaVencimiento = tareaUsuario.FechaVencimieno;
                            CrearResultadoViewModel.IdUsuario = tareaUsuario.IdUsuario;
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException();
                    }

                }
                else
                {
                    throw new InvalidExpressionException();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return CrearResultadoViewModel;
        }

        [HttpPost]
        [Route("actualizar")]
        public ActualizarResultadoViewModel Actualizar([FromBody]ActualizarParametrosViewModel parametros)
        {
            ActualizarResultadoViewModel ActualizarResultadoViewModel = new ActualizarResultadoViewModel();
            try
            {
                string IdUsuario = "";
                if (ModelState.IsValid)
                {
                    var nombreUsuarioActual = User.Identity.Name;
                    if (nombreUsuarioActual != null)
                    {
                        IdUsuario = db.TabUsuario.Where(item => item.NombreUsuario == nombreUsuarioActual).Select(item => item.IdUsuario).SingleOrDefault();
                        if (IdUsuario != "" && parametros.IdTarea != 0)
                        {
                            TabTareaUsuario tareaUsuarioCreador = db.TabTareaUsuario.Where(item => item.IdUsuario == IdUsuario && item.IdTarea == parametros.IdTarea).SingleOrDefault();

                            if (tareaUsuarioCreador != null)
                            {
                                TabTareaUsuario tareaUsuario = new TabTareaUsuario();
                                if (!string.IsNullOrEmpty(parametros.Descripcion))
                                {
                                    tareaUsuarioCreador.Descripcion = parametros.Descripcion;
                                }

                                if (!string.IsNullOrWhiteSpace(parametros.FechaVencimiento))
                                {
                                    tareaUsuarioCreador.FechaVencimieno = Convert.ToDateTime(parametros.FechaVencimiento);
                                }
                                if (parametros.Estado != null)
                                {
                                    switch (parametros.Estado)
                                    {
                                        case Estados.finalizada:
                                            tareaUsuario.Estado = true;
                                            break;
                                        case Estados.pendiente:
                                            tareaUsuario.Estado = false;
                                            break;
                                        case Estados.sinespecificar:
                                            tareaUsuario.Estado = false;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                tareaUsuarioCreador.FechaActualizacion = DateTime.Now;
                                db.TabTareaUsuario.Attach(tareaUsuarioCreador);
                                db.Entry(tareaUsuarioCreador).State = EntityState.Modified;
                                db.SaveChanges();
                                ActualizarResultadoViewModel.IdTarea = tareaUsuarioCreador.IdTarea;
                                ActualizarResultadoViewModel.Descripcion = tareaUsuarioCreador.Descripcion;
                                ActualizarResultadoViewModel.Estado = tareaUsuarioCreador.Estado;
                                ActualizarResultadoViewModel.FechaActualizacion = tareaUsuarioCreador.FechaActualizacion;
                                ActualizarResultadoViewModel.FechaCreacion = tareaUsuarioCreador.FechaCreacion;
                                ActualizarResultadoViewModel.FechaVencimiento = tareaUsuarioCreador.FechaVencimieno;
                                ActualizarResultadoViewModel.IdUsuario = tareaUsuarioCreador.IdUsuario;
                            }
                            else
                            {
                                throw new UnauthorizedAccessException();
                            }
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException();
                    }

                }
                else
                {
                    throw new InvalidExpressionException();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ActualizarResultadoViewModel;
        }

        [HttpPost]
        [Route("borrar")]
        public BorrarResultadoViewModel Borrar([FromBody]BorrarParametrosViewModel parametros)
        {
            BorrarResultadoViewModel BorrarResultadoViewModel = new BorrarResultadoViewModel();
            try
            {
                string IdUsuario = "";
                if (ModelState.IsValid)
                {
                    var nombreUsuarioActual = User.Identity.Name;
                    if (nombreUsuarioActual != null)
                    {
                        IdUsuario = db.TabUsuario.Where(item => item.NombreUsuario == nombreUsuarioActual).Select(item => item.IdUsuario).SingleOrDefault();
                        if (IdUsuario != "")
                        {
                            TabTareaUsuario tareaUsuarioCreador = db.TabTareaUsuario.Where(item => item.IdTarea == parametros.IdTarea).SingleOrDefault();

                            if (tareaUsuarioCreador != null)
                            {
                                db.TabTareaUsuario.Attach(tareaUsuarioCreador);
                                db.TabTareaUsuario.Remove(tareaUsuarioCreador);
                                db.SaveChanges();
                            }
                            else
                            {
                                throw new UnauthorizedAccessException();
                            }
                        }
                    }
                    else
                    {
                        throw new UnauthorizedAccessException();
                    }

                }
                else
                {
                    throw new InvalidExpressionException();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return BorrarResultadoViewModel;
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TabTareaUsuarioExists(long id)
        {
            return db.TabTareaUsuario.Count(e => e.IdTarea == id) > 0;
        }
    }
}