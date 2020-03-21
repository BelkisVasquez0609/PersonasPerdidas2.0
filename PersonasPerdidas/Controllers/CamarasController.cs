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
    public class CamarasController : Controller
    {
        private VisualRekognitionComparisonEntities2 db = new VisualRekognitionComparisonEntities2();

        // GET: Camaras
        public ActionResult Index(int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            ViewBag.Fecha = DateTime.Now;
            return View(db.Camara.ToList());
        }
        public ActionResult EncontramosTuPariente(int id = 0)
        {
            ViewBag.id = id;

            return View();
        }
        public ActionResult ParienteEncontrado(int id)
        {
            ViewBag.Fecha = DateTime.Now;
            var query = (from a in db.CrearPersonaPerdida
                         where a.Id_CPP == id
                         select a).FirstOrDefault();

            query.FechaEncontrado = DateTime.Now;
            query.Estado = true;

            db.SaveChanges();

            return View("EncontramosTuPariente");
        }

        // GET: Camaras/Details/5
        public ActionResult Details(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            ViewBag.Fecha = DateTime.Now;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camara camara = db.Camara.Find(id);
            if (camara == null)
            {
                return HttpNotFound();
            }
            return View(camara);
        }

        // GET: Camaras/Create
        public ActionResult Create(int idpariente, string correousuario,int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            ViewBag.Fecha = DateTime.Now;
            return View();
        }

        // POST: Camaras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_foto,foto")] Camara camara, int idpariente = 0, int rol = 0, int usuario = 0, string NombreUsuario = "", string Correo = "")
        {

            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            ViewBag.Fecha = DateTime.Now;

            //nos va a representar un objeto y nos permite administrar lo 
            //que es un archivo como tal,es unna clase base que nos permite proporcionar 
            //ese acceso a los archivos que hemos cargado desde el navegador
            HttpPostedFileBase FileBase = Request.Files[0];

            if (FileBase.ContentLength == 0)
            {
                ModelState.AddModelError("Foto", "El campo necesario seleccionar una imagen.");

            }
            else
            {
                if (FileBase.FileName.EndsWith(".jpg"))
                {
                    //ahora esta clase nos permite administrar la imagen

                    System.Web.Helpers.WebImage image = new WebImage(FileBase.InputStream);

                    camara.foto = image.GetBytes(); //aqui se obtienen los bytes de nuestra imagen


                }
                else
                {
                    ModelState.AddModelError("Foto", "El sistema solo acepta un formato.JPG");
                }

               
            }

            if (ModelState.IsValid)
            {
                db.Camara.Add(camara);
                db.SaveChanges();
                idpariente = 2007;
                Ccorreo objcorreo = new Ccorreo(Correo, "Hemos encontrado tu pariente!", "Hola! hemos encontrado tu pariente, entra a este link y confirmanos :"+ "https://vencindarioseguro.azurewebsites.net/Camaras/EncontramosTuPariente/" + idpariente);
                if (objcorreo.Estado)
                {
                    Response.Write("el correo se envio con exito...");

                }
                else
                {
                    Response.Write("Error al enviar el correo electronico...<br>" + objcorreo.mensaje_error);

                }
                return RedirectToAction("FotoEnviadaCamara", "Camaras", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });

            }

            return View(camara);
        }

        // GET: Camaras/Edit/5
        public ActionResult Edit(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            ViewBag.Fecha = DateTime.Now;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camara camara = db.Camara.Find(id);
            if (camara == null)
            {
                return HttpNotFound();
            }
            return View(camara);
        }

        // POST: Camaras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_foto,foto")] Camara camara, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            ViewBag.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(camara).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(camara);
        }

        // GET: Camaras/Delete/5
        public ActionResult Delete(int? id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            ViewBag.Fecha = DateTime.Now;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camara camara = db.Camara.Find(id);
            if (camara == null)
            {
                return HttpNotFound();
            }
            return View(camara);
        }

        // POST: Camaras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            ViewBag.Fecha = DateTime.Now;
            Camara camara = db.Camara.Find(id);
            db.Camara.Remove(camara);
            db.SaveChanges();
            return RedirectToAction("Index", "Camaras", new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
        }
        public ActionResult FotoEnviadaCamara( int rol, int usuario, string NombreUsuario, string Correo)
        {
            ViewBag.Rol = rol;
            ViewBag.UsuarioActual = usuario;
            ViewBag.NombreUsuario = NombreUsuario;
            ViewBag.correo = Correo;
            ViewBag.Fecha = DateTime.Now;
           
            return View(new { rol = rol, usuario = usuario, NombreUsuario = NombreUsuario, Correo = Correo });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //nuevo metodo
        public ActionResult getImage(int id)
        {
            Camara camara = db.Camara.Find(id);
            byte[] byteImage = camara.foto;

            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(byteImage);

            Image image = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }
    }
}
