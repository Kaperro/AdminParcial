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
    public class ReciboesController : Controller
    {
        private ParcialAdminEntities db = new ParcialAdminEntities();

        // GET: Reciboes
        public ActionResult Index()
        {
            var reciboes = db.Reciboes.Include(r => r.Pago_Detalle).Include(r => r.Terreno);
            return View(reciboes.ToList());
        }

        // GET: Reciboes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recibo recibo = db.Reciboes.Find(id);
            if (recibo == null)
            {
                return HttpNotFound();
            }
            return View(recibo);
        }

        // GET: Reciboes/Create
        public ActionResult Create()
        {
            ViewBag.Id_Pago_Detalle = new SelectList(db.Pago_Detalle, "Id_Pago_Detalle", "Descripcion");
            ViewBag.Id_Tereno = new SelectList(db.Terrenoes, "Id_Terreno", "Lote");
            return View();
        }

        // POST: Reciboes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Recibo,Id_Tereno,Id_Pago_Detalle,Fecha,Mes")] Recibo recibo)
        {
            if (ModelState.IsValid)
            {
                db.Reciboes.Add(recibo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Pago_Detalle = new SelectList(db.Pago_Detalle, "Id_Pago_Detalle", "Descripcion", recibo.Id_Pago_Detalle);
            ViewBag.Id_Tereno = new SelectList(db.Terrenoes, "Id_Terreno", "Lote", recibo.Id_Tereno);
            return View(recibo);
        }

        // GET: Reciboes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recibo recibo = db.Reciboes.Find(id);
            if (recibo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Pago_Detalle = new SelectList(db.Pago_Detalle, "Id_Pago_Detalle", "Descripcion", recibo.Id_Pago_Detalle);
            ViewBag.Id_Tereno = new SelectList(db.Terrenoes, "Id_Terreno", "Lote", recibo.Id_Tereno);
            return View(recibo);
        }

        // POST: Reciboes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Recibo,Id_Tereno,Id_Pago_Detalle,Fecha,Mes")] Recibo recibo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recibo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Pago_Detalle = new SelectList(db.Pago_Detalle, "Id_Pago_Detalle", "Descripcion", recibo.Id_Pago_Detalle);
            ViewBag.Id_Tereno = new SelectList(db.Terrenoes, "Id_Terreno", "Lote", recibo.Id_Tereno);
            return View(recibo);
        }

        // GET: Reciboes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recibo recibo = db.Reciboes.Find(id);
            if (recibo == null)
            {
                return HttpNotFound();
            }
            return View(recibo);
        }

        // POST: Reciboes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recibo recibo = db.Reciboes.Find(id);
            db.Reciboes.Remove(recibo);
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
