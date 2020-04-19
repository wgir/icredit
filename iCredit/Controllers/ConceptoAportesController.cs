using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrediAdmin.Models;
using PagedList;
using System.Configuration;
using CrediAdmin.Util;

namespace CrediAdmin.Controllers
{
    [SessionExpire]
    [Authorize]
    public class ConceptoAportesController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        // GET: ConceptoAportes
        public ActionResult Index(string campos1, string filtro1, string currentCampo, string currentFilter,string sortOrder, int? page)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);

            ViewBag.CurrentSort = sortOrder;
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
						  listaFiltro.Add(new SelectListItem { Text = "ConceptoAporteId", Value = "ConceptoAporteId" });
			  ViewBag.SortConceptoAporteId = sortOrder == "ConceptoAporteId" ? "ConceptoAporteId_Desc" : "ConceptoAporteId";
						  listaFiltro.Add(new SelectListItem { Text = "Nombre", Value = "Nombre" });
			  ViewBag.SortNombre = sortOrder == "Nombre" ? "Nombre_Desc" : "Nombre";
						  listaFiltro.Add(new SelectListItem { Text = "EmpresaId", Value = "EmpresaId" });
			  ViewBag.SortEmpresaId = sortOrder == "EmpresaId" ? "EmpresaId_Desc" : "EmpresaId";
						  listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
			  ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
			            ViewBag.campos1 = listaFiltro;
            var q = "select * from conceptoaporte where empresaId='"+empresaId.ToString()+"'";
            List<conceptoaporte> lista;
            if (!String.IsNullOrEmpty(campos1))
            {
                 if (!campos1.ToUpper().Equals("ESTADO"))
                    q = q + " and  upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
                else
                    q = q + " and (CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

                lista = db.Database.SqlQuery< conceptoaporte >(q).ToList();
            }
            else
			{
                							var conceptoaporte = db.conceptoaporte.Include(c => c.empresa).Where(c=>c.EmpresaId==empresaId);
											lista=conceptoaporte.ToList();
								
				switch (sortOrder)
                {

								  case "ConceptoAporteId":
					lista = lista.OrderBy(s => s.ConceptoAporteId).ToList();
					break;
				   
				   case "ConceptoAporteId_Desc":
					lista = lista.OrderByDescending(s => s.ConceptoAporteId).ToList();
					break;
								  case "Nombre":
					lista = lista.OrderBy(s => s.Nombre).ToList();
					break;
				   
				   case "Nombre_Desc":
					lista = lista.OrderByDescending(s => s.Nombre).ToList();
					break;
								  case "EmpresaId":
					lista = lista.OrderBy(s => s.EmpresaId).ToList();
					break;
				   
				   case "EmpresaId_Desc":
					lista = lista.OrderByDescending(s => s.EmpresaId).ToList();
					break;
								  case "Estado":
					lista = lista.OrderBy(s => s.Estado).ToList();
					break;
				   
				   case "Estado_Desc":
					lista = lista.OrderByDescending(s => s.Estado).ToList();
					break;
				                }
			}

			int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["RegistrosPorPagina"].ToString()); 
            int pageNumber = (page ?? 1);
            return View(lista.ToPagedList(pageNumber, pageSize));

    		//return  View(lista);
        }

        // GET: ConceptoAportes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);
            conceptoaporte conceptoaporte = db.conceptoaporte.Find(intId);
            if (conceptoaporte == null)
            {
                return HttpNotFound();
            }
            return View(conceptoaporte);
        }

        // GET: ConceptoAportes/Create
        public ActionResult Create()
        {
            int empresaId = 0;
            //Convert.ToInt32(data);
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);

            ViewBag.EmpresaId = empresaId;// new SelectList(db.empresa.Where(c => c.Estado == true).OrderBy(e => e.Nit), "EmpresaId", "Nit");
            conceptoaporte ca = new conceptoaporte();
            ca.EmpresaId = empresaId;
            ca.Estado = true;
            return View();
        }

        // POST: ConceptoAportes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConceptoAporteId,Nombre,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] conceptoaporte conceptoaporte)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            conceptoaporte.EmpresaId = empresaId;

            if (ModelState.IsValid)
            {
                db.conceptoaporte.Add(conceptoaporte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", conceptoaporte.EmpresaId);
            return View(conceptoaporte);
        }

        // GET: ConceptoAportes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);
            conceptoaporte conceptoaporte = db.conceptoaporte.Find(intId);
            if (conceptoaporte == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", conceptoaporte.EmpresaId);
            return View(conceptoaporte);
        }

        // POST: ConceptoAportes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConceptoAporteId,Nombre,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] conceptoaporte conceptoaporte)
        {

            if (ModelState.IsValid)
            {
                db.Entry(conceptoaporte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", conceptoaporte.EmpresaId);
            return View(conceptoaporte);
        }

        // GET: ConceptoAportes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conceptoaporte conceptoaporte = db.conceptoaporte.Find(id);
            if (conceptoaporte == null)
            {
                return HttpNotFound();
            }
            return View(conceptoaporte);
        }

        // POST: ConceptoAportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            conceptoaporte conceptoaporte = db.conceptoaporte.Find(id);
            db.conceptoaporte.Remove(conceptoaporte);
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
