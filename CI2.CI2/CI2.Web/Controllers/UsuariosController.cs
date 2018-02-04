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
                    TabUsuario Usuario = new TabUsuario();
                    Usuario.Contrasena = GenerardorPassword();
                    Usuario.Correo = usuarioViewModel.Correo;
                    Usuario.NombreUsuario = usuarioViewModel.NombreUsuario;
                    var manager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var usuarioIdentity = new ApplicationUser() { UserName = Usuario.NombreUsuario, Email = Usuario.Correo };
                    var resultadoOperacion = manager.Create(usuarioIdentity, Usuario.Contrasena);
                    if (resultadoOperacion.Succeeded)
                    {
                        TabUsuarioRol usuarioRol = new TabUsuarioRol();
                        usuarioRol.IdRol = usuarioViewModel.IdRol;
                        usuarioRol.IdUsuario = Usuario.IdUsuario;
                        usuarioRol.IdentityUser_Id = Usuario.IdUsuario;
                        db.TabUsuarioRol.Add(usuarioRol);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(usuarioViewModel);
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }

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
                            db.Entry(usuarioRolTemp).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");
                    }
                    return View(usuarioViewModel);
                }

            }
            catch (Exception ex)
            {

                throw;
            }

            return View(usuarioViewModel);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(UsuarioViewModel usuarioViewModel)
        {
            var usuarioExiste = (from us in db.TabUsuario
                                 join ur in db.TabUsuarioRol on us.IdUsuario equals ur.IdentityUser_Id
                                 where us.IdUsuario == usuarioViewModel.IdUsuario
                                 select new { ur, us }).SingleOrDefault();

            if (usuarioExiste != null)
            {
                TabUsuarioRol usuarioRol = new TabUsuarioRol();
                usuarioRol.IdUsuario = usuarioViewModel.IdUsuario;
                TabUsuario usuarioTemp = new TabUsuario();
                usuarioTemp.IdUsuario = usuarioViewModel.IdUsuario;
                db.TabUsuarioRol.Remove(usuarioRol);
                db.TabUsuario.Remove(usuarioTemp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(usuarioViewModel);
            }
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
