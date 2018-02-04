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
    [Authorize]
    [RoutePrefix("api/tareas")]
    public class TareasController : ApiController
    {
        private CI2Entities db = new CI2Entities();
        /// <summary>
        /// Ejemplo 
        /// </summary>
        /// <param name="parametros">parametro</param>
        /// <returns>parametro</returns>
        // GET: api/Tareas
        [HttpGet]
        [Route("consultar")]
        public IEnumerable<ConsultaResultadoViewModel> Consultar([FromUri]ConsultaParametrosViewModel parametros)
        {
            parametros = parametros ?? new ConsultaParametrosViewModel();

            IQueryable<TabTareaUsuario> consulta = null;
            try
            {
                //var nombreUsuarioActual = User.Identity.Name;
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
                    //    TabTareaUsuario tareaUsuario = new TabTareaUsuario()
                    //    {
                    //        Descripcion = parametros.Descripcion;
                    //};
                    var nombreUsuarioActual = User.Identity.Name;
                    if (nombreUsuarioActual != null)
                    {
                        IdUsuario = db.TabUsuario.Where(item => item.NombreUsuario == nombreUsuarioActual).Select(item => item.IdUsuario).SingleOrDefault();
                        if (IdUsuario != "")
                        {
                            TabTareaUsuario tareaUsuario = new TabTareaUsuario();
                            tareaUsuario.Descripcion = parametros.Descripcion;
                            tareaUsuario.Estado = true;//revisar
                            tareaUsuario.FechaCreacion = DateTime.Now;
                            tareaUsuario.FechaActualizacion = DateTime.Now;
                            tareaUsuario.FechaVencimieno = DateTime.Now;//revisar
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
            }
            catch (Exception ex)
            {
                throw;
            }
            return CrearResultadoViewModel;
        }


        // GET: api/Tareas/5
        [ResponseType(typeof(TabTareaUsuario))]
        public IHttpActionResult GetTabTareaUsuario(long id)
        {
            TabTareaUsuario tabTareaUsuario = db.TabTareaUsuario.Find(id);
            if (tabTareaUsuario == null)
            {
                return NotFound();
            }

            return Ok(tabTareaUsuario);
        }

        // PUT: api/Tareas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTabTareaUsuario(long id, TabTareaUsuario tabTareaUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tabTareaUsuario.IdTarea)
            {
                return BadRequest();
            }

            db.Entry(tabTareaUsuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabTareaUsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tareas
        [ResponseType(typeof(TabTareaUsuario))]
        public IHttpActionResult PostTabTareaUsuario(TabTareaUsuario tabTareaUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TabTareaUsuario.Add(tabTareaUsuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tabTareaUsuario.IdTarea }, tabTareaUsuario);
        }

        // DELETE: api/Tareas/5
        [ResponseType(typeof(TabTareaUsuario))]
        public IHttpActionResult DeleteTabTareaUsuario(long id)
        {
            TabTareaUsuario tabTareaUsuario = db.TabTareaUsuario.Find(id);
            if (tabTareaUsuario == null)
            {
                return NotFound();
            }

            db.TabTareaUsuario.Remove(tabTareaUsuario);
            db.SaveChanges();

            return Ok(tabTareaUsuario);
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