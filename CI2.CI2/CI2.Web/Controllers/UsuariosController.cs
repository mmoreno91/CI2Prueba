using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CI2.Persistencia;
using Microsoft.AspNet.Identity;
using System.Web.UI;
using CI2.Web.Models;
using Microsoft.AspNet.Identity.Owin;

namespace CI2.Web.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private CI2Entities db = new CI2Entities();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.TabUsuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioViewModel usuario = new UsuarioViewModel();
            var usuarioExiste = (from us in db.TabUsuario
                                 join ur in db.TabUsuarioRol on us.IdUsuario equals ur.IdentityUser_Id
                                 where us.IdUsuario == id
                                 select new { ur, us }).SingleOrDefault();

            if (usuarioExiste == null)
            {
                return HttpNotFound();
            }
            else
            {
                usuario.NombreRol = (from rl in db.TabRol
                                     where rl.IdRol == usuarioExiste.ur.IdRol
                                     select rl.NombreRol).SingleOrDefault();
                usuario.IdUsuario = usuarioExiste.us.IdUsuario;
                usuario.NombreUsuario = usuarioExiste.us.NombreUsuario;
                usuario.Telefono = usuarioExiste.us.Telefono;
                usuario.TelefonoConfirmacion = usuarioExiste.us.TelefonoConfirmacion;
                usuario.Bloqueo = usuarioExiste.us.Bloqueo;
                usuario.Correo = usuarioExiste.us.Correo;
                usuario.CorreoConfirmacion = usuarioExiste.us.CorreoConfirmacion;
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.IdRol = new SelectList(db.TabRol.ToList(), "IdRol", "NombreRol");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.UsuarioViewModel usuarioViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TabUsuario usuario = new TabUsuario();
                    usuario.Contrasena = GenerardorPassword();
                    usuario.Correo = usuarioViewModel.Correo;
                    usuario.NombreUsuario = usuarioViewModel.NombreUsuario;
                    usuario.Telefono = usuarioViewModel.Telefono;
                    usuario.CorreoConfirmacion = usuarioViewModel.CorreoConfirmacion;
                    usuario.TelefonoConfirmacion = usuarioViewModel.TelefonoConfirmacion;
                    usuario.Bloqueo = usuarioViewModel.Bloqueo;
                    var manager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var usuarioIdentity = new ApplicationUser() { UserName = usuario.NombreUsuario, Email = usuario.Correo, PhoneNumber=usuario.Telefono, EmailConfirmed= usuario.CorreoConfirmacion, PhoneNumberConfirmed=usuario.TelefonoConfirmacion, LockoutEnabled=usuario.Bloqueo};
                    var resultadoOperacion = manager.Create(usuarioIdentity, usuario.Contrasena);
                    if (resultadoOperacion.Succeeded)
                    {
                        TabUsuarioRol usuarioRol = new TabUsuarioRol();
                        usuarioRol.IdRol = usuarioViewModel.IdRol;
                        usuarioRol.IdUsuario = usuarioIdentity.Id;
                        usuarioRol.IdentityUser_Id = usuarioIdentity.Id;
                        db.TabUsuarioRol.Add(usuarioRol);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Correo","El Correo o el nombre de usuario ya existe");
                        ViewBag.IdRol = new SelectList(db.TabRol.ToList(), "IdRol", "NombreRol");
                        return View(usuarioViewModel);
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            ViewBag.IdRol = new SelectList(db.TabRol.ToList(), "IdRol", "NombreRol", usuarioViewModel.IdRol);
            return View(usuarioViewModel);
        }

        public string GenerardorPassword()
        {
            try
            {
                Random r = new Random(DateTime.Now.Millisecond);
                int L1, L2, l1, l2;
                L1 = r.Next(65, 90);
                L2 = r.Next(65, 90);
                l1 = r.Next(97, 122);
                l2 = r.Next(97, 122);
                string PN = Convert.ToChar(L1).ToString() + Convert.ToChar(L2).ToString();
                string PA = Convert.ToChar(l1).ToString() + Convert.ToChar(l2).ToString();
                string PassAuto = (PN + PA + r.Next(99999) + "$").ToString();
                return PassAuto;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsuarioViewModel usuario = new UsuarioViewModel();
            var usuarioExiste = (from us in db.TabUsuario
                                 join ur in db.TabUsuarioRol on us.IdUsuario equals ur.IdentityUser_Id
                                 where us.IdUsuario == id
                                 select new { ur, us }).SingleOrDefault();

            if (usuarioExiste == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.IdRol = new SelectList(db.TabRol.ToList(), "IdRol", "NombreRol", usuarioExiste.ur.IdRol);
                usuario.IdUsuario = usuarioExiste.us.IdUsuario;
                usuario.NombreUsuario = usuarioExiste.us.NombreUsuario;
                usuario.Telefono = usuarioExiste.us.Telefono;
                usuario.TelefonoConfirmacion = usuarioExiste.us.TelefonoConfirmacion;
                usuario.Bloqueo = usuarioExiste.us.Bloqueo;
                usuario.Correo = usuarioExiste.us.Correo;
                usuario.CorreoConfirmacion = usuarioExiste.us.CorreoConfirmacion;
                usuario.IdRol = usuarioExiste.ur.IdRol;

            }
            return View(usuario);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioViewModel usuarioViewModel)
        {
            Boolean crearTransaccion = db.Database.CurrentTransaction == null;
            var transaccion = crearTransaccion ? db.Database.BeginTransaction() : db.Database.CurrentTransaction;
            string IdRol = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var usuarioTemp = db.TabUsuario.Find(usuarioViewModel.IdUsuario);
                    if (usuarioViewModel.IdUsuario != null && usuarioTemp != null)
                    {

                        usuarioTemp.Bloqueo = usuarioViewModel.Bloqueo;
                        usuarioTemp.Correo = usuarioViewModel.Correo;
                        usuarioTemp.CorreoConfirmacion = usuarioViewModel.CorreoConfirmacion;
                        usuarioTemp.NombreUsuario = usuarioViewModel.NombreUsuario;
                        usuarioTemp.Telefono = usuarioViewModel.Telefono;
                        usuarioTemp.TelefonoConfirmacion = usuarioViewModel.TelefonoConfirmacion;
                        db.Entry(usuarioTemp).State = EntityState.Modified;
                        db.SaveChanges();
                        var usuarioRolTemp = (from r in db.TabUsuarioRol where r.IdentityUser_Id == usuarioTemp.IdUsuario select r).Single();
                        if (usuarioTemp != null)
                        {
                            TabUsuarioRol usuarioRol = new TabUsuarioRol();
                            usuarioRolTemp.IdRol = usuarioViewModel.IdRol;
                            IdRol=usuarioRol.IdRol;
                            db.Entry(usuarioRolTemp).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        if (crearTransaccion)
                        {
                            db.SaveChanges();
                            transaccion.Commit();
                        }
                        return RedirectToAction("Index");
                    }
                    return View(usuarioViewModel);
                }

            }
            catch (Exception ex)
            {
                if (crearTransaccion && transaccion != null)
                {
                    transaccion.Rollback();
                    ViewBag.IdRol = new SelectList(db.TabRol.ToList(), "IdRol", "NombreRol", IdRol);
                }
                throw;
            }
            finally
            {
                if (crearTransaccion)
                {
                    if (transaccion != null) ((IDisposable)transaccion).Dispose();
                }
            }

            return View(usuarioViewModel);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string id)
        {
            UsuarioViewModel usuario = new UsuarioViewModel();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var usuarioExiste = (from us in db.TabUsuario
                                     join ur in db.TabUsuarioRol on us.IdUsuario equals ur.IdentityUser_Id
                                     where us.IdUsuario == id
                                     select new { ur, us }).SingleOrDefault();

                if (usuarioExiste == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    usuario.NombreRol = (from rl in db.TabRol
                                         where rl.IdRol == usuarioExiste.ur.IdRol
                                         select rl.NombreRol).SingleOrDefault();
                    usuario.IdUsuario = usuarioExiste.us.IdUsuario;
                    usuario.NombreUsuario = usuarioExiste.us.NombreUsuario;
                    usuario.Telefono = usuarioExiste.us.Telefono;
                    usuario.TelefonoConfirmacion = usuarioExiste.us.TelefonoConfirmacion;
                    usuario.Bloqueo = usuarioExiste.us.Bloqueo;
                    usuario.Correo = usuarioExiste.us.Correo;
                    usuario.CorreoConfirmacion = usuarioExiste.us.CorreoConfirmacion;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Boolean crearTransaccion = db.Database.CurrentTransaction == null;
            var transaccion = crearTransaccion ? db.Database.BeginTransaction() : db.Database.CurrentTransaction;

            try
            {
                TabUsuario usuario = db.TabUsuario.Find(id);
                TabUsuarioRol usuarioRol = db.TabUsuarioRol.Where(item => item.IdUsuario == id).SingleOrDefault();
                if (usuario != null && usuarioRol != null)
                {
                    db.TabUsuarioRol.Attach(usuarioRol);
                    db.TabUsuarioRol.Remove(usuarioRol);
                    db.TabUsuario.Attach(usuario);
                    db.TabUsuario.Remove(usuario);
                    db.SaveChanges();
                    if (crearTransaccion)
                    {
                        db.SaveChanges();
                        transaccion.Commit();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(id);
                }
            }

            catch (Exception ex)
            {
                if (crearTransaccion && transaccion != null)
                {
                    transaccion.Rollback();
                }
                throw;
            }
            finally
            {
                if (crearTransaccion)
                {
                    if (transaccion != null) ((IDisposable)transaccion).Dispose();
                }
            }
            return View(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
