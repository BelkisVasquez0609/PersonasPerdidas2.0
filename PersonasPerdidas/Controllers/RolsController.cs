using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonasPerdidas.Models;

namespace PersonasPerdidas.Controllers
{
    public class RolsController : Controller
    {
        private VisualRekognitionComparisonEntities1 db = new VisualRekognitionComparisonEntities1();

        // GET: Rols
        public ActionResult Index(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            return View(db.Rol.ToList());
        }

        // GET: Rols/Details/5
        public ActionResult Details(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rols = db.Rol.Find(id);
            if (rols == null)
            {
                return HttpNotFound();
            }
            return View(rols);
        }

        // GET: Rols/Create
        public ActionResult Create(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            return View();
        }

        // POST: Rols/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Rol,Descripcion")] Rol rols, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (ModelState.IsValid)
            {
                db.Rol.Add(rols);
                db.SaveChanges();
                return RedirectToAction("Index", "Rols", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
            }
               
            return View(rols);
        }

        // GET: Rols/Edit/5
        public ActionResult Edit(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rols = db.Rol.Find(id);
            if (rols == null)
            {
                return HttpNotFound();
            }
            return View(rols);
        }

        // POST: Rols/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Rol,Descripcion")] Rol rols, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (ModelState.IsValid)
            {
                db.Entry(rols).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rols);
        }

        // GET: Rols/Delete/5
        public ActionResult Delete(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rol rols = db.Rol.Find(id);
            if (rols == null)
            {
                return HttpNotFound();
            }
            return View(rols);
        }

        // POST: Rols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            Rol rols = db.Rol.Find(id);
            db.Rol.Remove(rols);
            db.SaveChanges();
            return RedirectToAction("Index", "Rols", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
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
