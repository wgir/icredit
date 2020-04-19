using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Configuration;
using CrediAdmin.Util;

namespace CrediAdmin.Models
{
    [SessionExpire]
    [Authorize]
    public class AporteController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        // GET: Aporte
        public ActionResult Index(string socioId, string campos1, string filtro1, string currentCampo, string currentFilter, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            string socioDecrypted = MiUtil.desEncriptar(socioId);
            int intSocioId = Convert.ToInt32(socioDecrypted);
            socio socio = db.socio.Find(intSocioId);
            ViewBag.socioId = intSocioId.ToString();
            ViewBag.socioNombre = socio.Nombre;

            if (filtro1 != null)
                page = 1;
            else
            {
                campos1 = currentCampo;
                filtro1 = currentFilter;
            }
            campos1 = String.IsNullOrEmpty(campos1) ? "" : campos1;
            ViewBag.CurrentCampo = campos1;
            ViewBag.CurrentFilter = filtro1;

            List<SelectListItem> listaFiltro = new List<SelectListItem>();
            //listaFiltro.Add(new SelectListItem { Text = "AporteId", Value = "AporteId" });
            //ViewBag.SortAporteId = sortOrder == "AporteId" ? "AporteId_Desc" : "AporteId";
            listaFiltro.Add(new SelectListItem { Text = "Valor", Value = "Valor" });
            ViewBag.SortValor = sortOrder == "Valor" ? "Valor_Desc" : "Valor";
            listaFiltro.Add(new SelectListItem { Text = "Fecha", Value = "Fecha" });
            ViewBag.SortFecha = sortOrder == "Fecha" ? "Fecha_Desc" : "Fecha";
            //            listaFiltro.Add(new SelectListItem { Text = "SocioId", Value = "SocioId" });
            //ViewBag.SortSocioId = sortOrder == "SocioId" ? "SocioId_Desc" : "SocioId";
            listaFiltro.Add(new SelectListItem { Text = "Concepto", Value = "c.Nombre" });
            ViewBag.SortConceptoAporteId = sortOrder == "ConceptoAporteId" ? "ConceptoAporteId_Desc" : "ConceptoAporteId";
            listaFiltro.Add(new SelectListItem { Text = "Observacion", Value = "Observacion" });
            ViewBag.SortObservacion = sortOrder == "Observacion" ? "Observacion_Desc" : "Observacion";
            listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
            ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
            ViewBag.campos1 = listaFiltro;
            var q = "select * from aporte a,conceptoaporte c where a.ConceptoAporteId=c.ConceptoAporteId";
            q=q+" and socioId='" + intSocioId.ToString() + "'";
            List<aporte> lista;
            if (!String.IsNullOrEmpty(campos1) && !String.IsNullOrEmpty(filtro1))
            {
                if (!campos1.ToUpper().Equals("ESTADO"))
                    q = q + " and upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
                else
                    q = q + " and (CASE WHEN a.estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

                lista = db.Database.SqlQuery<aporte>(q).ToList();
                foreach(aporte a in lista)
                {
                    conceptoaporte ca=db.conceptoaporte.Find(a.ConceptoAporteId);
                    a.conceptoaporte = ca;
                }
            }
            else
            {
                var aporte = db.aporte.Include(a => a.conceptoaporte).Include(a => a.socio).Where(s => s.SocioId == intSocioId);
                lista = aporte.ToList();

                switch (sortOrder)
                {

                    case "AporteId":
                        lista = lista.OrderBy(s => s.AporteId).ToList();
                        break;

                    case "AporteId_Desc":
                        lista = lista.OrderByDescending(s => s.AporteId).ToList();
                        break;
                    case "Valor":
                        lista = lista.OrderBy(s => s.Valor).ToList();
                        break;

                    case "Valor_Desc":
                        lista = lista.OrderByDescending(s => s.Valor).ToList();
                        break;
                    case "Fecha":
                        lista = lista.OrderBy(s => s.Fecha).ToList();
                        break;

                    case "Fecha_Desc":
                        lista = lista.OrderByDescending(s => s.Fecha).ToList();
                        break;
                    case "SocioId":
                        lista = lista.OrderBy(s => s.SocioId).ToList();
                        break;

                    case "SocioId_Desc":
                        lista = lista.OrderByDescending(s => s.SocioId).ToList();
                        break;
                    case "ConceptoAporteId":
                        lista = lista.OrderBy(s => s.ConceptoAporteId).ToList();
                        break;

                    case "ConceptoAporteId_Desc":
                        lista = lista.OrderByDescending(s => s.ConceptoAporteId).ToList();
                        break;
                    case "Observacion":
                        lista = lista.OrderBy(s => s.Observacion).ToList();
                        break;

                    case "Observacion_Desc":
                        lista = lista.OrderByDescending(s => s.Observacion).ToList();
                        break;
                    case "Estado":
                        lista = lista.OrderBy(s => s.Estado).ToList();
                        break;

                    case "Estado_Desc":
                        lista = lista.OrderByDescending(s => s.Estado).ToList();
                        break;
                    default:
                        lista = lista.OrderByDescending(s => s.AporteId).ToList();
                        break;
                }
            }

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["RegistrosPorPagina"].ToString());
            int pageNumber = (page ?? 1);
            return View(lista.ToPagedList(pageNumber, pageSize));

            //return  View(lista);
        }

        // GET: Aporte/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string socioDecrypted = MiUtil.desEncriptar(id);
            int intSocioId = Convert.ToInt32(socioDecrypted);
            aporte aporte = db.aporte.Find(intSocioId);
            ViewBag.idSocio = aporte.SocioId.ToString();
            if (aporte == null)
            {
                return HttpNotFound();
            }
            return View(aporte);
        }

        // GET: Aporte/Create
        public ActionResult Create(string id)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            string socioDecrypted = MiUtil.desEncriptar(id);
            int intSocioId = Convert.ToInt32(socioDecrypted);

            socio socio = db.socio.Find(intSocioId);
            ViewBag.idSocio = intSocioId.ToString();
            ViewBag.socioNombre = socio.Nombre;

            ViewBag.ConceptoAporteId = new SelectList(db.conceptoaporte.Where(c => c.Estado == true && c.EmpresaId == empresaId).OrderBy(e => e.Nombre), "ConceptoAporteId", "Nombre");
            ViewBag.EmpresaId = empresaId;

            // ViewBag.SocioId = new SelectList(db.socio.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "SocioId", "Nit");
            aporte a = new aporte();
            a.SocioId = intSocioId;
            a.Fecha = DateTime.Now;
            a.Estado = true;
            return View(a);

            //return View();
        }

        // POST: Aporte/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AporteId,Valor,Fecha,SocioId,ConceptoAporteId,Observacion,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] aporte aporte,
            int? EmpresaId)
        {
            if (ModelState.IsValid)
            {
                db.aporte.Add(aporte);
                db.SaveChanges();
                return RedirectToAction("Index", new { socioId = MiUtil.encriptar(aporte.SocioId.ToString()) });
            }
            ViewBag.EmpresaId = EmpresaId;
            ViewBag.idSocio = aporte.SocioId.ToString();
            ViewBag.ConceptoAporteId = new SelectList(db.conceptoaporte.Where(c => c.Estado == true && c.EmpresaId == EmpresaId).OrderBy(e => e.Nombre), "ConceptoAporteId", "Nombre", aporte.ConceptoAporteId);
            ViewBag.SocioId = new SelectList(db.socio.Where(c => c.Estado == true).OrderBy(e => e.Nit), "SocioId", "Nit", aporte.SocioId);
            return View(aporte);
        }

        // GET: Aporte/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);

            
            //socio socio = db.socio.Find(intSocioId);
            
            //ViewBag.socioNombre = socio.Nombre;

            aporte aporte = db.aporte.Find(intId);
            if (aporte == null)
            {
                return HttpNotFound();
            }

            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);

            ViewBag.idSocio = aporte.SocioId.ToString();
            ViewBag.EmpresaId = empresaId;
            ViewBag.ConceptoAporteId = new SelectList(db.conceptoaporte.Where(c => c.Estado == true && c.EmpresaId==empresaId).OrderBy(e => e.Nombre), "ConceptoAporteId", "Nombre", aporte.ConceptoAporteId);
            ViewBag.SocioId = new SelectList(db.socio.Where(c => c.Estado == true).OrderBy(e => e.Nit), "SocioId", "Nit", aporte.SocioId);
            return View(aporte);
        }

        // POST: Aporte/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AporteId,Valor,Fecha,SocioId,ConceptoAporteId,Observacion,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] aporte aporte,
            int EmpresaId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aporte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { socioId = MiUtil.encriptar(aporte.SocioId.ToString()) });
            }
            ViewBag.idSocio = aporte.SocioId.ToString();
            ViewBag.EmpresaId = EmpresaId;
            //int intSocioId = Convert.ToInt32(socioDecrypted);
           // socio socio = db.socio.Find(intSocioId);
            //ViewBag.idSocio = intSocioId.ToString();

            ViewBag.ConceptoAporteId = new SelectList(db.conceptoaporte.Where(c => c.Estado == true && c.EmpresaId==EmpresaId).OrderBy(e => e.Nombre), "ConceptoAporteId", "Nombre", aporte.ConceptoAporteId);
            ViewBag.SocioId = new SelectList(db.socio.Where(c => c.Estado == true).OrderBy(e => e.Nit), "SocioId", "Nit", aporte.SocioId);
            return View(aporte);
        }

        // GET: Aporte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            aporte aporte = db.aporte.Find(id);
            if (aporte == null)
            {
                return HttpNotFound();
            }
            return View(aporte);
        }

        // POST: Aporte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            aporte aporte = db.aporte.Find(id);
            db.aporte.Remove(aporte);
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
