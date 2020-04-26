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
    public class TerrenoesController : Controller
    {
        private ParcialAdminEntities db = new ParcialAdminEntities();

        // GET: Terrenoes
        public ActionResult Index()
        {
            var terrenoes = db.Terrenoes.Include(t => t.Propietario);
            return View(terrenoes.ToList());
        }

        // GET: Terrenoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terreno terreno = db.Terrenoes.Find(id);
            if (terreno == null)
            {
                return HttpNotFound();
            }
            return View(terreno);
        }

        // GET: Terrenoes/Create
        public ActionResult Create()
        {
            ViewBag.Id_Propietario = new SelectList(db.Propietarios, "Id_Propietario", "Nombre");
            return View();
        }

        // POST: Terrenoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Terreno,Id_Propietario,Lote,Direccion,Solvencia")] Terreno terreno)
        {
            if (ModelState.IsValid)
            {
                db.Terrenoes.Add(terreno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Propietario = new SelectList(db.Propietarios, "Id_Propietario", "Nombre", terreno.Id_Propietario);
            return View(terreno);
        }

        // GET: Terrenoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terreno terreno = db.Terrenoes.Find(id);
            if (terreno == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Propietario = new SelectList(db.Propietarios, "Id_Propietario", "Nombre", terreno.Id_Propietario);
            return View(terreno);
        }

        // POST: Terrenoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Terreno,Id_Propietario,Lote,Direccion,Solvencia")] Terreno terreno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(terreno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Propietario = new SelectList(db.Propietarios, "Id_Propietario", "Nombre", terreno.Id_Propietario);
            return View(terreno);
        }

        // GET: Terrenoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terreno terreno = db.Terrenoes.Find(id);
            if (terreno == null)
            {
                return HttpNotFound();
            }
            return View(terreno);
        }

        // POST: Terrenoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Terreno terreno = db.Terrenoes.Find(id);
            db.Terrenoes.Remove(terreno);
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
