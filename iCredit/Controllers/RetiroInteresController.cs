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
    public class RetiroInteresController : Controller
    {
        private CrediAdminContext db = new CrediAdminContext();

        // GET: RetiroInteres
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
            listaFiltro.Add(new SelectListItem { Text = "Nro", Value = "SortRetiroInteresNro" });
            ViewBag.SortRetiroInteresId = sortOrder == "SortRetiroInteresNro" ? "SortRetiroInteresNro_Desc" : "SortRetiroInteresNro";
						  listaFiltro.Add(new SelectListItem { Text = "Valor", Value = "Valor" });
			  ViewBag.SortValor = sortOrder == "Valor" ? "Valor_Desc" : "Valor";
						  listaFiltro.Add(new SelectListItem { Text = "Fecha", Value = "Fecha" });
			  ViewBag.SortFecha = sortOrder == "Fecha" ? "Fecha_Desc" : "Fecha";
              //            listaFiltro.Add(new SelectListItem { Text = "EmpresaId", Value = "EmpresaId" });
              //ViewBag.SortEmpresaId = sortOrder == "EmpresaId" ? "EmpresaId_Desc" : "EmpresaId";
						  listaFiltro.Add(new SelectListItem { Text = "Observacion", Value = "Observacion" });
			  ViewBag.SortObservacion = sortOrder == "Observacion" ? "Observacion_Desc" : "Observacion";
						  listaFiltro.Add(new SelectListItem { Text = "Estado", Value = "Estado" });
			  ViewBag.SortEstado = sortOrder == "Estado" ? "Estado_Desc" : "Estado";
			            ViewBag.campos1 = listaFiltro;
            var q = "select * from retirointeres where EmpresaId='"+empresaId.ToString()+"'";
            List<retirointeres> lista;
            if (!String.IsNullOrEmpty(campos1) && !String.IsNullOrEmpty(filtro1))
            {
                 if (!campos1.ToUpper().Equals("ESTADO"))
                    q = q + " and upper(" + campos1 + ") like '%" + filtro1.Trim().ToUpper() + "%'";
                else
                    q = q + " and (CASE WHEN estado = 1 THEN 'ACTIVO' ELSE 'INACTIVO' END)= '" + filtro1.Trim().ToUpper() + "'";

                lista = db.Database.SqlQuery< retirointeres >(q).ToList();
            }
            else
			{
                							var retirointeres = db.retirointeres.Include(r => r.empresa).Where(r=>r.EmpresaId==empresaId);
											lista=retirointeres.ToList();
								
				switch (sortOrder)
                {

								  case "RetiroInteresId":
					lista = lista.OrderBy(s => s.RetiroInteresId).ToList();
					break;
				   
				   case "RetiroInteresId_Desc":
					lista = lista.OrderByDescending(s => s.RetiroInteresId).ToList();
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
								  case "EmpresaId":
					lista = lista.OrderBy(s => s.EmpresaId).ToList();
					break;
				   
				   case "EmpresaId_Desc":
					lista = lista.OrderByDescending(s => s.EmpresaId).ToList();
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
                    lista = lista.OrderByDescending(s => s.RetiroInteresId).ToList();
                    break;
				                }
			}

			int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["RegistrosPorPagina"].ToString()); 
            int pageNumber = (page ?? 1);
            return View(lista.ToPagedList(pageNumber, pageSize));

    		//return  View(lista);
        }

        // GET: RetiroInteres/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);

            retirointeres retirointeres = db.retirointeres.Find(intId);

            if (retirointeres == null)
            {
                return HttpNotFound();
            }
            return View(retirointeres);
        }

        // GET: RetiroInteres/Create
        public ActionResult Create()
        {
            int empresaId = 0;
            if (Session["EmpresaId"] != null)
                Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            retirointeres r = new retirointeres();
            r.EmpresaId=empresaId;
            //ViewBag.EmpresaId = empresaId;
            r.Fecha=DateTime.Now;
                //new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit");
            return View(r);
        }

        // POST: RetiroInteres/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RetiroInteresId,Valor,Fecha,EmpresaId,Observacion,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion,Estado")] retirointeres retirointeres)
        {
            if (ModelState.IsValid)
            {
                //int empresaId = 0;
                //if (Session["EmpresaId"] != null)
                //    Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
                consecutivo c = db.consecutivo.Find(retirointeres.EmpresaId);
                c.RetiroInteresNro = c.RetiroInteresNro + 1;
                retirointeres.RetiroInteresNro = c.RetiroInteresNro;

                db.retirointeres.Add(retirointeres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpresaId = retirointeres.EmpresaId;
            //ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", retirointeres.EmpresaId);
            return View(retirointeres);
        }

        // GET: RetiroInteres/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			string idDecrypted = MiUtil.desEncriptar(HttpUtility.UrlDecode(id));
            int intId = Convert.ToInt32(idDecrypted);
            retirointeres retirointeres = db.retirointeres.Find(intId);
            if (retirointeres == null)
            {
                return HttpNotFound();
            }
            //ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", retirointeres.EmpresaId);
            ViewBag.EmpresaId = retirointeres.EmpresaId;
            return View(retirointeres);
        }

        // POST: RetiroInteres/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RetiroInteresId,RetiroInteresNro,Valor,Fecha,EmpresaId,Observacion,CreadoPor,FechaCreacion,ModificadoPor,FechaModificacion,Estado")] retirointeres retirointeres)
        {

            if (ModelState.IsValid)
            {
                db.Entry(retirointeres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.EmpresaId = new SelectList(db.empresa.Where(c=>c.Estado==true).OrderBy(e=>e.Nit), "EmpresaId", "Nit", retirointeres.EmpresaId);
            ViewBag.EmpresaId = retirointeres.EmpresaId;
            return View(retirointeres);
        }

        // GET: RetiroInteres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            retirointeres retirointeres = db.retirointeres.Find(id);
            if (retirointeres == null)
            {
                return HttpNotFound();
            }
            return View(retirointeres);
        }

        // POST: RetiroInteres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            retirointeres retirointeres = db.retirointeres.Find(id);
            db.retirointeres.Remove(retirointeres);
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
