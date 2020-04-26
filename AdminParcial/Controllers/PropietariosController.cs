using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminParcial.Models.DB;

namespace AdminParcial.Controllers
{
    public class PropietariosController : Controller
    {
        private ParcialAdminEntities db = new ParcialAdminEntities();

        // GET: Propietarios
        public ActionResult Index()
        {
            return View(db.Propietarios.ToList());
        }

        // GET: Propietarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propietario propietario = db.Propietarios.Find(id);
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        // GET: Propietarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Propietario,Nombre,Apelllido,DPI,Telefono,Direccion")] Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                db.Propietarios.Add(propietario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propietario);
        }

        // GET: Propietarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propietario propietario = db.Propietarios.Find(id);
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        // POST: Propietarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Propietario,Nombre,Apelllido,DPI,Telefono,Direccion")] Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propietario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propietario);
        }

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Propietario propietario = db.Propietarios.Find(id);
            if (propietario == null)
            {
                return HttpNotFound();
            }
            return View(propietario);
        }

        // POST: Propietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Propietario propietario = db.Propietarios.Find(id);
            db.Propietarios.Remove(propietario);
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
    }
}
