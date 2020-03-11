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
    public class UsuariosController : Controller
    {
        private VisualRekognitionComparisonEntities1 db = new VisualRekognitionComparisonEntities1();

        // GET: Usuarios
        public ActionResult Index(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            var usuarios = db.Usuario.Include(u => u.Rol);
            return View(usuarios.ToList());
        }
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult Registrarse()
        {

            return View();
        }


        [HttpPost]
        public ActionResult ValidarLogin(Usuario usuarios)
        {
            try
            {

                int IdUsuario =
              (from USR in db.Usuario.ToList()
               where USR.Nombre == usuarios.Nombre && USR.Contrasena == usuarios.Contrasena
               select USR.Id_Usuario).First();

                int IdRol =
              (from USR in db.Usuario.ToList()
               where USR.Id_Usuario == IdUsuario
               select USR.id_rol).First();

                string Email =
       (from Correo in db.Usuario.ToList()
        where Correo.Id_Usuario == IdUsuario
        select Correo.Correo).First();

                if (IdUsuario > 0)
                {

                    return RedirectToAction("Login", "Home", new { rol = IdRol, usuario = IdUsuario, NombreUsuario = usuarios.Nombre, Correo = Email });
                }
                else
                {
                    // el usuario es invalido
                    return RedirectToAction("Login", "Usuarios");
                }


            }
            catch (Exception ex)
            {

                ViewBag.error = ex.Message;
                return RedirectToAction("Login", "Usuarios");
            }
        }

        // GET: Usuarios/Details/5
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
            Usuario usuarios = db.Usuario.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            ViewBag.id_rol = new SelectList(db.Rol, "Id_Rol", "Descripcion");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Usuario,Nombre,Correo,Contrasena,id_rol")] Usuario usuarios, int rol = 0, int usuario = 0, string NombreUsuario = "", string Correo = "")
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_rol = new SelectList(db.Rol, "Id_Rol", "Descripcion", usuarios.id_rol);
            return View(usuarios);
        }
       
        

        // GET: Usuarios/Edit/5
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
            Usuario usuarios = db.Usuario.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_rol = new SelectList(db.Rol, "Id_Rol", "Descripcion", usuarios.id_rol);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Usuario,Nombre,Correo,Contrasena,id_rol")] Usuario usuarios, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_rol = new SelectList(db.Rol, "Id_Rol", "Descripcion", usuarios.id_rol);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
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
            Usuario usuarios = db.Usuario.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;

            Usuario usuarios = db.Usuario.Find(id);
            db.Usuario.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index", "Usuarios", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
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
