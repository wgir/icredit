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
    [Authorize]
    public class CiudadController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        // GET: ciudads
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
						  
			  listaFiltro.Add(new SelectListItem { Text = "Nombre", Value = "Nombre" });
			  ViewBag.SortNombre = sortOrder == "Nombre" ? "Nombre_Desc" : "Nombre";
						  
			  listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
			  ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
			            ViewBag.campos1 = listaFiltro;
            var q = "select * from ciudad where empresaId='"+empresaId.ToString()+"'";
            List<ciudad> lista;
            if (!String.IsNullOrEmpty(campos1) && !String.IsNullOrEmpty(filtro1))
            {
                 if (!campos1.ToUpper().Equals("ESTADO"))
                    q = q + " and  upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
                else
                    q = q + " and (CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

                lista = db.Database.SqlQuery< ciudad >(q).ToList();
            }
            else
			{
                							var ciudad = db.ciudad.Include(c => c.empresa).Where(c=>c.EmpresaId==empresaId);
											lista=ciudad.ToList();
								
				switch (sortOrder)
                {

								  case "CiudadId":
					lista = lista.OrderBy(s => s.CiudadId).ToList();
					break;
				   
				   case "CiudadId_Desc":
					lista = lista.OrderByDescending(s => s.CiudadId).ToList();
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
            ciudad ciudad = db.ciudad.Find(intId);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            return View(ciudad);
        }

        // GET: ConceptoAportes/Create
        public ActionResult Create()
        {
            int empresaId = 0;
            //Convert.ToInt32(data);
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);

            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit");
            ciudad ca = new ciudad();
            ca.EmpresaId = empresaId;
            ca.Estado = true;
            return View(ca);
        }

        // POST: ConceptoAportes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CiudadId,Nombre,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] ciudad ciudad)
        {
            //int empresaId = 0;
            //if (Session["EmpresaId"] != null)
            //    Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            //ciudad.EmpresaId = empresaId;

            if (ModelState.IsValid)
            {
                db.ciudad.Add(ciudad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", ciudad.EmpresaId);
            return View(ciudad);
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
            ciudad ciudad = db.ciudad.Find(intId);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", ciudad.EmpresaId);
            return View(ciudad);
        }

        // POST: ConceptoAportes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CiudadId,Nombre,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] ciudad ciudad)
        {

            if (ModelState.IsValid)
            {
                db.Entry(ciudad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", ciudad.EmpresaId);
            return View(ciudad);
        }

        // GET: ConceptoAportes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ciudad ciudad = db.ciudad.Find(id);
            if (ciudad == null)
            {
                return HttpNotFound();
            }
            return View(ciudad);
        }

        // POST: ConceptoAportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ciudad ciudad = db.ciudad.Find(id);
            db.ciudad.Remove(ciudad);
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
