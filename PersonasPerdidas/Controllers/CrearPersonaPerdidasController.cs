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
    public class CrearPersonaPerdidasController : Controller
    {
        private VisualRekognitionComparisonEntities2 db = new VisualRekognitionComparisonEntities2();

        // GET: CrearPersonaPerdidas
        public ActionResult Index(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            var crearPersonaPerdida = db.CrearPersonaPerdida.Include(c => c.Usuario);
            return View(crearPersonaPerdida.ToList());
        }

        // GET: CrearPersonaPerdidas/Details/5
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
            CrearPersonaPerdida crearPersonaPerdida = db.CrearPersonaPerdida.Find(id);
            if (crearPersonaPerdida == null)
            {
                return HttpNotFound();
            }
            return View(crearPersonaPerdida);
        }

        // GET: CrearPersonaPerdidas/Create
        public ActionResult Create(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            ViewBag.Pariente = new SelectList(db.Usuario, "Id_Usuario", "Nombre");
            return View();
        }

        // POST: CrearPersonaPerdidas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_CPP,Nombre,Fecha_desaparicion,Edad,Pariente,Estado,FechaEncontrado")] CrearPersonaPerdida crearPersonaPerdida, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (ModelState.IsValid)
            {
                db.CrearPersonaPerdida.Add(crearPersonaPerdida);
                db.SaveChanges();
                return RedirectToAction("Create", "FotosPersonas", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
            }

            ViewBag.Pariente = new SelectList(db.Usuario, "Id_Usuario", "Nombre", crearPersonaPerdida.Pariente);
            return View(crearPersonaPerdida);
        }

        // GET: CrearPersonaPerdidas/Edit/5
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
            CrearPersonaPerdida crearPersonaPerdida = db.CrearPersonaPerdida.Find(id);
            if (crearPersonaPerdida == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pariente = new SelectList(db.Usuario, "Id_Usuario", "Nombre", crearPersonaPerdida.Pariente);
            return View(crearPersonaPerdida);
        }

        // POST: CrearPersonaPerdidas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_CPP,Nombre,Fecha_desaparicion,Edad,Pariente,Estado,FechaEncontrado")] CrearPersonaPerdida crearPersonaPerdida, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            if (ModelState.IsValid)
            {
                db.Entry(crearPersonaPerdida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "CrearPersonaPerdidas", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
            }
            ViewBag.Pariente = new SelectList(db.Usuario, "Id_Usuario", "Nombre", crearPersonaPerdida.Pariente);
            return View(crearPersonaPerdida);
        }

        // GET: CrearPersonaPerdidas/Delete/5
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
            CrearPersonaPerdida crearPersonaPerdida = db.CrearPersonaPerdida.Find(id);
            if (crearPersonaPerdida == null)
            {
                return HttpNotFound();
            }
            return View(crearPersonaPerdida);
        }

        // POST: CrearPersonaPerdidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            CrearPersonaPerdida crearPersonaPerdida = db.CrearPersonaPerdida.Find(id);
            db.CrearPersonaPerdida.Remove(crearPersonaPerdida);
            db.SaveChanges();
            return RedirectToAction("Index", "CrearPersonaPerdidas", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
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
