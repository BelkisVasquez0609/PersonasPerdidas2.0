using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PersonasPerdidas.Models;

namespace PersonasPerdidas.Controllers
{
    public class FotosPersonasController : Controller
    {
        private VisualRekognitionComparisonEntities1 db = new VisualRekognitionComparisonEntities1();

        // GET: FotosPersonas
        public ActionResult Index(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            var fotosPersona = db.FotosPersona.Include(f => f.CrearPersonaPerdida);
            return View(fotosPersona.ToList());
        }

        // GET: FotosPersonas/Details/5
        public ActionResult Details(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            FotosPersona fotosPersona = db.FotosPersona.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
       
            if (fotosPersona == null)
            {
                return HttpNotFound();
            }
            return View(fotosPersona);
        }

        // GET: FotosPersonas/Create
        public ActionResult Create(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;
            ViewBag.id_personaDesaparecida = new SelectList(db.CrearPersonaPerdida, "Id_CPP", "Nombre");
            return View();
        }

        // POST: FotosPersonas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Fotos,id_personaDesaparecida,Fotos")] FotosPersona fotosPersona, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            HttpPostedFileBase FileBase = Request.Files[0];//leeme el archivo en la posicion 0
                                                           //HttpFileCollectionBase collectionBase = Request.Files;
                                                           //el request le permite al servidor o al asp.net le permite leer los valores del http
                                                           //filebase nos proporciona acceso al archivo
            if (FileBase.ContentLength == 0)
            {
                ModelState.AddModelError("Fotos", "El campo necesario seleccionar una imagen.");

            }
            else
            {
                if (FileBase.FileName.EndsWith(".jpg"))
                {
                    //ahora esta clase nos permite administrar la imagen

                    System.Web.Helpers.WebImage image = new WebImage(FileBase.InputStream);

                    fotosPersona.Fotos = image.GetBytes(); //aqui se obtienen los bytes de nuestra imagen


                }
                else
                {
                    ModelState.AddModelError("Fotos", "El sistema solo acepta un formato.JPG");
                }

            }
            if (ModelState.IsValid)
            {
                db.FotosPersona.Add(fotosPersona);
                db.SaveChanges();
                return RedirectToAction("Index", "CrearPersonasPerdidas", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
            
        }

            ViewBag.id_personaDesaparecida = new SelectList(db.CrearPersonaPerdida, "Id_CPP", "Nombre", fotosPersona.id_personaDesaparecida);
            return View(fotosPersona);
        }

        // GET: FotosPersonas/Edit/5
        public ActionResult Edit(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FotosPersona fotosPersona = db.FotosPersona.Find(id);
            if (fotosPersona == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_personaDesaparecida = new SelectList(db.CrearPersonaPerdida, "Id_CPP", "Nombre", fotosPersona.id_personaDesaparecida);
            return View(fotosPersona);
        }

        // POST: FotosPersonas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Fotos,id_personaDesaparecida,Fotos")] FotosPersona fotosPersona, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            byte[] imagenActual = null;
            HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase == null)
            {
                imagenActual = db.FotosPersona.SingleOrDefault(t => t.Id_Fotos == fotosPersona.Id_Fotos).Fotos;

            }
            else
            {

                WebImage image = new WebImage(FileBase.InputStream);
                fotosPersona.Fotos = image.GetBytes();
            }
            if (ModelState.IsValid)
            {
                db.Entry(fotosPersona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_personaDesaparecida = new SelectList(db.CrearPersonaPerdida, "Id_CPP", "Nombre", fotosPersona.id_personaDesaparecida);
            return View(fotosPersona);
        }

        // GET: FotosPersonas/Delete/5
        public ActionResult Delete(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FotosPersona fotosPersona = db.FotosPersona.Find(id);
            if (fotosPersona == null)
            {
                return HttpNotFound();
            }
            return View(fotosPersona);
        }

        // POST: FotosPersonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.nombre = NombreUsuario;
            ViewBag.correo = Correo;

            FotosPersona fotosPersona = db.FotosPersona.Find(id);
            db.FotosPersona.Remove(fotosPersona);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult getImage(int id)
        {
            FotosPersona fotos = db.FotosPersona.Find(id);
            byte[] byteImage = fotos.Fotos;

            System.IO.MemoryStream memoryStream = new MemoryStream(byteImage);

            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }
    }
}
