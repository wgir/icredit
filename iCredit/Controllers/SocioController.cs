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
using CrediAdmin.ViewModels;
using CrediAdmin.Util;

namespace CrediAdmin.Controllers
{
    [SessionExpire]
    [Authorize]
    public class SocioController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        // GET: socio
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
            //listaFiltro.Add(new SelectListItem { Text = "SocioId", Value = "SocioId" });
            //ViewBag.SortSocioId = sortOrder == "SocioId" ? "SocioId_Desc" : "SocioId";
			listaFiltro.Add(new SelectListItem { Text = "Nit", Value = "s.Nit" });
			ViewBag.SortNit = sortOrder == "Nit" ? "Nit_Desc" : "Nit";
			listaFiltro.Add(new SelectListItem { Text = "Nombre", Value = "s.Nombre" });
			ViewBag.SortNombre = sortOrder == "Nombre" ? "Nombre_Desc" : "Nombre";
			listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "s.Estado" });
            //ViewBag.SortEmpresaId = sortOrder == "EmpresaId" ? "EmpresaId_Desc" : "EmpresaId";
            //            listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
			ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
			ViewBag.campos1 = listaFiltro;
            //var q = "select * from socio where empresaId='"+empresaId.ToString()+"'";
            List<SocioViewModel> lista=getListaSocios(campos1,filtro1);
            //if (!String.IsNullOrEmpty(campos1))
            //{
            //     if (!campos1.ToUpper().Equals("ESTADO"))
            //        q = q + " and upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
            //    else
            //        q = q + " and (CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

            //    lista = db.Database.SqlQuery< socio >(q).ToList();
            //}
            //else
            //{
              // var socio = db.socio.Include(s => s.empresa).Where(s=>s.EmpresaId==empresaId);
              //  lista = socio.ToList();
				switch (sortOrder)
                {

				   case "SocioId":
					lista = lista.OrderBy(s => s.SocioId).ToList();
					break;
				   
				   case "SocioId_Desc":
					lista = lista.OrderByDescending(s => s.SocioId).ToList();
					break;
								  case "Nit":
					lista = lista.OrderBy(s => s.Nit).ToList();
					break;
				   
				   case "Nit_Desc":
					lista = lista.OrderByDescending(s => s.Nit).ToList();
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
				               // }
			}

			int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["RegistrosPorPagina"].ToString()); 
            int pageNumber = (page ?? 1);
            return View(lista.ToPagedList(pageNumber, pageSize));

    		//return  View(lista);
        }

        public List<SocioViewModel> getListaSocios(string campo,string criterio)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            var q = @"select s.SocioId,s.Nit,s.Nombre,e.EmpresaId,s.Estado,
                    IFNULL((select sum(valor) from aporte a where a.socioid=s.socioid
                    and a.estado=1),0.0) as Aportes,
                    IFNULL((select sum(valor) from retiro r where r.socioid=s.socioid
                    and r.estado=1),0.0) as Retiros,0.0 as Saldo
                    from socio s,empresa e
                    where s.empresaid=e.empresaid
                    and s.empresaid='" + empresaId.ToString()+"'";
            if (!String.IsNullOrEmpty(campo))
            {
                if (!campo.ToUpper().Equals("S.ESTADO"))
                    q = q + " and upper(" + campo + ") like '%" + criterio.Trim().ToUpper() + "%'";
                else
                    q = q + " and (CASE WHEN s.estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + criterio.Trim().ToUpper() + "'";
            }
            var lista = db.Database.SqlQuery<SocioViewModel>(q).ToList();
            foreach (SocioViewModel item in lista)
            {
                item.Saldo = item.Aportes - item.Retiros;
            }
            return lista;
           
        }
        // GET: socio/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);

            socio socio = db.socio.Find(intId);
            if (socio == null)
            {
                return HttpNotFound();
            }
            return View(socio);
        }

        // GET: socio/Create
        public ActionResult Create()
        {
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit");
            return View();
        }

        // POST: socio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SocioId,Nit,Nombre,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] socio socio)
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            socio.EmpresaId = empresaId;
            if (ModelState.IsValid)
            {
                db.socio.Add(socio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", socio.EmpresaId);
            return View(socio);
        }

        // GET: socio/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string idDecrypted = MiUtil.desEncriptar(id);
            int intId = Convert.ToInt32(idDecrypted);

            socio socio = db.socio.Find(intId);
            if (socio == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", socio.EmpresaId);
            return View(socio);
        }

        // POST: socio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SocioId,Nit,Nombre,EmpresaId,Estado,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion")] socio socio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", socio.EmpresaId);
            return View(socio);
        }

        // GET: socio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            socio socio = db.socio.Find(id);
            if (socio == null)
            {
                return HttpNotFound();
            }
            return View(socio);
        }

        // POST: socio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            socio socio = db.socio.Find(id);
            db.socio.Remove(socio);
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
