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
    public class Pago_DetalleController : Controller
    {
        private ParcialAdminEntities db = new ParcialAdminEntities();

        // GET: Pago_Detalle
        public ActionResult Index()
        {
            return View(db.Pago_Detalle.ToList());
        }

        // GET: Pago_Detalle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago_Detalle pago_Detalle = db.Pago_Detalle.Find(id);
            if (pago_Detalle == null)
            {
                return HttpNotFound();
            }
            return View(pago_Detalle);
        }

        // GET: Pago_Detalle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pago_Detalle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Pago_Detalle,Descripcion,Monto")] Pago_Detalle pago_Detalle)
        {
            if (ModelState.IsValid)
            {
                db.Pago_Detalle.Add(pago_Detalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pago_Detalle);
        }

        // GET: Pago_Detalle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago_Detalle pago_Detalle = db.Pago_Detalle.Find(id);
            if (pago_Detalle == null)
            {
                return HttpNotFound();
            }
            return View(pago_Detalle);
        }

        // POST: Pago_Detalle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Pago_Detalle,Descripcion,Monto")] Pago_Detalle pago_Detalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pago_Detalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pago_Detalle);
        }

        // GET: Pago_Detalle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago_Detalle pago_Detalle = db.Pago_Detalle.Find(id);
            if (pago_Detalle == null)
            {
                return HttpNotFound();
            }
            return View(pago_Detalle);
        }

        // POST: Pago_Detalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pago_Detalle pago_Detalle = db.Pago_Detalle.Find(id);
            db.Pago_Detalle.Remove(pago_Detalle);
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
