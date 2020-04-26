using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminParcial.Models;
using AdminParcial.Models.DB;
using FastReport;
using FastReport.Export.PdfSimple;
using FastReport.Utils;

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

        public ActionResult Deudores()
        {
            var propietarios = db.Propietarios
                .Include(x => x.Terrenoes)
                .ToList();

            var temp = new List<PropietarioDTO>();
            foreach (var item in propietarios)
            {
                if(item.Terrenoes.Any(x => !x.Solvencia.GetValueOrDefault()))
                {
                    continue;
                }
                else
                {
                    temp.Add(new PropietarioDTO() { Nombre = item.Nombre, Apellido = item.Apelllido});
                }
            }

            Report report = new Report();

            string thisFolder = Config.ApplicationFolder;

            string path = Path.Combine(thisFolder, "Reportes\\Deudores.frx");
            string fullPath = Path.GetFullPath(path);

            report.Load(fullPath);

            report.RegisterData(temp, "Propietario");

            var fecha = DateTime.Now.ToLongDateString();

            report.SetParameterValue("Fecha", fecha);

            var nombreArchivo = $"{fecha} Deudores.pdf";

            report.Prepare();

            PDFSimpleExport export = new PDFSimpleExport();
            using (MemoryStream ms = new MemoryStream())
            {
                export.Export(report, ms);
                ms.Flush();
                return File(ms.ToArray(), "application/pdf", nombreArchivo);
            }
        }

        public ActionResult EstadoDeCuenta(long id)
        {
            var propietario = db.Propietarios
                .Where(x => x.Id_Propietario == id)
                .Include(x => x.Terrenoes)
                .FirstOrDefault();

            var temp = new List<EstadoCuentaDTO>();
            var nombrePropietario = $"{propietario.Nombre} {propietario.Apelllido}";

            var total = 0m;
            foreach (var item in propietario.Terrenoes)
            {
                if (!item.Solvencia.GetValueOrDefault())
                {
                    continue;
                }
                var suma = 0m;
                foreach (var recibo in item.Reciboes)
                {
                    suma += recibo.Pago_Detalle.Monto.GetValueOrDefault();
                }
                temp.Add(new EstadoCuentaDTO() { Direccion = $"{item.Direccion} Lote {item.Lote}", Monto = suma.ToString("F")});
                total += suma;
            }

            Report report = new Report();

            string thisFolder = Config.ApplicationFolder;

            string path = Path.Combine(thisFolder, "Reportes\\EstadoCuenta.frx");
            string fullPath = Path.GetFullPath(path);

            report.Load(fullPath);

            report.RegisterData(temp, "EstadoCuenta");

            var fecha = DateTime.Now.ToLongDateString();

            report.SetParameterValue("Fecha", fecha);
            report.SetParameterValue("Propietario", nombrePropietario);
            report.SetParameterValue("Total", total.ToString("F"));

            var nombreArchivo = $"{fecha} {nombrePropietario} Estado de Cuenta.pdf";

            report.Prepare();

            PDFSimpleExport export = new PDFSimpleExport();
            using (MemoryStream ms = new MemoryStream())
            {
                export.Export(report, ms);
                ms.Flush();
                return File(ms.ToArray(), "application/pdf", nombreArchivo);
            }
        }
    }
}
