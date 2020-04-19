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
    public class ConceptoRetirosController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        // GET: ConceptoRetiros
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
						  listaFiltro.Add(new SelectListItem { Text = "ConceptoRetiroId", Value = "ConceptoRetiroId" });
			  ViewBag.SortConceptoRetiroId = sortOrder == "ConceptoRetiroId" ? "ConceptoRetiroId_Desc" : "ConceptoRetiroId";
						  listaFiltro.Add(new SelectListItem { Text = "Nombre", Value = "Nombre" });
			  ViewBag.SortNombre = sortOrder == "Nombre" ? "Nombre_Desc" : "Nombre";
						  listaFiltro.Add(new SelectListItem { Text = "EmpresaId", Value = "EmpresaId" });
			  ViewBag.SortEmpresaId = sortOrder == "EmpresaId" ? "EmpresaId_Desc" : "EmpresaId";
						  listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
			  ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
			            ViewBag.campos1 = listaFiltro;
            var q = "select * from conceptoretiro where empresaId='" + empresaId.ToString() + "'";
            List<conceptoretiro> lista;
            if (!String.IsNullOrEmpty(campos1) && !String.IsNullOrEmpty(filtro1))
            {
                 if (!campos1.ToUpper().Equals("ESTADO"))
                    q = q + " where upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
                else
                    q = q + " where (CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

                lista = db.Database.SqlQuery< conceptoretiro >(q).ToList();
            }
            else
			{
                var conceptoretiro = db.conceptoretiro.Include(c => c.empresa).Where(c => c.EmpresaId == empresaId);
											lista=conceptoretiro.ToList();
								
				switch (sortOrder)
                {

								  case "ConceptoRetiroId":
					lista = lista.OrderBy(s => s.ConceptoRetiroId).ToList();
					break;
				   
				   case "ConceptoRetiroId_Desc":
					lista = lista.OrderByDescending(s => s.ConceptoRetiroId).ToList();
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

        // GET: ConceptoRetiros/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);
            conceptoretiro conceptoretiro = db.conceptoretiro.Find(intId);
            if (conceptoretiro == null)
            {
                return HttpNotFound();
            }
            return View(conceptoretiro);
        }

        // GET: ConceptoRetiros/Create
        public ActionResult Create()
        {
            int empresaId = 0;
            //Convert.ToInt32(data);
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);

            ViewBag.EmpresaId = empresaId;//new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit");
            conceptoretiro cr = new conceptoretiro();
            cr.EmpresaId = empresaId;
            cr.Estado = true;

            return View(cr);
        }

        // POST: ConceptoRetiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConceptoRetiroId,Nombre,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] conceptoretiro conceptoretiro)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            conceptoretiro.EmpresaId = empresaId;
            if (ModelState.IsValid)
            {
                db.conceptoretiro.Add(conceptoretiro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", conceptoretiro.EmpresaId);
            return View(conceptoretiro);
        }

        // GET: ConceptoRetiros/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);
            conceptoretiro conceptoretiro = db.conceptoretiro.Find(intId);
            if (conceptoretiro == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", conceptoretiro.EmpresaId);
            return View(conceptoretiro);
        }

        // POST: ConceptoRetiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConceptoRetiroId,Nombre,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] conceptoretiro conceptoretiro)
        {

            if (ModelState.IsValid)
            {
                db.Entry(conceptoretiro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", conceptoretiro.EmpresaId);
            return View(conceptoretiro);
        }

        // GET: ConceptoRetiros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conceptoretiro conceptoretiro = db.conceptoretiro.Find(id);
            if (conceptoretiro == null)
            {
                return HttpNotFound();
            }
            return View(conceptoretiro);
        }

        // POST: ConceptoRetiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            conceptoretiro conceptoretiro = db.conceptoretiro.Find(id);
            db.conceptoretiro.Remove(conceptoretiro);
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
